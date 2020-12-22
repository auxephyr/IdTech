using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomBlockmapSerializer
    {
        List<DoomBlock> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomBlock> blocks);
    }
}