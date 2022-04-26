using Microsoft.AspNetCore.Mvc;
using Riganti.Selenium.Core.Samples.Models;
using Riganti.Selenium.Core.Samples.New.Controllers;

namespace Riganti.Selenium.Core.Samples.Controllers
{
    public class HomeController :Controller
    {

        public ActionResult Index()
        {
            var urls = new UrlsModel();
            urls.Urls = typeof(TestController).GetMethods().Where(s=> s.ReturnType == typeof(ActionResult)).Select(s => "/test/" + s.Name).ToList();
            return View(urls);

        }
    }
}
