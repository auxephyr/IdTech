using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomRejectSerializer
    {
        List<DoomReject> Decode(ReadOnlySpan<byte> data, int sectorCount);
        byte[] Encode(ICollection<DoomReject> rejects, int sectorCount);
    }
}