﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public byte[] Encode(ICollection<DoomTexture> textures)
        {
            var data = new byte[textures.Sum(t => 22 + t.Patches.Count * 10)];
            var offset = 0;

            foreach (var texture in textures)
            {
                SpanStream.Write(data, ref offset, Cp437.Pad(Cp437.Encode(texture.Name), 8));
                SpanStream.Write(data, ref offset, texture.Reserved0);
                SpanStream.Write(data, ref offset, texture.Reserved1);
                SpanStream.Write(data, ref offset, texture.Width);
                SpanStream.Write(data, ref offset, texture.Height);
                SpanStream.Write(data, ref offset, texture.Reserved4);
                SpanStream.Write(data, ref offset, texture.Reserved5);
                SpanStream.Write(data, ref offset, (short) texture.Patches.Count);

                foreach (var patch in texture.Patches)
                {
                    SpanStream.Write(data, ref offset, patch.X);
                    SpanStream.Write(data, ref offset, patch.Y);
                    SpanStream.Write(data, ref offset, patch.PNameIndex);
                    SpanStream.Write(data, ref offset, patch.StepDir);
                    SpanStream.Write(data, ref offset, patch.ColorMap);
                }
            }

            return data;
        }
    }
}