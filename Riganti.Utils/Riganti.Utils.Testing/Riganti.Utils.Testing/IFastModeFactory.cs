namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface IFastModeFactory: IWebDriverFactory
    {
        void Clear();
        void Dispose();
        void Recreate();
    }
}