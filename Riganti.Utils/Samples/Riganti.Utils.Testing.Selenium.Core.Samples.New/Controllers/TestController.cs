using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.New.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Alert() { return View(null); }
        public ActionResult Attribute()
        {
            return View(null);
        }
        public ActionResult Checkboxes() { return View(null); }
        public ActionResult ClickTest() { return View(null); }
        public ActionResult Confirm() { return View(null); }

        public ActionResult CookiesTest()
        {
            if (!ControllerContext.HttpContext.Request.Cookies.ContainsKey("test_cookie"))
            {
                CookieOptions cookie = new CookieOptions();
                Response.Cookies.Append("test_cookie", "default value", cookie);
            }
            return View(new CookieModel(){Text = ControllerContext.HttpContext.Request.Cookies["test_cookie"] });
        }

        public ActionResult CookiesSetCookie()
        {
            CookieOptions cookie = new CookieOptions();
            Response.Cookies.Append("test_cookie", "new value", cookie);
            return View("CookiesTest", new CookieModel() { Text = ControllerContext.HttpContext.Request.Cookies["test_cookie"] });
        }
        public ActionResult Displayed()
        {
            return View(null);
        }
        public ActionResult ElementAtFirst() { return View(null); }
        public ActionResult ElementContained() { return View(null); }
        public ActionResult ElementSelectionTest() { return View(null); }
        public ActionResult ElementVisibilityTests() { return View(null); }
        public ActionResult FileDialog() { return View(null); }
        public ActionResult FrameTest1() { return View(null); }
        public ActionResult FrameTest2() { return View(null); }
        public ActionResult FindElements() { return View(null); }
        public ActionResult HyperLink() { return View(null); }
        public ActionResult Index() { return View(null); }
        public ActionResult JsView() { return View(null); }
        public ActionResult JSPropertySetTest() { return View(null); }
        public ActionResult JSTestSite() { return View(null); }
        public ActionResult NoParentTest() { return View(null); }
        public ActionResult SelectMethod() { return View(null); }
        public ActionResult Site2() { return View(null); }
        public ActionResult TemporarySelector() { return View(null); }
        public ActionResult Text() { return View(null); }
        public ActionResult Title()
        {
            return View(null);
        }
        public ActionResult Value() { return View(null); }

    }

    public class CookieModel
    {
        public string Text { get; set; }
    }
}
