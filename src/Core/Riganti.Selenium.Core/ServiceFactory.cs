using System;
using System.Collections.Concurrent;
using Riganti.Selenium.Core.Abstractions;

namespace Riganti.Selenium.Core
{
    public class ServiceFactory : IServiceFactory
    {

        private ConcurrentDictionary<Type, Type> Repository { get; set; } = new ConcurrentDictionary<Type, Type>();

        public bool RegisterTransient<TDescriptor, TImplementation>()
        {
            return Repository.TryAdd(typeof(TDescriptor), typeof(TImplementation));
        }
        public bool ReplaceTransient<TDescriptor, TImplementation>()
        {
            var implementation = typeof(TImplementation);
            var descriptor = typeof(TDescriptor);

            Repository.TryRemove(descriptor, out var a);
            return Repository.TryAdd(descriptor, implementation);
        }




        public Type ResolveType<T>()
        {
            return ResolveType(typeof(T));
        }

        private Type ResolveType(Type type)
        {
            if (Repository.ContainsKey(type))
            {
                Type a;
                Repository.TryGetValue(type, out a);
                return a;
            }

            throw new ArgumentException($"Type {type.FullName} is not registered in container.");
        }

        /// <inheritdoc />
        public T Resolve<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(ResolveType<T>(), args);
        }

        public object Resolve(Type type, params object[] args)
        {
            return Activator.CreateInstance(ResolveType(type), args);
        }
    }
}