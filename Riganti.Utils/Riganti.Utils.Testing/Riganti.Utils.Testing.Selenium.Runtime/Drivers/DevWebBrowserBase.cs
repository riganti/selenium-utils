namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers
{
    public abstract class DevWebBrowserBase : WebBrowserBase
    {
        public override void ClearBrowserState()
        {
            Driver?.Dispose();
            Driver = null;
        }
    }
}