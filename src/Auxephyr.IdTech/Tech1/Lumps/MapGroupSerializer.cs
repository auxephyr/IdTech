using System;
using System.Collections.Generic;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [Service]
    public class MapGroupSerializer : IMapGroupSerializer
    {
        public static IMapGroupSerializer Default { get; } = new MapGroupSerializer();
        
        public List<Lump> Read(IEnumerable<Lump> lumps, IEnumerable<string> names)
        {
            var validNames = names.ToArray();

            var existingLumps = lumps
                .SkipWhile(l => l.Name != validNames[0])
                .TakeWhile(l => validNames.Contains(l.Name))
                .ToList();

            return validNames
                .Select(name => existingLumps.FirstOrDefault(l => l.Name == name)
                                ?? new Lump {Name = name, Data = Array.Empty<byte>()})
                .ToList();
        }

        public void Write(IList<Lump> lumps, IEnumerable<Lump> map)
        {
            var mapLumps = map.ToArray();
            var validNames = mapLumps.Select(l => l.Name).ToArray();
            Lump prevLump = default;

            var existingLumps = lumps
                .SkipWhile(l => l.Name != validNames[0])
                .TakeWhile(l => validNames.Contains(l.Name))
                .ToList();

            if (existingLumps.Count > 0)
            {
                // Map already exists somewhere in the archive.

                foreach (var mapLump in mapLumps)
                {
                    var existingLump = existingLumps.FirstOrDefault(l => l.Name == mapLump.Name);
                    if (existingLump != default)
                    {
                        prevLump = existingLump;
                        existingLump.Data = mapLump.Data.ToArray();
                    }
                    else if (prevLump != default)
                    {
                        // When a lump is missing, insert it in the order in which it belongs.

                        var index = lumps.IndexOf(prevLump) + 1;
                        lumps.Insert(index, new Lump {Name = mapLump.Name, Data = mapLump.Data.ToArray()});
                    }
                }
            }
            else
            {
                // Map does not exist.

                foreach (var mapLump in mapLumps)
                    lumps.Add(new Lump {Name = mapLump.Name, Data = mapLump.Data.ToArray()});
            }
        }
    }

    public interface IMapGroupSerializer
    {
        public List<Lump> Read(IEnumerable<Lump> lumps, IEnumerable<string> names);
        public void Write(IList<Lump> lumps, IEnumerable<Lump> map);
    }
}