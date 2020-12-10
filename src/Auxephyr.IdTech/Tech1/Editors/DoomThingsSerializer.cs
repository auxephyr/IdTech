using System.Collections.Generic;
using System.IO;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Editors
{
    [Service]
    public class DoomThingsSerializer : IDoomThingsSerializer
    {
        public List<DoomThing> Read(byte[] data)
        {
            using var reader = new BinaryReader(new MemoryStream(data));
            var result = new List<DoomThing>(data.Length / 10);

            for (var i = 0; i < data.Length; i++)
            {
                result[i] = new DoomThing
                {
                    X = reader.ReadInt16(),
                    Y = reader.ReadInt16(),
                    Angle = reader.ReadInt16(),
                    Type = reader.ReadInt16(),
                    Options = (DoomThingOptions) reader.ReadInt16()
                };
            }

            return result;
        }

        public byte[] Write(IEnumerable<DoomThing> things)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IDoomThingsSerializer
    {
        List<DoomThing> Read(byte[] data);
        byte[] Write(IEnumerable<DoomThing> things);
    }
}