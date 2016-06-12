using System;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public interface IWebDriverWrapper

    {
        Guid DriverId { get; }
        bool Disposed { get; set; }
    }
}