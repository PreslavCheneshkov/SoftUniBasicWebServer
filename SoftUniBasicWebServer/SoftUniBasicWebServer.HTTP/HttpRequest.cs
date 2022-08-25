using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>> Sessions = new Dictionary<string, Dictionary<string, string>>();
        public HttpRequest(string requestString)
        {
            var lines = requestString.Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);
            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;
            bool isInHeaders = true;
            StringBuilder bodyBuilder = new StringBuilder();
            while (lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }
                if (isInHeaders)
                {
                    //read headers
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodyBuilder.AppendLine(line);
                }
            }
            if (this.Headers.Any(x => x.Name == "Cookie"))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x => x.Name == "Cookie").Value;
                var cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }

            var sessionCookie = this.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);
            if (sessionCookie == null)
            {
                var sessionId = Guid.NewGuid().ToString();
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionId, this.Session);
                this.Cookies.Add(new Cookie(HttpConstants.SessionCookieName, sessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            this.Body = bodyBuilder.ToString();
            var parameters = this.Body.Split('&', StringSplitOptions.RemoveEmptyEntries);
            foreach (var param in parameters)
            {
                var paramParts = param.Split('=');
                var name = paramParts[0];
                var value = WebUtility.UrlDecode(paramParts[1]);
                if (!this.FormData.ContainsKey(name))
                {
                    this.FormData.Add(name, value);
                }
                this.FormData[name] = value;
            }
        }
        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public ICollection<Header> Headers { get; set; } = new List<Header>();
        public ICollection<Cookie> Cookies { get; set; } = new List<Cookie>();
        public IDictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Session { get; set; }

        public string Body { get; set; }
    }
}
