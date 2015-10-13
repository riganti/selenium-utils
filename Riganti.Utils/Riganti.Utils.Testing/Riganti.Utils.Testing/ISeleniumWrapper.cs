namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface ISeleniumWrapper

    {
        string Selector { get; }
        ISeleniumWrapper Parent { get; set; }
        string FullSelector { get; }
    }
}