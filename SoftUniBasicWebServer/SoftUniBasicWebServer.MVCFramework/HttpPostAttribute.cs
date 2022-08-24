
using SoftUniBasicWebServer.HTTP;

namespace SoftUniBasicWebServer.MVCFramework
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public HttpPostAttribute()
        {

        }
        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }
        public override HttpMethod Method => HttpMethod.Post;
    }
}
