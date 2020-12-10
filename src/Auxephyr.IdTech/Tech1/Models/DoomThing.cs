using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public class DoomThing
    {
        public short X;
        public short Y;
        public short Angle;
        public short Type;
        public DoomThingOptions Options;

        public bool PresentOnSkill1And2
        {
            get => (Options & DoomThingOptions.PresentOnSkill1And2) != 0;
            set => Options = value
                ? Options & ~DoomThingOptions.PresentOnSkill1And2
                : Options | DoomThingOptions.PresentOnSkill1And2;
        }

        public bool PresentOnSkill3
        {
            get => (Options & DoomThingOptions.PresentOnSkill3) != 0;
            set => Options = value
                ? Options & ~DoomThingOptions.PresentOnSkill3
                : Options | DoomThingOptions.PresentOnSkill3;
        }

        public bool PresentOnSkill4And5
        {
            get => (Options & DoomThingOptions.PresentOnSkill4And5) != 0;
            set => Options = value
                ? Options & ~DoomThingOptions.PresentOnSkill4And5
                : Options | DoomThingOptions.PresentOnSkill4And5;
        }

        public bool Ambush
        {
            get => (Options & DoomThingOptions.Ambush) != 0;
            set => Options = value
                ? Options & ~DoomThingOptions.Ambush
                : Options | DoomThingOptions.Ambush;
        }

        public bool MultiplayerOnly
        {
            get => (Options & DoomThingOptions.MultiplayerOnly) != 0;
            set => Options = value
                ? Options & ~DoomThingOptions.MultiplayerOnly
                : Options | DoomThingOptions.MultiplayerOnly;
        }
    }
}