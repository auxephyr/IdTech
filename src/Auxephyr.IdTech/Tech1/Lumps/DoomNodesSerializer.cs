using System;
using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomNodesSerializer : IDoomNodesSerializer
    {
        public static IDoomNodesSerializer Default { get; } = new DoomNodesSerializer();
        
        public List<DoomNode> Decode(ReadOnlySpan<byte> data)
        {
            var offset = 0;
            var count = data.Length / 28;
            var result = new List<DoomNode>(count);

            for (var i = 0; i < count; i++)
            {
                var node = new DoomNode
                {
                    X = SpanStream.ReadInt16(data, ref offset),
                    Y = SpanStream.ReadInt16(data, ref offset),
                    DeltaX = SpanStream.ReadInt16(data, ref offset),
                    DeltaY = SpanStream.ReadInt16(data, ref offset),
                    RightYUpperBound = SpanStream.ReadInt16(data, ref offset),
                    RightYLowerBound = SpanStream.ReadInt16(data, ref offset),
                    RightXLowerBound = SpanStream.ReadInt16(data, ref offset),
                    RightXUpperBound = SpanStream.ReadInt16(data, ref offset),
                    LeftYUpperBound = SpanStream.ReadInt16(data, ref offset),
                    LeftYLowerBound = SpanStream.ReadInt16(data, ref offset),
                    LeftXLowerBound = SpanStream.ReadInt16(data, ref offset),
                    LeftXUpperBound = SpanStream.ReadInt16(data, ref offset),
                    RightChild = SpanStream.ReadInt16(data, ref offset),
                    LeftChild = SpanStream.ReadInt16(data, ref offset)
                };
                
                result.Add(node);
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomNode> nodes)
        {
            var data = new byte[nodes.Count * 28];
            var offset = 0;
            
            foreach (var node in nodes)
            {
                SpanStream.Write(data, ref offset, node.X);
                SpanStream.Write(data, ref offset, node.Y);
                SpanStream.Write(data, ref offset, node.DeltaX);
                SpanStream.Write(data, ref offset, node.DeltaY);
                SpanStream.Write(data, ref offset, node.RightYUpperBound);
                SpanStream.Write(data, ref offset, node.RightYLowerBound);
                SpanStream.Write(data, ref offset, node.RightXLowerBound);
                SpanStream.Write(data, ref offset, node.RightXUpperBound);
                SpanStream.Write(data, ref offset, node.LeftYUpperBound);
                SpanStream.Write(data, ref offset, node.LeftYLowerBound);
                SpanStream.Write(data, ref offset, node.LeftXLowerBound);
                SpanStream.Write(data, ref offset, node.LeftXUpperBound);
                SpanStream.Write(data, ref offset, node.RightChild);
                SpanStream.Write(data, ref offset, node.LeftChild);
            }

            return data;
        }
    }
}