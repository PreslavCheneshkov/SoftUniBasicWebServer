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
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new StartUp(), 80);
        }
    }
}
