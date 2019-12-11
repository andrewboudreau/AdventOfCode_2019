using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2019.Week01
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }
    }
}
