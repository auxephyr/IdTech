using System.Collections.Generic;
using System.IO;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Service]
    public class WadEntrySerializer : IWadEntrySerializer
    {
        public static IWadEntrySerializer Default { get; } = new WadEntrySerializer();
        
        public List<WadEntry> ReadStream(Stream stream, int count)
        {
            var reader = new BinaryReader(stream);
            return Enumerable.Range(0, count).Select(i => new WadEntry
            {
                Offset = reader.ReadInt32(),
                Length = reader.ReadInt32(),
                Name = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8)))
            }).ToList();
        }

        public int WriteStream(Stream stream, IEnumerable<WadEntry> entries)
        {
            var result = 0;
            var writer = new BinaryWriter(stream);
            foreach (var entry in entries)
            {
                writer.Write(entry.Offset);
                writer.Write(entry.Length);
                writer.Write(Cp437.Pad(Cp437.Encode(entry.Name), 8));
                result += 16;
            }

            return result;
        }
    }
}