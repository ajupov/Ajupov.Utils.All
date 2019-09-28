using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.All.Enums
{
    public static class EnumsExtensions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).OfType<T>();
        }
    }
}