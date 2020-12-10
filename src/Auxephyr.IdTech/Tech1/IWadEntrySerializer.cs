using System.Collections.Generic;
using System.IO;

namespace Auxephyr.IdTech.Tech1
{
    public interface IWadEntrySerializer
    {
        List<WadEntry> ReadStream(Stream stream, int count);
        int WriteStream(Stream stream, IEnumerable<WadEntry> entries);
    }
}