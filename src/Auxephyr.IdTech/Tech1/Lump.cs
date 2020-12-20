using System.Linq;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class Lump
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public Lump Clone()
        {
            return new Lump
            {
                Name = Name,
                Data = Data?.ToArray()
            };
        }
    }
}