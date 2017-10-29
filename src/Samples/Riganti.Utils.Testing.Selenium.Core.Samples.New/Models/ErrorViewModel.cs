using System;

namespace Riganti.Utils.Testing.Selenium.Core.Samples.New.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}