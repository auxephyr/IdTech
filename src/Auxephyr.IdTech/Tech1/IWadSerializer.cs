using System.IO;

namespace Auxephyr.IdTech.Tech1
{
    public interface IWadSerializer
    {
        Wad ReadFile(string fileName);
        Wad ReadStream(Stream stream);
        void WriteFile(string fileName, Wad wad);
        int WriteStream(Stream stream, Wad wad);
    }
}