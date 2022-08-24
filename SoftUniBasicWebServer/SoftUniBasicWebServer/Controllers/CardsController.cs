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
    public class CardsController : Controller
    {
        public HttpResponse Add()
        {
            return this.View();
        }
        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {
            var request = this.Request;
            var viewModel = new DoAddViewModel
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
            };
            return this.View(viewModel);
        }
        public HttpResponse All()
        {
            return this.View();
        }
        public HttpResponse Collection()
        {
            return this.View();
        }
    }
}
