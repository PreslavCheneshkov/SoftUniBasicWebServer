using SoftUniBasicWebServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class Route
    {
        public Route(string path, HttpMethod method, Func<HttpRequest, HttpResponse> action)
        {
            Path = path;
            Method = method;
            Action = action;
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}
