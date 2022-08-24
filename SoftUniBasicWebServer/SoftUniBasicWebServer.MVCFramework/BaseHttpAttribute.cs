using SoftUniBasicWebServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework
{
    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }
        public abstract HttpMethod Method { get; }
    }
}
