using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomSsectorsSerializer : IDoomSsectorsSerializer
    {
        public static IDoomSsectorsSerializer Default { get; } = new DoomSsectorsSerializer();

        public List<DoomSubSector> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 4;
            var result = new List<DoomSubSector>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomSubSector
                {
                    SegCount = reader.ReadInt16(),
                    StartSeg = reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomSubSector> subSectors)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var subSector in subSectors)
            {
                writer.Write(subSector.SegCount);
                writer.Write(subSector.StartSeg);
            }

            writer.Flush();
            return stream.ToArray();
        }
    }
}