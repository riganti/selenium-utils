using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Testing.Selenium.Core;

namespace WebApplication1.Tests
{
  public class DotvvmUITests :SeleniumTestBase
    {
      public override void BeforeSpecificBrowserTestStarts(BrowserWrapper browser)
      {
          browser.BaseUrl = "http://localhost:58872/";
      }

        public void DetectDotvvm()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl();

            });


        }

        public void FileUpload()
      {
          
      }





    }
}
