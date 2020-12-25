using System;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Flags]
    public enum HexenLinedefFlags : short
    {
        Impassible = 0x1,
        BlockMonsters = 0x2,
        TwoSided = 0x4,
        UpperUnpegged = 0x8,
        LowerUnpegged = 0x10,
        Secret = 0x20,
        BlockSound = 0x40,
        NotOnMap = 0x80,
        AlwaysOnMap = 0x100,
        Repeatable = 0x200,
        ActivateByPlayerAndMonster = 0x2000,
        BlockEverything = unchecked((short) 0x8000)
    }
}