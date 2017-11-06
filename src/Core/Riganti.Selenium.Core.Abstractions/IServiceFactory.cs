using System;

namespace Riganti.Selenium.Core.Abstractions
{
    public interface IServiceFactory
    {
        T Resolve<T>(params object[] args);
        Type ResolveType<T>();
    }
}