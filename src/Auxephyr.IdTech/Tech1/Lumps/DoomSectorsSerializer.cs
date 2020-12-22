using System;
using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSectorsSerializer : IDoomSectorsSerializer
    {
        public static IDoomSectorsSerializer Default { get; } = new DoomSectorsSerializer();
        
        public List<DoomSector> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 26;
            var result = new List<DoomSector>(count);
            var offset = 0;
            
            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSector
                {
                    FloorHeight = SpanStream.ReadInt16(data, ref offset),
                    CeilingHeight = SpanStream.ReadInt16(data, ref offset),
                    FloorTexture = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    CeilingTexture = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    LightLevel = SpanStream.ReadInt16(data, ref offset),
                    Special = (DoomSectorSpecial) SpanStream.ReadInt16(data, ref offset),
                    Tag = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomSector> sectors)
        {
            var data = new byte[sectors.Count * 26];
            var offset = 0;
            
            foreach (var sector in sectors)
            {
                SpanStream.Write(data, ref offset, sector.FloorHeight);
                SpanStream.Write(data, ref offset, sector.CeilingHeight);
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(sector.FloorTexture), 8));
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(sector.CeilingTexture), 8));
                SpanStream.Write(data, ref offset, sector.LightLevel);
                SpanStream.Write(data, ref offset, (short) sector.Special);
                SpanStream.Write(data, ref offset, sector.Tag);
            }

            return data;
        }
    }
}