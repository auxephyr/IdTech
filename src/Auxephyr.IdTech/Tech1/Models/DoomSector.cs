using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomSector
    {
        public short FloorHeight;
        public short CeilingHeight;
        public string FloorTexture;
        public string CeilingTexture;
        public short LightLevel;
        public DoomSectorSpecial Special;
        public short Tag;
    }
}