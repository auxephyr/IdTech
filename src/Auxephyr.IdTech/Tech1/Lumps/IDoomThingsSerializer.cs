using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomThingsSerializer
    {
        List<DoomThing> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomThing> things);
    }
}