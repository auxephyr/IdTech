using System;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Flags]
    public enum DoomThingOptions : short
    {
        PresentOnSkill1And2 = 0x1,
        PresentOnSkill3 = 0x2,
        PresentOnSkill4And5 = 0x4,
        Ambush = 0x8,
        MultiplayerOnly = 0x10
    }
}