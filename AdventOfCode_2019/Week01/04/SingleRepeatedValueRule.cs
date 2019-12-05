using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    /// <summary>
    /// Returns false if no single digit repeated pair is found.
    /// </summary>
    public class SingleRepeatedValueRule
    {
        private static readonly ILogger logger = AdventOfCode.LogFactory.CreateLogger<SingleRepeatedValueRule>();

        public bool Check(int value)
        {
            var characters = value.ToString().Select(x => x).ToList();
            for (var i = 1; i < characters.Count; i++)
            {
                // Match is found but is it really only repeated twice?
                if (characters[i - 1] == characters[i])
                {
                    // Check beyond the matching pair just left and right for more matches.
                    if (i - 2 >= 0 && characters[i - 2] == characters[i])
                    {
                        logger.LogDebug($"{value}: character[{i - 2}] also contains repeated pair '{characters[i]}'");
                        continue;
                    }

                    if (i + 1 < characters.Count && characters[i + 1] == characters[i])
                    {
                        logger.LogDebug($"{value}: character[{i + 1}] also contains repeated pair '{characters[i]}'");
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
