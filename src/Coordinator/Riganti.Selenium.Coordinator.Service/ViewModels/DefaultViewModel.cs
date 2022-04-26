using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using Riganti.Selenium.Coordinator.Service.Data;
using Riganti.Selenium.Coordinator.Service.Services;

namespace Riganti.Selenium.Coordinator.Service.ViewModels
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
