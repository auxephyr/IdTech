using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class HexenLinedef
    {
        public short StartVertex;
        public short EndVertex;
        public HexenLinedefFlags Flags;
        public HexenActionSpecial ActionSpecial;
        public byte Arg1;
        public byte Arg2;
        public byte Arg3;
        public byte Arg4;
        public byte Arg5;
        public short RightSidedef;
        public short LeftSidedef;

        public HexenLinedefActivation Activation
        {
            get => (HexenLinedefActivation) (((int) Flags & 0x1C00) >> 10);
            set => Flags = (HexenLinedefFlags) (((int) Flags & ~0x1C00) | (((int) value & 0x7) << 10));
        }
    }
}