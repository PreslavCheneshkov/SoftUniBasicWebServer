﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBasicWebServer.MVCFramework.ViewEngine
{
    public interface IView
    {
        string ExecuteTemplate(object viewModel);
    }
}
