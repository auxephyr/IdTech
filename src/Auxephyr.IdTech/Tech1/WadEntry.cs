using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class WadEntry
    {
        public string Name { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}