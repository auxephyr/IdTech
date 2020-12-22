using System;
using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSegsSerializer : IDoomSegsSerializer
    {
        public static IDoomSegsSerializer Default { get; } = new DoomSegsSerializer();

        public List<DoomSeg> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 12;
            var result = new List<DoomSeg>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSeg
                {
                    StartVertex = SpanStream.ReadInt16(data, ref offset),
                    EndVertex = SpanStream.ReadInt16(data, ref offset),
                    Angle = SpanStream.ReadInt16(data, ref offset),
                    Linedef = SpanStream.ReadInt16(data, ref offset),
                    Direction = SpanStream.ReadInt16(data, ref offset),
                    Offset = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomSeg> segs)
        {
            var data = new byte[segs.Count * 12];
            var offset = 0;

            foreach (var seg in segs)
            {
                SpanStream.Write(data, ref offset, seg.StartVertex);
                SpanStream.Write(data, ref offset, seg.EndVertex);
                SpanStream.Write(data, ref offset, seg.Angle);
                SpanStream.Write(data, ref offset, seg.Linedef);
                SpanStream.Write(data, ref offset, seg.Direction);
                SpanStream.Write(data, ref offset, seg.Offset);
            }

            return data;
        }
    }
}