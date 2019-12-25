using System;
using System.Collections.Generic;
using System.Linq;

namespace Ajupov.Utils.All.Enums
{
    public static class EnumsExtensions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).OfType<T>();
        }

        public static Dictionary<string, T> GetAsDictionary<T>()
        {
            return GetValues<T>().ToDictionary(k => k.ToString(), v => v);
        }
    }
}