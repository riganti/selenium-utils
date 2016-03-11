using System;

namespace Riganti.Utils.Testing.SeleniumCore
{
    public interface IWebDriverWrapper

    {
        Guid DriverId { get; }
        bool Disposed { get; set; }
    }
}