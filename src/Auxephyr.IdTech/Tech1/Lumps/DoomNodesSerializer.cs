using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomNodesSerializer : IDoomNodesSerializer
    {
        public static IDoomNodesSerializer Default { get; } = new DoomNodesSerializer();
        
        public List<DoomNode> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 28;
            var result = new List<DoomNode>();

            for (var i = 0; i < count; i++)
            {
                var node = new DoomNode
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    DeltaX = reader.ReadInt16(),
                    DeltaY = reader.ReadInt16(),
                    RightYUpperBound = reader.ReadInt16(),
                    RightYLowerBound = reader.ReadInt16(),
                    RightXLowerBound = reader.ReadInt16(),
                    RightXUpperBound = reader.ReadInt16(),
                    LeftYUpperBound = reader.ReadInt16(),
                    LeftYLowerBound = reader.ReadInt16(),
                    LeftXLowerBound = reader.ReadInt16(),
                    LeftXUpperBound = reader.ReadInt16(),
                    RightChild = reader.ReadInt16(),
                    LeftChild = reader.ReadInt16()
                };
                
                result.Add(node);
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomNode> nodes)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var node in nodes)
            {
                writer.Write(node.X);
                writer.Write(node.Y);
                writer.Write(node.DeltaX);
                writer.Write(node.DeltaY);
                writer.Write(node.RightYUpperBound);
                writer.Write(node.RightYLowerBound);
                writer.Write(node.RightXLowerBound);
                writer.Write(node.RightXUpperBound);
                writer.Write(node.LeftYUpperBound);
                writer.Write(node.LeftYLowerBound);
                writer.Write(node.LeftXLowerBound);
                writer.Write(node.LeftXUpperBound);
                writer.Write(node.RightChild);
                writer.Write(node.LeftChild);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}