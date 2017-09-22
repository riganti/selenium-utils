using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.Options;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Data;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Model;
using Riganti.Utils.Testing.Selenium.Coordinator.Service.Services;

namespace Riganti.Utils.Testing.Selenium.Coordinator.Service.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        private readonly ContainerLeaseRepository containerLeaseRepository;


        public List<BrowserStatus> Browsers { get; private set; }


        public DefaultViewModel(ContainerLeaseRepository containerLeaseRepository)
        {
            this.containerLeaseRepository = containerLeaseRepository;
        }

        
        public override Task Init()
        {
            Browsers = containerLeaseRepository.GetAllBrowsers();

            return base.Init();
        }

        public async Task AcquireBrowser(string browserType)
        {
            try
            {
                await containerLeaseRepository.AcquireLease(browserType);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
