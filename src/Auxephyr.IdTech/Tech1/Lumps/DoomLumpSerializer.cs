using System;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [Service]
    public class DoomLumpSerializer : IDoomLumpSerializer
    {
        public static IDoomLumpSerializer Default { get; } =
            new DoomLumpSerializer(
                DoomLinedefSerializer.Default,
                DoomBlockMapSerializer.Default);

        public DoomLumpSerializer(IDoomLinedefSerializer linedefs, IDoomBlockMapSerializer blockMap)
        {
            Linedefs = linedefs;
            BlockMap = blockMap;
        }

        public IDoomLinedefSerializer Linedefs { get; }
        public IDoomBlockMapSerializer BlockMap { get; }
    }
}