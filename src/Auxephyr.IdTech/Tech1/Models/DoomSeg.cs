using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public struct DoomSeg
    {
        public short StartVertex;
        public short EndVertex;
        public short Angle;
        public short Linedef;
        public short Direction;
        public short Offset;
    }
}