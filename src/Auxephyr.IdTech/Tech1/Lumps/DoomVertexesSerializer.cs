using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomVertexesSerializer : IDoomVertexesSerializer
    {
        public static IDoomVertexesSerializer Default { get; } = new DoomVertexesSerializer();

        public List<DoomVertex> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 4;
            var result = new List<DoomVertex>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomVertex
                {
                    X = SpanStream.ReadInt16(data, ref offset),
                    Y = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomVertex> vertices)
        {
            var data = new byte[vertices.Count * 4];
            var offset = 0;

            foreach (var vertex in vertices)
            {
                SpanStream.Write(data, ref offset, vertex.X);
                SpanStream.Write(data, ref offset, vertex.Y);
            }

            return data;
        }
    }
}