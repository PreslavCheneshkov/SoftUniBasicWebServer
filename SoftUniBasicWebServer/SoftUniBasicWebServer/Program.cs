using SoftUniBasicWebServer.Controllers;
using SoftUniBasicWebServer.HTTP;
using SoftUniBasicWebServer.MVCFramework;
using System;
using System.Collections.Generic;
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
            List<Route> routeTable = new List<Route>();

            


            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/favicon.ico", new StaticFilesController().Favicon));
            routeTable.Add(new Route("/users/login", new UsersController().Login));
            routeTable.Add(new Route("/users/register", new UsersController().Register));
            routeTable.Add(new Route("/cards/all", new CardsController().All));
            routeTable.Add(new Route("/cards/add", new CardsController().Add));
            routeTable.Add(new Route("/cards/collection", new CardsController().Collection));
            //Process.Start(@"C:\Program Files(x86)\Microsoft\Edge\Application\msedge", @"http://localhost/");

            await Host.CreateHostAsync(routeTable, 80);
        }
    }
}
