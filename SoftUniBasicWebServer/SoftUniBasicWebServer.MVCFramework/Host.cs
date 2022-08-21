using SoftUniBasicWebServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routeTable, int port = 80)
        {
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }
    }
}
