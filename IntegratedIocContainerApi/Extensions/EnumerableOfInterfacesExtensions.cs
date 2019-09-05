using System.Collections.Generic;
using System.Linq;

namespace IntegratedIocContainerApi.Extensions
{
    public static class EnumerableOfInterfacesExtensions
    {
        public static T ResolveClass<T>(this IEnumerable<T> enumerable, string className)
        {
            return enumerable.FirstOrDefault(x => x.GetType().Name == className);
        }
    }
}
