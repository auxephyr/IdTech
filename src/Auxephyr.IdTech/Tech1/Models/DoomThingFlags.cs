using System;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Flags]
    public enum DoomThingFlags : short
    {
        /// <summary>
        /// Thing exists in skill levels "I'm too young to die" and "Hey, not too rough"
        /// </summary>
        Skill1And2 = 0x1,
        
        /// <summary>
        /// Thing exists in skill level "Hurt me plenty"
        /// </summary>
        Skill3 = 0x2,
        
        /// <summary>
        /// Thing exists in skill levels "Ultra-Violence" and "Nightmare"
        /// </summary>
        Skill4And5 = 0x4,
        
        /// <summary>
        /// Thing will not react to sound.
        /// </summary>
        Ambush = 0x8,
        
        /// <summary>
        /// Thing is not available in single player mode.
        /// </summary>
        NotSinglePlayer = 0x10,
        
        /// <summary>
        /// Thing is not available in multiplayer deathmatch mode. BOOM and MBF only.
        /// </summary>
        NotDeathmatch = 0x20,
        
        /// <summary>
        /// Thing is not available in multiplayer cooperative mode. BOOM and MBF only.
        /// </summary>
        NotCooperative = 0x40,
        
        /// <summary>
        /// Thing is not hostile toward the player. MBF only.
        /// </summary>
        Friendly = 0x80
    }
}