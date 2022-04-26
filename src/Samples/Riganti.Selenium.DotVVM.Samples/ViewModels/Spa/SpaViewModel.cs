using System.Threading;
using DotVVM.Framework.ViewModel;

namespace Riganti.Selenium.DotVVM.Samples.ViewModels.Spa
{
    public class SpaViewModel : DotvvmViewModelBase
    {
        public SpaViewModel()
        {
            Thread.Sleep(5000);
        }
    }
}