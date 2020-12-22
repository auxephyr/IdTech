using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomTextureSerializer : IDoomTextureSerializer
    {
        public static IDoomTextureSerializer Default { get; } = new DoomTextureSerializer();

        public List<DoomTexture> Decode(ReadOnlySpan<byte> data)
        {
            var result = new List<DoomTexture>();
            var offset = 0;
            var maxTextureOffset = data.Length - 20;
            var maxPatchOffset = data.Length - 10;

            while (offset < maxTextureOffset)
            {
                var texture = new DoomTexture
                {
                    Name = Cp437.Decode(Cp437.UnPad(SpanStream.ReadBytes(data, ref offset, 8))),
                    Reserved0 = SpanStream.ReadInt16(data, ref offset),
                    Reserved1 = SpanStream.ReadInt16(data, ref offset),
                    Width = SpanStream.ReadInt16(data, ref offset),
                    Height = SpanStream.ReadInt16(data, ref offset),
                    Reserved4 = SpanStream.ReadInt16(data, ref offset),
                    Reserved5 = SpanStream.ReadInt16(data, ref offset)
                };

                var patchCount = SpanStream.ReadInt16(data, ref offset);
                var patches = new List<DoomPatch>();

                for (var j = 0; j < patchCount && offset < maxPatchOffset; j++)
                {
                    var patch = new DoomPatch
                    {
                        X = SpanStream.ReadInt16(data, ref offset),
                        Y = SpanStream.ReadInt16(data, ref offset),
                        PNameIndex = SpanStream.ReadInt16(data, ref offset),
                        StepDir = SpanStream.ReadInt16(data, ref offset),
                        ColorMap = SpanStream.ReadInt16(data, ref offset)
                    };
                    
                    patches.Add(patch);
                }

                texture.Patches = patches;
                result.Add(texture);
            }

            return result;
        }

        public byte[] Encode(ICollection<DoomTexture> subSectors)
        {
            // var data = new byte[subSectors.Count * 4];
            // var offset = 0;
            //
            // foreach (var subSector in subSectors)
            // {
            //     SpanStream.Write(data, ref offset, subSector.SegCount);
            //     SpanStream.Write(data, ref offset, subSector.StartSeg);
            // }
            //
            // return data;
            throw new NotImplementedException();
        }
    }

    public interface IDoomTextureSerializer
    {
        List<DoomTexture> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomTexture> blocks);
    }
}