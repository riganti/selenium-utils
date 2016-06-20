using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace Selenium.DotVVM.Samples.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {

        public string Title { get; set; }
        public List<RouteDTO> Routes {  get; set; } 
        public DefaultViewModel()
        {
            Title = "Hello from DotVVM!";
        }

        public override Task Load()
        {
            Routes = Context.Configuration.RouteTable.Where(a=> a.RouteName != "DefaultRoute").Select(s => new RouteDTO() {Name = s.RouteName, Url = s.Url}).ToList();
            return base.Load();
        }
    }
}
