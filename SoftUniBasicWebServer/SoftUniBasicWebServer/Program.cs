using SoftUniBasicWebServer.HTTP;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHttpServer server = new HttpServer();


            server.AddRoute("/", HomePage);
            server.AddRoute("/favicon.ico", Favicon);
            server.AddRoute("/about", About);
            server.AddRoute("/users/login", Login);
            Process.Start(@"C:\Program Files(x86)\Microsoft\Edge\Application\msedge", @"http://localhost/");
            await server.StartAsync(80);
        }

        private static HttpResponse Favicon(HttpRequest request)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/favicon.ico");
            var response = new HttpResponse("image,vnd.microsoft.icon", fileBytes);
            return response;
        }

        static HttpResponse HomePage(HttpRequest request)
        {
            var responseHtml = "<h1>Hello, Welcome!</h1>" + request.Headers.FirstOrDefault(x => x.Name == "User-Agent")?.Value;
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        static HttpResponse About(HttpRequest request)
        {
            var responseHtml = "<h1>About.....</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        static HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login.....</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);
            return response; ;
        }
    }
}
