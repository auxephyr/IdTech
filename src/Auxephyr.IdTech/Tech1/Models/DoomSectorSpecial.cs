// ReSharper disable UnusedMember.Global

namespace Auxephyr.IdTech.Tech1.Models
{
    public enum DoomSectorSpecial : short
    {
        None,
        LightBlinksRandomly,
        LightBlinksEveryHalfSecond,
        LightBlinksEverySecond,
        Damage10To20HealthAndLightBlinksEveryHalfSecond,
        Damage5To10Health,
        Damage2To5Health = 7,
        LightPulsesSmoothly,
        Secret,
        Wait30SecondsThenCloseDoor,
        Damage10To20HealthAndEndLevelAtLowHealth,
        LightBlinksEverySecondSynchronized,
        LightBlinksEveryHalfSecondSynchronized,
        Wait5MinutesThenOpenAndWaitThenClose,
        Damage10To20Health = 16,
        LightFlickers
    }
}