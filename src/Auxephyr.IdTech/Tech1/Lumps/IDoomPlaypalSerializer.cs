using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomPlaypalSerializer
    {
        List<DoomPalette> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomPalette> palettes);
    }
}