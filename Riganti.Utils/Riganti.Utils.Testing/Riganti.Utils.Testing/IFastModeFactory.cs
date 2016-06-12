namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IFastModeFactory: IWebDriverFactory
    {
        void Clear();
        void Dispose();
        void Recreate();
    }
}