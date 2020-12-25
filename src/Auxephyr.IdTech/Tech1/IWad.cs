using System.Collections.Generic;

namespace Auxephyr.IdTech.Tech1
{
    public interface IWad
    {
        List<Lump> Lumps { get; set; }
        WadType Type { get; set; }
    }
}