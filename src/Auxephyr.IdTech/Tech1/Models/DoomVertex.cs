using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public struct DoomVertex
    {
        public DoomVertex(int x, int y)
        {
            X = x;
            Y = y;
        }
        
        public int X;
        public int Y;
    }
}