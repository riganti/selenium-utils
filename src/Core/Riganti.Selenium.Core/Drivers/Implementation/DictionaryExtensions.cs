﻿using System.Collections.Generic;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class DictionaryExtensions
    {

        public static T2 TryGetOrDefault<T1, T2>(this IDictionary<T1, T2> dic, T1 key, T2 defaultValue)
        {
            if (dic is not null && dic.TryGetValue(key, out T2 value) && value is T2)
            {
                return value;
            }
            return defaultValue;
        }
    }
}