using System;
using System.Collections.Generic;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
using Auxephyr.IdTech.Tech1.Lumps;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Modes
{
    public class DoomWad : IWad
    {
        private readonly IWad _wad;
        private readonly IDoomTextureSerializer _doomTextureSerializer;
        private readonly IDoomMapSerializer _doomMapSerializer;

        public DoomWad(IWad wad)
        {
            _wad = wad;
            _doomTextureSerializer = DoomTextureSerializer.Default;
            _doomMapSerializer = DoomMapSerializer.Default;
        }

        public Dictionary<string, List<DoomTexture>> LoadTextures()
        {
            return _wad.ReadLumps(_doomTextureSerializer.GetTextureLumpNames(_wad.Lumps))
                .ToDictionary(l => l.Name, l => _doomTextureSerializer.Decode(l.Data));
        }

        public void SaveTextures(Dictionary<string, List<DoomTexture>> textureSets)
        {
            _wad.WriteLumps(textureSets
                .Select(ts => new Lump {Name = ts.Key?.ToUpper(), Data = _doomTextureSerializer.Encode(ts.Value)})
                .ToList());
        }

        public List<DoomMap> LoadMaps()
        {
            return _doomMapSerializer.GetMapLumpNames(_wad.Lumps)
                .Select(ln => _doomMapSerializer.Decode(_wad.ReadLumpGroup(_doomMapSerializer.GetLumpNames(ln))))
                .ToList();
        }

        public void SaveMaps(List<DoomMap> maps)
        {
            throw new NotImplementedException();
        }

        public List<Lump> Lumps
        {
            get => _wad.Lumps;
            set => _wad.Lumps = value;
        }

        public WadType Type
        {
            get => _wad.Type;
            set => _wad.Type = value;
        }
    }
}