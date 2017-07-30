﻿using Riganti.Utils.Testing.Selenium.Coordinator.Client;
using Riganti.Utils.Testing.Selenium.Runtime.Factories;

namespace Riganti.Utils.Testing.Selenium.Runtime.Drivers.Implementation
{
    public abstract class CoordinatorWebBrowserBase : WebBrowserBase
    {
        public new CoordinatorWebBrowserFactoryBase Factory => (CoordinatorWebBrowserFactoryBase) base.Factory;

        public ContainerLeaseDataDTO Lease { get; }

        public CoordinatorWebBrowserBase(CoordinatorWebBrowserFactoryBase factory, ContainerLeaseDataDTO lease) : base(factory)
        {
            Lease = lease;
        }
        
    }
}