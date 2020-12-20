using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomVertexesSerializer
    {
        List<DoomVertex> Decode(byte[] data);
        byte[] Encode(IEnumerable<DoomVertex> vertices);
    }
}