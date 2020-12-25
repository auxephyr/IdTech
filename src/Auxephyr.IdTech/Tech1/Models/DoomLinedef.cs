using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomLinedef
    {
        public short StartVertex;
        public short EndVertex;
        public DoomLinedefFlags Flags;
        public DoomLinedefSpecial Special;
        public short Tag;
        public short RightSidedef;
        public short LeftSidedef;
    }
}