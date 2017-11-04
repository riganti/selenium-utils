using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace Riganti.Selenium.DotVVM.Samples.ViewModels
{
    public class FileUploadViewModel : DotvvmViewModelBase
    {
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();
    }
}