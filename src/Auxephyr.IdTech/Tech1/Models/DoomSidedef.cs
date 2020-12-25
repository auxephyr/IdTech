using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomSidedef
    {
        public short X;
        public short Y;
        public string UpperTexture;
        public string LowerTexture;
        public string MiddleTexture;
        public short Sector;
    }
}