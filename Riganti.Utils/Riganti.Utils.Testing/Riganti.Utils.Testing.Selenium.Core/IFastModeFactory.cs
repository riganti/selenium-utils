namespace Riganti.Utils.Testing.Selenium.Core
{
    /// <summary>
    /// Represents mechanism to get browser for FastMode.
    /// </summary>
    public interface IFastModeFactory : IWebDriverFactory
    {
        /// <summary>
        /// Clears browser session.
        /// </summary>
        void Clear();

        /// <summary>
        /// Kill the browser.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Drop old instance and create new instance of the browser.
        /// </summary>
        void Recreate();
    }
}