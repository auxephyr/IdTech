using System;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [Service]
    public class DoomLumpSerializer : IDoomLumpSerializer
    {
        public static IDoomLumpSerializer Default { get; } =
            new DoomLumpSerializer(
                DoomLinedefsSerializer.Default,
                DoomBlockmapSerializer.Default);

        public DoomLumpSerializer(IDoomLinedefsSerializer linedefs, IDoomBlockmapSerializer blockmap)
        {
            Linedefs = linedefs;
            Blockmap = blockmap;
        }

        public IDoomLinedefsSerializer Linedefs { get; }
        public IDoomBlockmapSerializer Blockmap { get; }
    }
}