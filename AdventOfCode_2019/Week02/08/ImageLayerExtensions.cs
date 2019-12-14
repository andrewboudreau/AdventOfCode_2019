using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode_2019.Week01
{
    public static class ImageLayerExtensions
    {
        public static ImageLayer Composite(this List<ImageLayer> layers)
        {
            var composite = new StringBuilder();
            for (var pixel = 0; pixel < layers.First().Data.Length; pixel++)
            {
                for (var layer = 0; layer < layers.Count; layer++)
                {
                    var layerPixelColor = layers[layer].Data[pixel];
                    if (layerPixelColor == '2')
                    {
                        continue;
                    }

                    composite.Append(layerPixelColor);
                    break;
                }
            }

            return new ImageLayer(composite.ToString(), layers.First().Width, layers.First().Height, 0);
        }
    }
}
