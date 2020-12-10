using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class Wad
    {
        public List<Lump> Lumps { get; set; }
        public WadType Type { get; set; }
    }
}