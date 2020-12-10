using System.IO;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class WadEntry
    {
        public static WadEntry ReadStream(Stream stream)
        {
            var reader = new BinaryReader(stream);
            return new WadEntry
            {
                Offset = reader.ReadInt32(),
                Length = reader.ReadInt32(),
                Name = Cp437.Decode(Cp437.UnPad(reader.ReadBytes(8)))
            };
        }

        public int WriteStream(Stream stream)
        {
            var writer = new BinaryWriter(stream);
            writer.Write(Offset);
            writer.Write(Length);
            writer.Write(Cp437.Pad(Cp437.Encode(Name), 8));
            return 16;
        }
        
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}