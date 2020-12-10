using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class Lump
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}