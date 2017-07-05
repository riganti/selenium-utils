using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.Selenium.Core;
using Riganti.Utils.Testing.Selenium.Core.Api;
using Riganti.Utils.Testing.Selenium.DotVVM;

namespace Selenium.DotVVM.Samples.Tests
{
    [TestClass]
    public class BasicTestsNewApi : SeleniumTest
    {
        [TestMethod]
        public void DetectDotvvm()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();
                Assert.IsTrue(browser.IsDotvvmPage());
                browser.NavigateToUrl("//google.com/?query=wallpaper");
                browser.Wait(1000);
                Assert.IsFalse(browser.IsDotvvmPage());
            });
        }

        [TestMethod]
        public void FileUpload_ElementWrapper()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/FileUpload");

                var tempPath = Path.GetTempFileName();
                File.WriteAllBytes(tempPath, Enumerable.Range(0, 255).Select(i => (byte)i).ToArray());


                browser.First("#FUpload").UploadFile(tempPath);


                browser.WaitFor(() =>
                {
                    AssertUI.CheckIfInnerTextEquals(browser.First("#FUpload .dotvvm-upload-files"), "1 files", false);

                }, 4000, "File upload failed.");
            });
        }
    }
}