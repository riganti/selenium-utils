using System;

namespace Riganti.Selenium.Core.Abstractions
{
    public interface IServiceFactory
    {
        Type Resolve<T>();

    }
}