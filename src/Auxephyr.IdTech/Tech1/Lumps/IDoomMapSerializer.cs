using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomMapSerializer
    {
        List<string> GetAllMapNames(IEnumerable<Lump> lumps);
        DoomMap Read(IEnumerable<Lump> lumps, string name);
        void Write(IEnumerable<Lump> lumps, string name);
    }
}