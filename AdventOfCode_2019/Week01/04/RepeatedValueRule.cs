using System.Linq;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// Returns false if no single digit repeated pair is found.
    /// </summary>
    public class RepeatedValueRule
    {
        public bool Check(int value)
        {
            var characters = value.ToString().Select(x => x).ToList();
            for (var i = 1; i < characters.Count; i++)
            {
                if (characters[i - 1] == characters[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
