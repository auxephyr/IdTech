namespace Auxephyr.IdTech.Tech1.Models
{
    public enum HexenThingFlags : short
    {
        /// <summary>
        /// Thing exists in skill levels "Squire"/"Altar Boy"/"Apprentice" and "Knight"/"Acolyte"/"Enchanter"
        /// </summary>
        Skill1And2 = 0x1,

        /// <summary>
        /// Thing exists in skill level "Warrior"/"Priest"/"Sorcerer"
        /// </summary>
        Skill3 = 0x2,

        /// <summary>
        /// Thing exists in skill levels "Berserker"/"Cardinal"/"Warlock" and "Titan"/"Pope"/"Archmage"
        /// </summary>
        Skill4And5 = 0x4,

        /// <summary>
        /// Thing will stay in place.
        /// </summary>
        Stationary = 0x8,

        /// <summary>
        /// Thing is not available in single player mode.
        /// </summary>
        NotSinglePlayer = 0x10,

        /// <summary>
        /// Thing will not react to sound.
        /// </summary>
        Ambush = 0x20,

        /// <summary>
        /// Thing is not hostile toward the player.
        /// </summary>
        Friendly = 0x80,

        /// <summary>
        /// Thing is 25% translucent.
        /// </summary>
        Translucent = 0x100,

        /// <summary>
        /// Thing is invisible.
        /// </summary>
        Invisible = 0x200
    }
}