using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class Doom64Vertex
    {
        public Doom64Vertex()
        {
        }

        public Doom64Vertex(short x, short y) : this()
        {
            X = x;
            Y = y;
        }

        public Doom64Vertex(short x, short xfrac, short y, short yfrac) : this(x, y)
        {
            Xfrac = xfrac;
            Yfrac = yfrac;
        }

        public short X;
        public short Y;
        public short Xfrac;
        public short Yfrac;
    }
}