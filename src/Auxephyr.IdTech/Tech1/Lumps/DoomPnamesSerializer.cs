using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomPnamesSerializer : IDoomPnamesSerializer
    {
        public static IDoomPnamesSerializer Default { get; } = new DoomPnamesSerializer();

        public List<string> Decode(ReadOnlySpan<byte> data)
        {
            var offset = 0;
            var count = SpanStream.ReadInt32(data, ref offset);
            var result = new List<string>(count);

            for (var i = 0; i < count; i++)
                result.Add(Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))));

            return result;
        }

        public byte[] Encode(ICollection<string> names)
        {
            var offset = 0;
            var result = new byte[4 + names.Count * 4];

            SpanStream.Write(result, ref offset, names.Count);

            foreach (var name in names)
                SpanStream.Write(result, ref offset, Cp437.Pad(Cp437.Encode(name), 8));

            return result;
        }
    }
}