using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public class DefaultFirefoxWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateNewInstance()
        {
            try
            {
                return new FirefoxDriver();
            }
            catch (Exception exception)
            {
                var env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                var firefox = "Mozilla Firefox\\Firefox.exe";
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return AlternativeInstance(env, firefox);
                }

                env = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                if (File.Exists(Path.Combine(env, firefox)))
                {
                    return AlternativeInstance(env, firefox);
                }
                throw;
            }
        }

        private static IWebDriver AlternativeInstance(string env, string firefox)
        {
            return new FirefoxDriver(new FirefoxBinary(Path.Combine(env, firefox)), null);
        }
    }
}