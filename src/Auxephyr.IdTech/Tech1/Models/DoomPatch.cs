using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomPatch
    {
        public short X { get; set; }
        public short Y { get; set; }
        public short PNameIndex { get; set; }
        public short StepDir { get; set; }
        public short ColorMap { get; set; }
    }
}