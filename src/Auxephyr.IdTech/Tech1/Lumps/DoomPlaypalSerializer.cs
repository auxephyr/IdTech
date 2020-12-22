using System;
using System.Collections.Generic;
using System.Drawing;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomPlaypalSerializer : IDoomPlaypalSerializer
    {
        public List<DoomPalette> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 768;
            var result = new List<DoomPalette>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                var colors = new Color[256];

                for (var j = 0; j < 256; j++)
                {
                    var r = data[offset++];
                    var g = data[offset++];
                    var b = data[offset++];
                    colors[j] = Color.FromArgb(r, g, b);
                }

                result.Add(new DoomPalette {Colors = colors});
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomPalette> palettes)
        {
            Assert.IsNotNull(palettes, nameof(palettes));
            
            var data = new byte[768 * palettes.Count];
            var offset = 0;

            foreach (var palette in palettes)
            {
                Assert.IsNotNull(palette.Colors, nameof(palette.Colors));
                
                foreach (var color in palette.Colors)
                {
                    SpanStream.Write(data, ref offset, color.R);
                    SpanStream.Write(data, ref offset, color.G);
                    SpanStream.Write(data, ref offset, color.B);
                }
            }

            return data;
        }
    }
}