using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomBlockmapSerializer : IDoomBlockmapSerializer
    {
        public static IDoomBlockmapSerializer Default { get; } = new DoomBlockmapSerializer();

        public List<DoomBlock> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 8;
            var result = new List<DoomBlock>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomBlock
                {
                    X = SpanStream.ReadInt16(data, ref offset),
                    Y = SpanStream.ReadInt16(data, ref offset),
                    Width = SpanStream.ReadInt16(data, ref offset),
                    Height = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomBlock> blocks)
        {
            var data = new byte[blocks.Count * 8];
            var offset = 0;
            
            foreach (var block in blocks)
            {
                SpanStream.Write(data, ref offset, block.X);
                SpanStream.Write(data, ref offset, block.Y);
                SpanStream.Write(data, ref offset, block.Width);
                SpanStream.Write(data, ref offset, block.Height);
            }

            return data;
        }
    }
}