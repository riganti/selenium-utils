using System.Linq;
using Riganti.Selenium.DotVVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Selenium.DotVVM.Samples.Tests
{
    [TestClass]
    public class FileUploadTests :  AppSeleniumTest
    {

        [TestMethod]
        public void FileUpload_ElementWrapperByDiv()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/FileUpload");

                var tempPath = Path.GetTempFileName();
                File.WriteAllBytes(tempPath, Enumerable.Range(0, 255).Select(i => (byte)i).ToArray());

                DotVVMAssert.UploadFile(browser.First("#FUpload"), tempPath);
                
                browser.WaitFor(() =>
                {
                    browser.First("#FUpload .dotvvm-upload-files").CheckIfInnerTextEquals("1 files", false);

                }, 4000, "File upload failed.");
            });
        }

        [TestMethod]
        public void FileUpload_ElementWrapperByA()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("/FileUpload");

                var tempPath = Path.GetTempFileName();
                File.WriteAllBytes(tempPath, Enumerable.Range(0, 255).Select(i => (byte)i).ToArray());

                DotVVMAssert.UploadFile(browser.First(".dotvvm-upload-button a"), tempPath);

                browser.WaitFor(() =>
                {
                    browser.First("#FUpload .dotvvm-upload-files").CheckIfInnerTextEquals("1 files", false);

                }, 4000, "File upload failed.");
            });
        }
    }
}
