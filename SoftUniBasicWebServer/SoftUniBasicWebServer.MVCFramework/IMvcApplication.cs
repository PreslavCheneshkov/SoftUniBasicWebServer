using SoftUniBasicWebServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework
{
    public interface IMvcApplication
    {
        public void ConfigureServices();
        public void Configure(List<Route> routeTable);
    }
}
