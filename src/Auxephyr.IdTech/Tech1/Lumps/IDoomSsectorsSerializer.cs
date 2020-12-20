using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomSsectorsSerializer
    {
        List<DoomSubSector> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomSubSector> subSectors);
    }
}