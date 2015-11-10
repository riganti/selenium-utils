namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface ISeleniumWrapper

    {
        string Selector { get; }
        ISeleniumWrapper ParentWrapper { get; set; }
        string FullSelector { get; }
    }
}