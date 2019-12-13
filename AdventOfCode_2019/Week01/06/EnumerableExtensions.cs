using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode_2019
{
    public static partial class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }

        public static int[] IntegersFromCsv(this string csv)
        {
            return csv.Split(",").Select(x => int.Parse(x.Trim())).ToArray();
        }
    }
}
