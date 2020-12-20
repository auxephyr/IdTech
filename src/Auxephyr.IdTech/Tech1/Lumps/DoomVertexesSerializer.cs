using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomVertexesSerializer : IDoomVertexesSerializer
    {
        public static IDoomVertexesSerializer Default { get; } = new DoomVertexesSerializer();
        
        public List<DoomVertex> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 4;
            var result = new List<DoomVertex>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomVertex
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomVertex> vertices)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var vertex in vertices)
            {
                writer.Write(vertex.X);
                writer.Write(vertex.Y);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}