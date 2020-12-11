using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class UdmfSidedef
    {
        public int OffsetX;
        public int OffsetY;
        public string TextureTop;
        public string TextureBottom;
        public string TextureMiddle;
        public int Sector;
        public string Comment;
    }
}