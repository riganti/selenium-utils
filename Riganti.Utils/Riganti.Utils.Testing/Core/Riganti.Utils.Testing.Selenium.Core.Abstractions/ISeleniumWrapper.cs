namespace Riganti.Utils.Testing.Selenium.Core.Abstractions
{
    public interface ISeleniumWrapper

    {
        string Selector { get; }
        ISeleniumWrapper ParentWrapper { get; set; }
        string FullSelector { get; }
        void ActivateScope();
    }
}