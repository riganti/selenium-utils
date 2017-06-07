namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface ISeleniumWrapper

    {
        string Selector { get; }
        ISeleniumWrapper ParentWrapper { get; set; }
        string FullSelector { get; }
        void ActivateScope();
    }
}