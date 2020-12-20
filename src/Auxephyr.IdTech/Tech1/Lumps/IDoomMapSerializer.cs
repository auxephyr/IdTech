using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomMapSerializer
    {
        List<string> GetAllMapNames(IEnumerable<Lump> lumps);
        DoomMap Decode(IEnumerable<Lump> lumps);
        List<Lump> Encode(DoomMap map);
        List<string> GetLumpNames(string map);
    }
}