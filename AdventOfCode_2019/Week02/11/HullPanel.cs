using System.Numerics;

namespace AdventOfCode_2019
{
    public class HullPanel
    {
        public HullPanel(Vector2 location)
        {
            Location = location;
            White = false;
        }

        public Vector2 Location { get; }

        public bool White { get; private set; }

        public bool Black => !White;

        public void Paint(int white)
        {
            White = white == 1;
        }
    }
}