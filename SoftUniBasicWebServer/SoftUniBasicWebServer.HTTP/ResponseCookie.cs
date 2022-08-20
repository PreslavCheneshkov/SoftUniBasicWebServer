using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
            this.Path = "/";
        }
        public int MaxAge { get; set; }
        public string Path { get; set; }
        public bool HttpOnly { get; set; }
        //domain, secure
        public override string ToString()
        {
            StringBuilder cookieBuilder = new StringBuilder();
            cookieBuilder.Append($"{this.Name}={this.Value}; Path={this.Path};");

            if (MaxAge != 0)
            {
                cookieBuilder.Append($" Max-Age={this.MaxAge};");
            }
            if (this.HttpOnly)
            {
                cookieBuilder.Append($" HttpOnly;");
            }
            return cookieBuilder.ToString();
        }
    }
}
