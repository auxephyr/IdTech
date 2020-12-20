using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSectorsSerializer : IDoomSectorsSerializer
    {
        public static IDoomSectorsSerializer Default { get; } = new DoomSectorsSerializer();
        
        public List<DoomSector> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 26;
            var result = new List<DoomSector>();
            
            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSector
                {
                    FloorHeight = reader.ReadInt16(),
                    CeilingHeight = reader.ReadInt16(),
                    FloorTexture = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8))),
                    CeilingTexture = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8))),
                    LightLevel = reader.ReadInt16(),
                    Special = (DoomSectorSpecial) reader.ReadInt16(),
                    Tag = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomSector> sectors)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var sector in sectors)
            {
                writer.Write(sector.FloorHeight);
                writer.Write(sector.CeilingTexture);
                writer.Write(Cp437.Pad(Cp437.Encode(sector.FloorTexture), 8));
                writer.Write(Cp437.Pad(Cp437.Encode(sector.CeilingTexture), 8));
                writer.Write(sector.LightLevel);
                writer.Write((short) sector.Special);
                writer.Write(sector.Tag);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}