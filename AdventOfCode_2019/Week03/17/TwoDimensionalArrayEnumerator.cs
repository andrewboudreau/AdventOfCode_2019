using System.Collections;
using System.Collections.Generic;

namespace AdventOfCode_2019
{
    public class TwoDimensionalArrayEnumerator<TElement> : IEnumerator<(int X, int Y, TElement element)>
    {
        public static (int X, int Y, TElement element) NullArrayElement = (0, 0, default(TElement));

        private readonly TElement[,] array;
        private readonly int width;
        private readonly int height;
        private readonly int max;


        private int index = -1;

        public TwoDimensionalArrayEnumerator(TElement[,] array)
        {
            this.array = array;
            width = array.GetLength(1) - 1;
            height = array.GetLength(0) - 1;
            max = width * height;
        }

        public (int X, int Y, TElement element) Current
        {
            get
            {
                var x = index % width;
                var y = index / width;

                return (x, y, array[y, x]);
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            return (++index) < max;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
