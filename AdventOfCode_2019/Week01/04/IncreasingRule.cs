using System.Linq;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// Returns true if all single-digit values increase from left to right.
    /// </summary>
    public class IncreasingRule
    {
        public bool Check(int value)
        {
            var characters = value.ToString().Select(x => x).ToList();
            for (var i = 1; i < characters.Count; i++)
            {
                if (characters[i - 1] > characters[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
