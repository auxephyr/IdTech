using System.Collections.Generic;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [Service]
    public class DoomMapSerializer : IDoomMapSerializer
    {
        private readonly IMapGroupSerializer _mapGroupSerializer;
        private readonly IDoomBlockMapSerializer _doomBlockMapSerializer;

        public static IDoomMapSerializer Default { get; } = new DoomMapSerializer(
            MapGroupSerializer.Default,
            DoomBlockMapSerializer.Default);

        private static readonly string[] LumpNames =
        {
            "THINGS",
            "LINEDEFS",
            "SIDEDEFS",
            "VERTEXES",
            "SEGS",
            "SSECTORS",
            "NODES",
            "SECTORS",
            "REJECT",
            "BLOCKMAP"
        };

        public DoomMapSerializer(IMapGroupSerializer mapGroupSerializer, IDoomBlockMapSerializer doomBlockMapSerializer)
        {
            _mapGroupSerializer = mapGroupSerializer;
            _doomBlockMapSerializer = doomBlockMapSerializer;
        }

        public List<string> GetAllMapNames(IEnumerable<Lump> lumps)
        {
            return lumps.Select(l => l.Name).Where(n =>
                    (n.Length == 4 &&
                     n[0] == 'E' &&
                     n[2] == 'M' &&
                     char.IsDigit(n[1]) &&
                     char.IsDigit(n[3])) ||
                    (n.Length == 5 &&
                     n[0] == 'M' &&
                     n[1] == 'A' &&
                     n[2] == 'P' &&
                     char.IsDigit(n[3]) &&
                     char.IsDigit(n[4])))
                .ToList();
        }

        public DoomMap Read(IEnumerable<Lump> lumps, string name)
        {
            var mapLumps = _mapGroupSerializer.Read(lumps, new[] {name}.Concat(LumpNames));

            var blockMapLump = mapLumps.First(l => l.Name == "BLOCKMAP");
            
            return new DoomMap
            {
                BlockMap = blockMapLump?.Data == default ? default : _doomBlockMapSerializer.Decode(blockMapLump.Data)
            };
        }

        public void Write(IEnumerable<Lump> lumps, string name)
        {
        }
    }
}