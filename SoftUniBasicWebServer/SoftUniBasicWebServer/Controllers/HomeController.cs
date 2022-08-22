using SoftUniBasicWebServer.HTTP;
using SoftUniBasicWebServer.MVCFramework;
using SoftUniBasicWebServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            return this.View(viewModel);
        }
        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}
