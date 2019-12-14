using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AdventOfCode_2019.Week01
{
    public class Day08 : Day00
    {
        public Day08(IServiceProvider serviceProvider, ILogger<Day07> logger)
            : base(serviceProvider, logger)
        {
            //DirectInput = new[] { "123456789012", "3", "2" };
        }

        protected override string Solve(IEnumerable<string> inputs)
        {
            var data = inputs.ToList();
            var image = data[0];
            var width = int.Parse(string.IsNullOrEmpty(data.ElementAtOrDefault(1)) ? "25" : data[1]);
            var height = int.Parse(string.IsNullOrEmpty(data.ElementAtOrDefault(2)) ? "6" : data[2]);

            var pixelsPerLayer = width * height;
            var layersPerImage = image.Length / pixelsPerLayer;

            var layers = new List<ImageLayer>();
            for (var i = 0; i < layersPerImage; i++)
            {
                layers.Add(new ImageLayer(image.Substring((i * pixelsPerLayer), pixelsPerLayer), width, height, i));
            }

            var minZeros = layers.OrderBy(x => x.Zeros).ToList();


            return $"{minZeros.First().Twos * minZeros.First().Ones}";
        }

        protected override string Solve2(IEnumerable<string> inputs)
        {
            var data = inputs.ToList();
            var image = data[0];
            var width = int.Parse(string.IsNullOrEmpty(data.ElementAtOrDefault(1)) ? "25" : data[1]);
            var height = int.Parse(string.IsNullOrEmpty(data.ElementAtOrDefault(2)) ? "6" : data[2]);

            var pixelsPerLayer = width * height;
            var layersPerImage = image.Length / pixelsPerLayer;

            var layers = new List<ImageLayer>();
            for (var i = 0; i < layersPerImage; i++)
            {
                layers.Add(new ImageLayer(image.Substring((i * pixelsPerLayer), pixelsPerLayer), width, height, i));
            }

            var composite = layers.Composite();
            return "\r\n" + composite.ToString();
        }
    }
}
