using System;
using System.Collections.Generic;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomPnamesSerializer
    {
        List<string> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<string> names);
    }
}