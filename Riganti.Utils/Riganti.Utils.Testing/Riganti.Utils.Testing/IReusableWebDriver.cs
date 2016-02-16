namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface IReusableWebDriver
    {
        void Clear();
        void Recreate();
        void Dispose();
    }
}