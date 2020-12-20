using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomSectorsSerializer
    {
        List<DoomSector> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomSector> sectors);
    }
}