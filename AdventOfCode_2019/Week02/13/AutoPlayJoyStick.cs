using System;

namespace AdventOfCode_2019.Arcade
{
    public class AutoPlayJoyStick
    {
        private readonly ArcadeCabinet arcade;

        public AutoPlayJoyStick(ArcadeCabinet arcade)
        {
            this.arcade = arcade;
        }

        public int Value()
        {
            /*
             * If the joystick is in the neutral position, provide 0.
             * If the joystick is tilted to the left, provide -1.
             * If the joystick is tilted to the right, provide 1.
             */

            if (arcade.Ball == arcade.Paddle)
            {
                return 0;
            }
            else if (arcade.Ball > arcade.Paddle)
            {
                return 1;
            }
            else if (arcade.Ball < arcade.Paddle)
            {
                return -1;
            }

            return 0;
        }

        public int Random()
        {
            return new Random().Next(-1, 1);
        }
    }
}
