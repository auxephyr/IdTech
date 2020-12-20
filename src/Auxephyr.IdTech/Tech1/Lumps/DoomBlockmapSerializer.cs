using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomBlockmapSerializer : IDoomBlockmapSerializer
    {
        public static IDoomBlockmapSerializer Default { get; } = new DoomBlockmapSerializer();

        public List<DoomBlock> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 8;
            var result = new List<DoomBlock>(count);

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomBlock
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    Width = reader.ReadInt16(),
                    Height = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomBlock> blocks)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var block in blocks)
            {
                writer.Write(block.X);
                writer.Write(block.Y);
                writer.Write(block.Width);
                writer.Write(block.Height);
            }

            writer.Flush();
            return stream.ToArray();
        }
    }
}