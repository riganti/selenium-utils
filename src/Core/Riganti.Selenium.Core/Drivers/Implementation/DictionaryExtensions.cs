using System.Collections.Generic;

namespace Riganti.Selenium.Core.Drivers.Implementation
{
    public static class DictionaryExtensions
    {

        public static T2 TryGet<T1, T2>(this IDictionary<T1, T2> dic, T1 key)
        {
            if (dic is not null && dic.TryGetValue(key, out T2 value) && value is T2)
            {
                return value;
            }
            return default;
        }
    }
}