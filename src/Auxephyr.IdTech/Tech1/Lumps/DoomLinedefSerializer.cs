using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomLinedefSerializer : IDoomLinedefSerializer
    {
        public static IDoomLinedefSerializer Default { get; } = new DoomLinedefSerializer();
        
        public List<DoomLinedef> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 14;
            var result = new List<DoomLinedef>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomLinedef
                {
                    StartVertex = reader.ReadInt16(),
                    EndVertex = reader.ReadInt16(),
                    Flags = (DoomLinedefFlags) reader.ReadInt16(),
                    Special = (DoomLinedefSpecial) reader.ReadInt16(),
                    Tag = reader.ReadInt16(),
                    RightSidedef = reader.ReadInt16(),
                    LeftSidedef = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomLinedef> linedefs)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var linedef in linedefs)
            {
                writer.Write(linedef.StartVertex);
                writer.Write(linedef.EndVertex);
                writer.Write((short) linedef.Flags);
                writer.Write((short) linedef.Special);
                writer.Write(linedef.Tag);
                writer.Write(linedef.RightSidedef);
                writer.Write(linedef.LeftSidedef);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}