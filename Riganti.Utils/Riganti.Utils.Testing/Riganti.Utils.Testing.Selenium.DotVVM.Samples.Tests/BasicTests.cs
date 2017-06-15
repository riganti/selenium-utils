using Riganti.Utils.Testing.Selenium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.DotVVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Selenium.DotVVM.Samples.Tests
{
    public class BasicTests : SeleniumTest
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
                    browser.First("#FUpload .dotvvm-upload-files").CheckIfInnerTextEquals("1 files", false);

                }, 4000, "File upload failed.");
            });
        }


    }

}
