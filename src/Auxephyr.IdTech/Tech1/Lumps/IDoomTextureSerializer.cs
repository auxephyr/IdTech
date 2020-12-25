using System;
using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public interface IDoomTextureSerializer
    {
        List<string> GetTextureLumpNames(IEnumerable<Lump> lumps);
        List<DoomTexture> Decode(ReadOnlySpan<byte> data);
        byte[] Encode(ICollection<DoomTexture> textures);
    }
}