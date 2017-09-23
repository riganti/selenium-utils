using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Riganti.Utils.Testing.Selenium.Core.Samples.Models;
using Riganti.Utils.Testing.Selenium.Core.Samples.New.Controllers;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.Controllers
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
