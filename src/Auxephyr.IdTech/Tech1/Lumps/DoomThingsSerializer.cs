using System;
using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomThingsSerializer : IDoomThingsSerializer
    {
        public static IDoomThingsSerializer Default { get; } = new DoomThingsSerializer();

        public List<DoomThing> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 10;
            var result = new List<DoomThing>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomThing
                {
                    X = SpanStream.ReadInt16(data, ref offset),
                    Y = SpanStream.ReadInt16(data, ref offset),
                    Angle = SpanStream.ReadInt16(data, ref offset),
                    Type = SpanStream.ReadInt16(data, ref offset),
                    Flags = (DoomThingFlags) SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomThing> things)
        {
            var data = new byte[things.Count * 10];
            var offset = 0;

            foreach (var thing in things)
            {
                SpanStream.Write(data, ref offset, thing.X);
                SpanStream.Write(data, ref offset, thing.Y);
                SpanStream.Write(data, ref offset, thing.Angle);
                SpanStream.Write(data, ref offset, thing.Type);
                SpanStream.Write(data, ref offset, (short) thing.Flags);
            }

            return data;
        }
    }
}