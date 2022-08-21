﻿using SoftUniBasicWebServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.HTTP
{
    public class Route
    {
        public Route(string path, Func<HttpRequest, HttpResponse> action)
        {
            Path = path;
            Action = action;
        }

        public string Path { get; set; }
        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}