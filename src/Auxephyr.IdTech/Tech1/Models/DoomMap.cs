using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomMap
    {
        public string Name { get; set; }
        public List<DoomThing> Things { get; set; }
        public List<DoomLinedef> Linedefs { get; set; }
        public List<DoomSidedef> Sidedefs { get; set; }
        public List<DoomVertex> Vertices { get; set; }
        public List<DoomSeg> Segs { get; set; }
        public List<DoomSubSector> SubSectors { get; set; }
        public List<DoomNode> Nodes { get; set; }
        public List<DoomSector> Sectors { get; set; }
        public List<DoomReject> Rejects { get; set; }
        public List<DoomBlock> BlockMap { get; set; }
    }
}