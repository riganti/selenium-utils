namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IReusableWebDriver
    {
        void Clear();
        void Recreate();
        void Dispose();
    }
}