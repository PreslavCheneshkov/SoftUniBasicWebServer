namespace SoftUniBasicWebServer.MVCFramework.ViewEngine
{
    public interface IView
    {
        string ExecuteTemplate(object viewModel);
    }
}
