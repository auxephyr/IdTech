using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Editors
{
    [Service]
    public class DoomDoomVertexesSerializer : IDoomVertexesSerializer
    {
        public static IDoomVertexesSerializer Default { get; } = new DoomDoomVertexesSerializer();

        public List<DoomVertex> Read(byte[] data)
        {
            using var reader = new BinaryReader(new MemoryStream(data));
            var result = new List<DoomVertex>(data.Length / 2);
            
            for (var i = 0; i < data.Length; i++)
            {
                result[i] = new DoomVertex
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16()
                };
            }

            return result;
        }

        public byte[] Write(IEnumerable<DoomVertex> vertices)
        {
            using var output = new MemoryStream();
            using var writer = new BinaryWriter(output);

            foreach (var vertex in vertices)
            {
                Assert.That(() => vertex.X >= short.MinValue && vertex.X <= short.MaxValue, "Vertex X out of range.");
                Assert.That(() => vertex.Y >= short.MinValue && vertex.Y <= short.MaxValue, "Vertex Y out of range.");
                writer.Write((short) vertex.X);
                writer.Write((short) vertex.Y);
            }
            
            writer.Flush();
            return output.ToArray();
        }
    }
}