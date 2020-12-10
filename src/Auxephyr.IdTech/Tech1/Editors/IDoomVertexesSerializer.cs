using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Editors
{
    public interface IDoomVertexesSerializer
    {
        List<DoomVertex> Read(byte[] data);
        byte[] Write(IEnumerable<DoomVertex> vertices);
    }
}