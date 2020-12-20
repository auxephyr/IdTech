using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSegsSerializer : IDoomSegsSerializer
    {
        public static IDoomSegsSerializer Default { get; } = new DoomSegsSerializer();
        
        public List<DoomSeg> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 12;
            var result = new List<DoomSeg>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSeg
                {
                    StartVertex = reader.ReadInt16(),
                    EndVertex = reader.ReadInt16(),
                    Angle = reader.ReadInt16(),
                    Linedef = reader.ReadInt16(),
                    Direction = reader.ReadInt16(),
                    Offset = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomSeg> segs)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var seg in segs)
            {
                writer.Write(seg.StartVertex);
                writer.Write(seg.EndVertex);
                writer.Write(seg.Angle);
                writer.Write(seg.Linedef);
                writer.Write(seg.Direction);
                writer.Write(seg.Offset);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}