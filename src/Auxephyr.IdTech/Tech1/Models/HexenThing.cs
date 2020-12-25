using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public struct HexenThing
    {
        public short Id;
        public short X;
        public short Y;
        public short Z;
        public short Angle;
        public short Type;
        public HexenThingFlags Flags;
        public HexenActionSpecial ActionSpecial;
        public byte Arg1;
        public byte Arg2;
        public byte Arg3;
        public byte Arg4;
        public byte Arg5;
    }
}