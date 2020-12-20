using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    public class DoomRejectSerializer : IDoomRejectSerializer
    {
        public static IDoomRejectSerializer Default { get; } = new DoomRejectSerializer();
        
        public List<DoomReject> Decode(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Encode(IEnumerable<DoomReject> nodes)
        {
            throw new System.NotImplementedException();
        }
    }
}