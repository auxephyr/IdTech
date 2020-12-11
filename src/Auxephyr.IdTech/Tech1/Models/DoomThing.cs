﻿using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Models
{
    [Model]
    public struct DoomThing
    {
        public short X;
        public short Y;
        public short Angle;
        public short Type;
        public DoomThingFlags Flags;
    }
}