using System;
using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomLinedefsSerializer : IDoomLinedefsSerializer
    {
        public static IDoomLinedefsSerializer Default { get; } = new DoomLinedefsSerializer();

        public List<DoomLinedef> Decode(ReadOnlySpan<byte> data)
        {
            var offset = 0;
            var count = data.Length / 14;
            var result = new List<DoomLinedef>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomLinedef
                {
                    StartVertex = SpanStream.ReadInt16(data, ref offset),
                    EndVertex = SpanStream.ReadInt16(data, ref offset),
                    Flags = (DoomLinedefFlags) SpanStream.ReadInt16(data, ref offset),
                    Special = (DoomLinedefSpecial) SpanStream.ReadInt16(data, ref offset),
                    Tag = SpanStream.ReadInt16(data, ref offset),
                    RightSidedef = SpanStream.ReadInt16(data, ref offset),
                    LeftSidedef = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomLinedef> linedefs)
        {
            var data = new byte[linedefs.Count * 14];
            var offset = 0;

            foreach (var linedef in linedefs)
            {
                SpanStream.Write(data, ref offset, linedef.StartVertex);
                SpanStream.Write(data, ref offset, linedef.EndVertex);
                SpanStream.Write(data, ref offset, (short) linedef.Flags);
                SpanStream.Write(data, ref offset, (short) linedef.Special);
                SpanStream.Write(data, ref offset, linedef.Tag);
                SpanStream.Write(data, ref offset, linedef.RightSidedef);
                SpanStream.Write(data, ref offset, linedef.LeftSidedef);
            }

            return data;
        }
    }
}