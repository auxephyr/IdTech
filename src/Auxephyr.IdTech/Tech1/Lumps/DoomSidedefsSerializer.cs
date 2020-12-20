using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSidedefsSerializer : IDoomSidedefsSerializer
    {
        public static IDoomSidedefsSerializer Default { get; } = new DoomSidedefsSerializer();
        
        public List<DoomSidedef> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 30;
            var result = new List<DoomSidedef>();
            
            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSidedef
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    UpperTexture = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8))),
                    LowerTexture = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8))),
                    MiddleTexture = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8))),
                    Sector = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomSidedef> sidedefs)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var sidedef in sidedefs)
            {
                writer.Write(sidedef.X);
                writer.Write(sidedef.Y);
                writer.Write(Cp437.Pad(Cp437.Encode(sidedef.UpperTexture), 8));
                writer.Write(Cp437.Pad(Cp437.Encode(sidedef.LowerTexture), 8));
                writer.Write(Cp437.Pad(Cp437.Encode(sidedef.MiddleTexture), 8));
                writer.Write(sidedef.Sector);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}