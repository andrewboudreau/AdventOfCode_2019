using System.Collections.Generic;

namespace AdventOfCode_2019
{
    public static class IndexedArrayExtensions
    {
        public static IEnumerable<(int X, int Y, TElement item)> GetEnumerable<TElement>(this TElement[,] array)
        {
            for (var row = 0; row < array.GetLength(1); row++)
            {
                for (var column = 0; column < array.GetLength(2); column++)
                {
                    yield return (column, row, array[row, column]);
                }
            }
        }

        /// <summary>
        /// The largest value of an index for a 0-based array. (width-1) * (length-1).
        /// </summary>
        /// <typeparam name="TElement">They type of elements in the array.</typeparam>
        /// <param name="array">The array.</param>
        public static int IndexArea<TElement>(this TElement[,] array)
        {
            return (array.GetLength(0) - 1) * (array.GetLength(1) - 1);
        }

        /// <summary>
        /// The largest value of an index for a 0-based array. (width-1) * (length-1).
        /// </summary>
        /// <typeparam name="TElement">They type of elements in the array.</typeparam>
        /// <param name="array">The array.</param>
        public static int ToOneDimensionalIndex<TElement>(this TElement[,] array, (int X, int Y) index)
        {
            var width = array.GetLength(1);
            return index.Y * width + index.X;
        }
    }
}
