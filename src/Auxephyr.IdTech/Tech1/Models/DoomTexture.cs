using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomTexture
    {
        public string Name { get; set; }
        public short Reserved0 { get; set; }
        public short Reserved1 { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public short Reserved4 { get; set; }
        public short Reserved5 { get; set; }
        public List<DoomPatch> Patches { get; set; }
    }
}