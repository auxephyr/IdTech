using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomRejectSerializer
    {
        List<DoomReject> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomReject> nodes);
    }
}