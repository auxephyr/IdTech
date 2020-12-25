namespace Auxephyr.IdTech.Tech1.Models
{
    public enum HexenLinedefActivation : byte
    {
        None = 0,
        PlayerUse = 1,
        Monster = 2,
        ProjectileHit = 3,
        PlayerProximity = 4,
        ProjectileProximity = 5,
        PlayerUsePassThrough = 6
    }
}