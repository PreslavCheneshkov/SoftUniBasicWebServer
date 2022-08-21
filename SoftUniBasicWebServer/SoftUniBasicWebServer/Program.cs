using SoftUniBasicWebServer.Controllers;
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


            server.AddRoute("/", new HomeController().Index);
            server.AddRoute("/favicon.ico", new StaticFilesController().Favicon);

            server.AddRoute("/users/login", new UsersController().Login);
            server.AddRoute("/users/register", new UsersController().Register);

            server.AddRoute("/cards/all", new CardsController().All);
            server.AddRoute("/cards/add", new CardsController().Add);
            server.AddRoute("/cards/collection", new CardsController().Collection);
            //Process.Start(@"C:\Program Files(x86)\Microsoft\Edge\Application\msedge", @"http://localhost/");
            await server.StartAsync(80);
        }
    }
}
