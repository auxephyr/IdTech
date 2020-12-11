using System;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Flags]
    public enum DoomThingFlags : short
    {
        Skill1And2 = 0x1,
        Skill3 = 0x2,
        Skill4And5 = 0x4,
        Ambush = 0x8,
        MultiplayerOnly = 0x10
    }
}