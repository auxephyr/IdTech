using System.Collections.Generic;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IMapGroupSerializer
    {
        public List<Lump> Read(IEnumerable<Lump> lumps, IEnumerable<string> names);
        public void Write(IList<Lump> lumps, IEnumerable<Lump> map);
    }
}