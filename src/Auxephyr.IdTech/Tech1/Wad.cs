using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [Model]
    public class Wad
    {
        /// <summary>
        /// Read a WAD archive from a file.
        /// </summary>
        public static Wad ReadFile(string fileName)
        {
            Log.Write("Opening file to read WAD archive.");
            Assert.IsNotNull(fileName, "File name must not be null.");
            Assert.That(() => File.Exists(fileName), "File must exist.");
            using var stream = File.OpenRead(fileName);
            return ReadStream(stream);
        }

        /// <summary>
        /// Read a WAD archive from a stream.
        /// </summary>
        public static Wad ReadStream(Stream stream)
        {
            Log.Write("Starting to read WAD archive from stream.");
            Assert.IsNotNull(stream, "Stream must not be null.");
            Assert.That(() => stream.CanRead, "Stream must be readable.");

            var seekable = stream.CanSeek;
            var beginning = seekable ? stream.Position : 0;
            var reader = new BinaryReader(stream);

            // Header section.
            
            var id = (WadType) reader.ReadInt32();
            Assert.IsDefinedIn<WadType>(id, "ID must be either IWAD or PWAD.");
            Log.Write($"WAD type is {id}.");

            var entryCount = reader.ReadInt32();
            Assert.That(() => entryCount >= 0, "Entry count must be non-negative.");

            var directoryOffset = reader.ReadInt32();
            Assert.That(() => directoryOffset >= 0, "Directory offset must be non-negative.");

            // Directory section.
            
            var directoryBufferLength = entryCount * 16;
            var preDirectoryBufferLength = directoryOffset - 12;

            byte[] preDirectoryBuffer = null;
            byte[] directoryBuffer = null;
            Stream directoryStream;
            if (seekable)
            {
                stream.Seek(preDirectoryBufferLength, SeekOrigin.Current);
                directoryStream = stream;
            }
            else
            {
                // For forward-only access of non-seekable streams, we have to buffer the entire archive.
                preDirectoryBuffer = reader.ReadBytes(preDirectoryBufferLength);
                directoryBuffer = reader.ReadBytes(directoryBufferLength);
                directoryStream = new MemoryStream(directoryBuffer);
            }

            var directory = Enumerable.Range(0, entryCount)
                .Select(i => WadEntry.ReadStream(directoryStream))
                .ToList();
            
            if (directoryStream != stream)
                directoryStream.Dispose();

            // Secondary archive section if for whatever reason there's trailing data. A WAD author could
            // put the directory in the middle of the file. I don't know if this works in-engine, but the format
            // doesn't seem to explicitly forbid it.
            
            var archiveSize = entryCount < 1
                ? 12
                : Math.Max(directory.Max(e => e.Offset + e.Length), directoryOffset + directoryBufferLength);

            byte[] postDirectoryBuffer = null;
            var postDirectoryBufferLength = archiveSize - preDirectoryBufferLength - directoryBufferLength;
            if (seekable)
            {
                stream.Seek(postDirectoryBufferLength, SeekOrigin.Current);
            }
            else
            {
                postDirectoryBuffer = reader.ReadBytes(postDirectoryBufferLength);
            }

            // Lump processing section.
            
            var wad = new Wad
            {
                Type = id
            };

            Stream buffer;
            
            if (seekable)
            {
                // Seekable streams are to be used directly.
                buffer = stream;
                stream.Position = beginning;
            }
            else
            {
                // Non-seekable streams (such as ZIP streams) need an in-memory copy so that the source stream
                // is still used forward-only.
                buffer = new MemoryStream(archiveSize);
                var bufferWriter = new BinaryWriter(buffer);
                bufferWriter.Write((int) id);
                bufferWriter.Write(entryCount);
                bufferWriter.Write(directoryOffset);
                bufferWriter.Write(preDirectoryBuffer);
                bufferWriter.Write(directoryBuffer);
                bufferWriter.Write(postDirectoryBuffer);
                bufferWriter.Flush();
            }

            var bufferReader = new BinaryReader(buffer);
            wad.Lumps = new List<Lump>(
                directory.Select(e =>
                {
                    buffer.Position = e.Offset + beginning;
                    return new Lump
                    {
                        Name = e.Name,
                        Data = bufferReader.ReadBytes(e.Length)
                    };
                }));
            
            if (buffer != stream)
                buffer.Dispose();

            Log.Write("Finished reading WAD archive.");

            return wad;
        }

        /// <summary>
        /// Write a WAD archive to a file. This will overwrite a file if it already exists.
        /// </summary>
        public void WriteFile(string fileName)
        {
            using var stream = File.OpenWrite(fileName);
            WriteStream(stream);
            stream.Flush();
        }

        /// <summary>
        /// Write a WAD archive to a stream. A flush is not performed; do this before closing the stream.
        /// </summary>
        public int WriteStream(Stream stream)
        {
            Assert.IsNotNull(stream, "Stream must not be null.");
            Assert.That(() => stream.CanWrite, "Stream must be writeable.");
            Assert.IsDefinedIn<WadType>(Type, "ID must be either IWAD or PWAD.");
            
            var writer = new BinaryWriter(stream);
            writer.Write((int) Type);
            writer.Write(Lumps.Count);
            writer.Write(Lumps.Sum(l => l.Data?.Length ?? 0) + 12);
            
            var directory = new List<WadEntry>();
            var offset = 12;

            foreach (var lump in Lumps)
            {
                var length = lump.Data?.Length ?? 0;
                
                directory.Add(new WadEntry
                {
                    Name = lump.Name,
                    Length = length,
                    Offset = offset
                });
                
                if (lump.Data != null)
                    writer.Write(lump.Data);

                offset += length;
            }

            offset += directory.Sum(entry => entry.WriteStream(stream));

            return offset;
        }

        public List<Lump> Lumps { get; set; }
        public WadType Type { get; set; }
    }
}