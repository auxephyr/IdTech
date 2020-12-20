using System.Collections.Generic;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [Service]
    public class DoomMapSerializer : IDoomMapSerializer
    {
        private readonly IDoomBlockmapSerializer _doomBlockmapSerializer;
        private readonly IMapGroupSerializer _mapGroupSerializer;
        private readonly IDoomLinedefsSerializer _doomLinedefsSerializer;
        private readonly IDoomThingsSerializer _doomThingsSerializer;
        private readonly IDoomSidedefsSerializer _doomSidedefsSerializer;
        private readonly IDoomVertexesSerializer _doomVertexesSerializer;
        private readonly IDoomSegsSerializer _doomSegsSerializer;
        private readonly IDoomSsectorsSerializer _doomSsectorsSerializer;
        private readonly IDoomNodesSerializer _doomNodesSerializer;
        private readonly IDoomSectorsSerializer _doomSectorsSerializer;
        private readonly IDoomRejectSerializer _doomRejectSerializer;

        public static IDoomMapSerializer Default { get; } = new DoomMapSerializer(
            DoomThingsSerializer.Default,
            DoomLinedefsSerializer.Default,
            DoomSidedefsSerializer.Default,
            DoomVertexesSerializer.Default,
            DoomSegsSerializer.Default,
            DoomSsectorsSerializer.Default,
            DoomNodesSerializer.Default,
            DoomSectorsSerializer.Default,
            DoomRejectSerializer.Default,
            DoomBlockmapSerializer.Default,
            MapGroupSerializer.Default
        );

        private static string[] MapLumpNames =
        {
            LumpNames.Things,
            LumpNames.Linedefs,
            LumpNames.Sidedefs,
            LumpNames.Vertexes,
            LumpNames.Segs,
            LumpNames.Ssectors,
            LumpNames.Nodes,
            LumpNames.Sectors,
            LumpNames.Reject,
            LumpNames.Blockmap
        };

        public DoomMapSerializer(
            IDoomThingsSerializer doomThingsSerializer,
            IDoomLinedefsSerializer doomLinedefsSerializer,
            IDoomSidedefsSerializer doomSidedefsSerializer,
            IDoomVertexesSerializer doomVertexesSerializer,
            IDoomSegsSerializer doomSegsSerializer,
            IDoomSsectorsSerializer doomSsectorsSerializer,
            IDoomNodesSerializer doomNodesSerializer,
            IDoomSectorsSerializer doomSectorsSerializer,
            IDoomRejectSerializer doomRejectSerializer,
            IDoomBlockmapSerializer doomBlockmapSerializer,
            IMapGroupSerializer mapGroupSerializer)
        {
            _doomBlockmapSerializer = doomBlockmapSerializer;
            _mapGroupSerializer = mapGroupSerializer;
            _doomLinedefsSerializer = doomLinedefsSerializer;
            _doomThingsSerializer = doomThingsSerializer;
            _doomSidedefsSerializer = doomSidedefsSerializer;
            _doomVertexesSerializer = doomVertexesSerializer;
            _doomSegsSerializer = doomSegsSerializer;
            _doomSsectorsSerializer = doomSsectorsSerializer;
            _doomNodesSerializer = doomNodesSerializer;
            _doomSectorsSerializer = doomSectorsSerializer;
            _doomRejectSerializer = doomRejectSerializer;
        }

        public List<string> GetLumpNames(string map)
        {
            return new()
            {
                map,
                LumpNames.Things,
                LumpNames.Linedefs,
                LumpNames.Sidedefs,
                LumpNames.Vertexes,
                LumpNames.Segs,
                LumpNames.Ssectors,
                LumpNames.Nodes,
                LumpNames.Sectors,
                LumpNames.Reject,
                LumpNames.Blockmap
            };
        }

        public List<string> GetAllMapNames(IEnumerable<Lump> lumps)
        {
            return lumps.Select(l => l.Name).Where(IsMapName)
                .ToList();
        }

        private static bool IsMapName(string n)
        {
            return (n.Length == 4 &&
                    n[0] == 'E' &&
                    n[2] == 'M' &&
                    char.IsDigit(n[1]) &&
                    char.IsDigit(n[3])) ||
                   (n.Length == 5 &&
                    n[0] == 'M' &&
                    n[1] == 'A' &&
                    n[2] == 'P' &&
                    char.IsDigit(n[3]) &&
                    char.IsDigit(n[4]));
        }

        public DoomMap Decode(IEnumerable<Lump> lumps)
        {
            var lumpList = lumps.ToList();
            var idLump = lumpList.FirstOrDefault(l => IsMapName(l?.Name));
            var thingsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Things);
            var linedefsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Linedefs);
            var sidedefsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Sidedefs);
            var verticesLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Vertexes);
            var segsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Segs);
            var ssectorsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Ssectors);
            var nodesLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Nodes);
            var sectorsLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Sectors);
            var rejectLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Reject);
            var blockMapLump = lumpList.FirstOrDefault(l => l?.Name == LumpNames.Blockmap);

            return new DoomMap
            {
                Id = idLump?.Clone(),
                Things = thingsLump?.Data == default ? default : _doomThingsSerializer.Decode(thingsLump.Data),
                Linedefs = linedefsLump?.Data == default ? default : _doomLinedefsSerializer.Decode(linedefsLump.Data),
                Sidedefs = sidedefsLump?.Data == default ? default : _doomSidedefsSerializer.Decode(sidedefsLump.Data),
                Vertices = verticesLump.Data == default ? default : _doomVertexesSerializer.Decode(verticesLump.Data),
                Segs = segsLump.Data == default ? default : _doomSegsSerializer.Decode(segsLump.Data),
                SubSectors = ssectorsLump == default ? default : _doomSsectorsSerializer.Decode(ssectorsLump.Data),
                Nodes = nodesLump == default ? default : _doomNodesSerializer.Decode(nodesLump.Data),
                Sectors = sectorsLump?.Data == default ? default : _doomSectorsSerializer.Decode(sectorsLump.Data),
                Rejects = rejectLump?.Data == default ? default : _doomRejectSerializer.Decode(rejectLump.Data),
                Blocks = blockMapLump?.Data == default ? default : _doomBlockmapSerializer.Decode(blockMapLump.Data)
            };
        }

        public List<Lump> Encode(DoomMap map)
        {
            var result = new List<Lump>();

            if (map.Id != null)
                result.Add(map.Id.Clone());
            if (map.Things != null)
                result.Add(new Lump {Name = LumpNames.Things, Data = _doomThingsSerializer.Encode(map.Things)});
            if (map.Linedefs != null)
                result.Add(new Lump {Name = LumpNames.Linedefs, Data = _doomLinedefsSerializer.Encode(map.Linedefs)});
            if (map.Sidedefs != null)
                result.Add(new Lump {Name = LumpNames.Sidedefs, Data = _doomSidedefsSerializer.Encode(map.Sidedefs)});
            if (map.Vertices != null)
                result.Add(new Lump {Name = LumpNames.Vertexes, Data = _doomVertexesSerializer.Encode(map.Vertices)});
            if (map.Segs != null)
                result.Add(new Lump {Name = LumpNames.Segs, Data = _doomSegsSerializer.Encode(map.Segs)});
            if (map.SubSectors != null)
                result.Add(new Lump {Name = LumpNames.Ssectors, Data = _doomSsectorsSerializer.Encode(map.SubSectors)});
            if (map.Nodes != null)
                result.Add(new Lump {Name = LumpNames.Nodes, Data = _doomNodesSerializer.Encode(map.Nodes)});
            if (map.Sectors != null)
                result.Add(new Lump {Name = LumpNames.Sectors, Data = _doomSectorsSerializer.Encode(map.Sectors)});
            if (map.Rejects != null)
                result.Add(new Lump {Name = LumpNames.Reject, Data = _doomRejectSerializer.Encode(map.Rejects)});
            if (map.Blocks != null)
                result.Add(new Lump {Name = LumpNames.Blockmap, Data = _doomBlockmapSerializer.Encode(map.Blocks)});

            return result;
        }
    }
}