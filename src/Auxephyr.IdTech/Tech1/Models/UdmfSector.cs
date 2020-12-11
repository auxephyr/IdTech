using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class UdmfSector
    {
        public int HeightFloor;
        public int HeightCeiling;
        public string TextureFloor;
        public string TextureCeiling;
        public int LightLevel;
        public int Special;
        public int Id;
        public string Comment;
    }
}