using SoftUniBasicWebServer.HTTP;
using SoftUniBasicWebServer.MVCFramework.ViewEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework
{
    public abstract class Controller
    {
        private SUSViewEngine viewEngine;
        public Controller()
        {
            this.viewEngine = new SUSViewEngine();
        }
        public HttpRequest Request { get; set; }
        public HttpResponse View(object viewModel = null, [CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            var viewContent = System.IO.File.ReadAllText(
                "Views/" + 
                this.GetType().Name.Replace("Controller", string.Empty) + "/" 
                + viewPath + ".cshtml");

            viewContent = this.viewEngine.GetHtml(viewContent, viewModel);

            var responseHtml = layout.Replace("@RenderBody()", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

            var response = new HttpResponse("text/html", responseBodyBytes);
            return response;
        }
        public HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response =  new HttpResponse(contentType, fileBytes);
            return response;
        }
        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));
            return response;
        }
    }
}
