using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSsectorsSerializer : IDoomSsectorsSerializer
    {
        public static IDoomSsectorsSerializer Default { get; } = new DoomSsectorsSerializer();

        public List<DoomSubSector> Decode(ReadOnlySpan<byte> data)
        {
            var count = data.Length / 4;
            var result = new List<DoomSubSector>(count);
            var offset = 0;

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSubSector
                {
                    SegCount = SpanStream.ReadInt16(data, ref offset),
                    StartSeg = SpanStream.ReadInt16(data, ref offset)
                });
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomSubSector> subSectors)
        {
            var data = new byte[subSectors.Count * 4];
            var offset = 0;

            foreach (var subSector in subSectors)
            {
                SpanStream.Write(data, ref offset, subSector.SegCount);
                SpanStream.Write(data, ref offset, subSector.StartSeg);
            }

            return data;
        }
    }
}