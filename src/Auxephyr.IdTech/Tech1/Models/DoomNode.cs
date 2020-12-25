using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomNode
    {
        public short X;
        public short Y;
        public short DeltaX;
        public short DeltaY;
        public short RightYUpperBound;
        public short RightYLowerBound;
        public short RightXLowerBound;
        public short RightXUpperBound;
        public short LeftYUpperBound;
        public short LeftYLowerBound;
        public short LeftXLowerBound;
        public short LeftXUpperBound;
        public short RightChild;
        public short LeftChild;
    }
}