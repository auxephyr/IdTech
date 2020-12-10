using System;
using System.IO;

namespace Auxephyr.IdTech.Infrastructure
{
    internal static class StreamExtensions
    {
        public static int ReadBuffered(this Stream stream, byte[] buffer, int offset, int count)
        {
            var totalBytesRead = 0;
            
            while (true)
            {
                var bytesRead = stream.Read(buffer, offset, count);
                if (bytesRead < 1)
                    return totalBytesRead;

                totalBytesRead += bytesRead;
            }
        }

        public static int ReadBuffered(this Stream stream, Span<byte> buffer)
        {
            var totalBytesRead = 0;

            while (true)
            {
                var bytesRead = stream.Read(buffer);
                if (bytesRead < 1)
                    return totalBytesRead;

                totalBytesRead += bytesRead;
                buffer = buffer.Slice(bytesRead);
            }
        }
    }
}