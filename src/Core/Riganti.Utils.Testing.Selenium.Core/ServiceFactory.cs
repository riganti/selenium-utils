using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Riganti.Utils.Testing.Selenium.Core
{
    public class ServiceFactory
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


        public Type Resolve<T>() 
        {
            if (Repository.ContainsKey(typeof(T)))
            {
                Type a;
                Repository.TryGetValue(typeof(T), out a);
                return a;
            }
            
            throw new ArgumentException($"Type {typeof(T).FullName} is not registered in container.");
        }

    }
}