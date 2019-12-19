using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode_2019
{
    public partial class Day10
    {
        public class AsteroidField
        {
            public AsteroidField(IEnumerable<string> field)
            {
                Asteroids = new List<Vector2>();
                int x, y = 0;

                foreach (var row in field)
                {
                    x = 0;
                    foreach (var symbol in row)
                    {
                        if (symbol == '#')
                        {
                            Asteroids.Add(new Vector2(x, y));
                        }

                        x++;
                    }

                    y++;
                }
            }

            public List<Vector2> Asteroids { get; private set; }
        }
    }
}
