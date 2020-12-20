using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomThingsSerializer : IDoomThingsSerializer
    {
        public static IDoomThingsSerializer Default { get; } = new DoomThingsSerializer();
        
        public List<DoomThing> Decode(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var reader = new BinaryReader(stream);
            var count = data.Length / 10;
            var result = new List<DoomThing>();
            
            for (var i = 0; i < count; i++)
            {
                result.Add(new DoomThing
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    Angle = reader.ReadInt16(),
                    Type = reader.ReadInt16(),
                    Flags = (DoomThingFlags) reader.ReadInt16()
                });
            }

            return result;
        }

        public byte[] Encode(IEnumerable<DoomThing> things)
        {
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);

            foreach (var thing in things)
            {
                writer.Write(thing.X);
                writer.Write(thing.Y);
                writer.Write(thing.Angle);
                writer.Write(thing.Type);
                writer.Write((short) thing.Flags);
            }
            
            writer.Flush();
            return stream.ToArray();
        }
    }
}