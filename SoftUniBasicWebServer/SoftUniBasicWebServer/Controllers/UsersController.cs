using SoftUniBasicWebServer.HTTP;
using SoftUniBasicWebServer.MVCFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View(); ;
        }
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        internal HttpResponse DoLogin()
        {
            //read data
            //check user
            //log user
            //redirect home
            return this.Redirect("/");
        }
    }
}
