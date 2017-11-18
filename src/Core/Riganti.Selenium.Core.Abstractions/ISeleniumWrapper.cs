namespace Riganti.Selenium.Core.Abstractions
{
    public interface ISeleniumWrapper

    {
        /// <summary>
        /// Gets selector used to get this element.
        /// </summary>
        string Selector { get; }
        ISeleniumWrapper ParentWrapper { get; set; }
        /// <summary>
        /// Generated css selector to this element.
        /// </summary>
        string FullSelector { get; }
        /// <summary>
        /// Activates selenium SwitchTo function to change context to make element accessable.
        /// </summary>
        void ActivateScope();
    }
}