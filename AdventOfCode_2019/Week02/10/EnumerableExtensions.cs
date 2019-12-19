using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2019
{
    public static partial class EnumerableExtensions
    {
        public static IEnumerable<(T, T)> MakePairs<T>(this IEnumerable<T> set)
        {
            var i = 0;
            foreach (var item in set)
            {
                foreach (var other in set.Skip(i++))
                {
                    if (!other.Equals(item))
                    {
                        yield return (item, other);
                    }
                }
            }
        }

        public static IEnumerable<(T Yours, T Other)> MakePairs<T>(this IEnumerable<T> set, T item)
        {
            foreach (var other in set)
            {
                if (!other.Equals(item))
                {
                    yield return (item, other);
                }
            }
        }
    }
}
