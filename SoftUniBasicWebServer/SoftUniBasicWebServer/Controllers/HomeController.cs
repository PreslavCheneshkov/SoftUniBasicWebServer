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
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Welcome to Battle Cards";
            if (this.Request.Session.ContainsKey("about"))
            {
                viewModel.Message += "You were on the about page";
            }
            return this.View(viewModel);
        }
        public HttpResponse About()
        {
            this.Request.Session["about"] = "yes";
            return this.View();
        }
    }
}
