using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomLinedefsSerializer
    {
        List<DoomLinedef> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomLinedef> linedefs);
    }
}