using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomSegsSerializer
    {
        List<DoomSeg> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomSeg> segs);
    }
}