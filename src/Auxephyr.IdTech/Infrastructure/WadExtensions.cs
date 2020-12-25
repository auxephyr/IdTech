using System;
using System.Collections.Generic;
using System.Linq;
using Auxephyr.IdTech.Tech1;
using Auxephyr.IdTech.Tech1.Modes;

namespace Auxephyr.IdTech.Infrastructure
{
    public static class WadExtensions
    {
        public static List<Lump> ReadLumps(this IWad wad, params string[] names) => 
            ReadLumps(wad, names.AsEnumerable());

        public static List<Lump> ReadLumps(this IWad wad, IEnumerable<string> names)
        {
            var validNames = names.Select(n => n.ToUpper()).ToArray();

            return wad.Lumps
                .Where(l => l?.Name != null && validNames.Any(n => n == l.Name))
                .ToList();
        }
        
        public static List<Lump> ReadLumpGroup(this IWad wad, IEnumerable<string> names)
        {
            var validNames = names.Select(n => n.ToUpper()).ToArray();

            var existingLumps = wad.Lumps
                .Where(l => l?.Name != null)
                .SkipWhile(l => l.Name.ToUpper() != validNames[0])
                .TakeWhile(l => validNames.Contains(l.Name.ToUpper()))
                .ToList();

            return validNames
                .Select(name => existingLumps.FirstOrDefault(l => l.Name.ToUpper() == name)
                                ?? new Lump {Name = name, Data = Array.Empty<byte>()})
                .ToList();
        }
        
        public static void WriteLumps(this IWad wad, params Lump[] lumps) =>
            WriteLumps(wad, (ICollection<Lump>) lumps);
        
        public static void WriteLumps(this IWad wad, ICollection<Lump> lumps)
        {
            var validNames = lumps.Select(l => l.Name?.ToUpper()).Where(n => n != null).ToArray();
            var existingLumps = wad.Lumps
                .Where(l => l?.Name != null && validNames.Contains(l.Name.ToUpper()))
                .ToList();
            InsertLumps(wad, existingLumps, lumps);
        }

        public static void WriteLumpGroup(this IWad wad, ICollection<Lump> lumps)
        {
            var validNames = lumps.Select(l => l.Name?.ToUpper()).Where(n => n != null).ToArray();
            var existingLumps = lumps
                .SkipWhile(l => l.Name != validNames[0])
                .TakeWhile(l => validNames.Contains(l.Name))
                .ToList();
            InsertLumps(wad, existingLumps, lumps);
        }

        private static void InsertLumps(IWad wad, ICollection<Lump> existingLumps, IEnumerable<Lump> lumps)
        {
            Lump prevLump = default;
            
            if (existingLumps.Count > 0)
            {
                // Group already exists somewhere in the archive.

                foreach (var lump in lumps)
                {
                    var existingLump = existingLumps.FirstOrDefault(l => l.Name == lump.Name);
                    if (existingLump != default)
                    {
                        prevLump = existingLump;
                        existingLump.Data = lump.Data.ToArray();
                    }
                    else if (prevLump != default)
                    {
                        // When a lump is missing, insert it in the order in which it belongs.

                        var index = wad.Lumps.IndexOf(prevLump) + 1;
                        wad.Lumps.Insert(index, new Lump {Name = lump.Name, Data = lump.Data.ToArray()});
                    }
                }
            }
            else
            {
                // Group does not exist.

                foreach (var mapLump in lumps)
                    wad.Lumps.Add(new Lump {Name = mapLump.Name, Data = mapLump.Data.ToArray()});
            }
        }

        public static DoomWad AsDoom(this IWad wad)
        {
            return new(wad);
        }
    }
}