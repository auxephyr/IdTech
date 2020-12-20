using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomNodesSerializer
    {
        List<DoomNode> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomNode> nodes);
    }
}