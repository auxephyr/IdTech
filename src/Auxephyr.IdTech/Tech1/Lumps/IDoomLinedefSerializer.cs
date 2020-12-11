using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomLinedefSerializer
    {
        List<DoomLinedef> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomLinedef> linedefs);
    }
}