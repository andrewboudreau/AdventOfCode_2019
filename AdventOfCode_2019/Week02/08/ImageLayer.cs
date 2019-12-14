using System;
using System.Text;

namespace AdventOfCode_2019.Week01
{
    public class ImageLayer
    {
        public int Width;
        public int Height;
        public int Zeros;
        public int Ones;
        public int Twos;
        public string Data;
        public int OrderFrontToBack;

        public ImageLayer(string data, int width, int height, int order)
        {
            Width = width;
            Height = height;
            OrderFrontToBack = order;

            foreach (var number in data)
            {
                Data = data;
                switch (number)
                {
                    case '0':
                        Zeros++;
                        break;

                    case '1':
                        Ones++;
                        break;

                    case '2':
                        Twos++;
                        break;

                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        break;

                    default:
                        throw new NotImplementedException($"Not supported '{number}' character.");
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var lines = Data.Length / Width;
            for (var i = 0; i < lines; i++)
            {
                sb.AppendLine(Data.Substring(i * Width, Width));
            }

            return sb.ToString().Replace("0", " ").Replace("1", "█");
        }
    }
}
