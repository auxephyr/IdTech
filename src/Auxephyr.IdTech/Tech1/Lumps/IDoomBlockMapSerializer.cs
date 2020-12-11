using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomBlockMapSerializer
    {
        List<DoomBlock> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomBlock> blocks);
    }
}