﻿namespace SoftUniBasicWebServer.MVCFramework.ViewEngine
{
    public interface IViewEngine
    {
        string GetHtml(string templateCode, object viewModel);
    }
}
