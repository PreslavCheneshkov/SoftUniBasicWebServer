using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class HttpServer : IHttpServer
    {
        public void AddRoute(string path, Func<HttpRequest, HttpResponse> actoin)
        {
            throw new NotImplementedException();
        }

        public void Start(int port)
        {
            throw new NotImplementedException();
        }
    }
}
