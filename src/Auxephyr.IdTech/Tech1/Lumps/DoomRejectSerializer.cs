using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomRejectSerializer : IDoomRejectSerializer
    {
        public static IDoomRejectSerializer Default { get; } = new DoomRejectSerializer();

        public List<DoomReject> Decode(ReadOnlySpan<byte> data, int sectorCount)
        {
            var result = new List<DoomReject>();
            var buffer = 0;
            var offset = 0;

            for (short monsterSector = 0; monsterSector < sectorCount; monsterSector++)
            {
                for (short playerSector = 0; playerSector < sectorCount; playerSector++)
                {
                    // I use bits 8-15 as a counter, which should be zero once 8 bits are shifted out.
                    if ((buffer & 0xFF00) == 0)
                        buffer = 0xFF00 | SpanStream.ReadByte(data, ref offset);

                    if ((buffer & 0x1) != 0)
                        result.Add(new DoomReject {MonsterSector = monsterSector, PlayerSector = playerSector});

                    buffer >>= 1;
                }
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomReject> rejects, int sectorCount)
        {
            var data = new byte[(sectorCount * sectorCount + 7) >> 3];

            foreach (var reject in rejects)
            {
                var bitIndex = reject.PlayerSector + reject.MonsterSector * sectorCount;
                var byteIndex = bitIndex >> 3;
                data[byteIndex] |= unchecked((byte) (1 << (bitIndex & 0x7)));
            }

            return data;
        }
    }
}