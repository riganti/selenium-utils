using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.Options;
using Riganti.Selenium.Coordinator.Service.Services;

namespace Riganti.Selenium.Coordinator.Service.ViewModels
{
    public class SettingsViewModel : DotvvmViewModelBase
    {
        private readonly DockerProvisioningService service;
        private readonly IOptions<AppConfiguration> config;
        public string DockerUrl => config.Value.DockerApiUrl;
        public string ExternalUrlPattern => config.Value.ExternalUrlPattern;
        public SettingsViewModel(DockerProvisioningService service, IOptions<AppConfiguration> config)
        {
            this.service = service;
            this.config = config;
        }

        public override async Task PreRender()
        {
            Browsers = await service.GetAvailableBrowsers();
        }

        public async Task Download()
        {
            try
            {

         
            var downloads = Browsers.Where(a => !a.IsAvailable).ToList();
            foreach (var download in downloads)
            {
                await service.TryPullImages(download.ImageName);
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AvailableBrowserTypeImage> Browsers { get; set; }
    }
}

