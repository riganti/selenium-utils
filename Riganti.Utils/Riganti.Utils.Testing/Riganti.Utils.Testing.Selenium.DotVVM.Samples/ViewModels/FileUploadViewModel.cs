using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace Selenium.DotVVM.Samples.ViewModels
{
    public class FileUploadViewModel : DotvvmViewModelBase
    {
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();
    }
}