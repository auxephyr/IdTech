using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomSidedefsSerializer
    {
        List<DoomSidedef> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomSidedef> sidedefs);
    }
}