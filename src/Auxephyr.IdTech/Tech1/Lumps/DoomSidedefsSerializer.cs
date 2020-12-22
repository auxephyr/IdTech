using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSidedefsSerializer : IDoomSidedefsSerializer
    {
        public static IDoomSidedefsSerializer Default { get; } = new DoomSidedefsSerializer();

        public List<DoomSidedef> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 30;
            var result = new List<DoomSidedef>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSidedef
                {
                    X = SpanStream.ReadInt16(data, ref offset),
                    Y = SpanStream.ReadInt16(data, ref offset),
                    UpperTexture = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    LowerTexture = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    MiddleTexture = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    Sector = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomSidedef> sidedefs)
        {
            var data = new byte[sidedefs.Count * 30];
            var offset = 0;

            foreach (var sidedef in sidedefs)
            {
                SpanStream.Write(data, ref offset, sidedef.X);
                SpanStream.Write(data, ref offset, sidedef.Y);
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(sidedef.UpperTexture), 8));
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(sidedef.LowerTexture), 8));
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(sidedef.MiddleTexture), 8));
                SpanStream.Write(data, ref offset, sidedef.Sector);
            }

            return data;
        }
    }
}