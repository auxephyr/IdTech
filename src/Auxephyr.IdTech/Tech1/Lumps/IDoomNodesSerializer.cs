using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomNodesSerializer
    {
        List<DoomNode> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomNode> nodes);
    }
}