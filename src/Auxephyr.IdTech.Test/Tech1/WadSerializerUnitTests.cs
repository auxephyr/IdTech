using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1
{
    [TestFixture]
    public class WadSerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void ReadStream_ShouldReadWadCorrectly()
        {
            // Arrange.
            var entrySerializerMock = Mock<IWadEntrySerializer>();
            var subject = Create<WadSerializer>();

            var stream = new MemoryStream(new byte[]
            {
                0x49, 0x57, 0x41, 0x44, // 0x000: header - "IWAD"
                0x02, 0x00, 0x00, 0x00, // 0x004: header - entry count
                0x18, 0x00, 0x00, 0x00, // 0x008: header - directory offset
                0x01, 0x02, 0x03, 0x04, // 0x00C: data - file 1
                0x05, 0x06, 0x07, 0x08, // 0x010: data - file 2
                0x09, 0x0A, 0x0B, 0x0C, // 0x014: data - file 2 (cont)
                0x0C, 0x00, 0x00, 0x00, // 0x018: directory - file 1 offset
                0x04, 0x00, 0x00, 0x00, // 0x01C: directory - file 1 length
                0x41, 0x00, 0x00, 0x00, // 0x020: directory - file 1 name
                0x00, 0x00, 0x00, 0x00, // 0x024: directory - file 1 name (cont)
                0x10, 0x00, 0x00, 0x00, // 0x028: directory - file 2 offset
                0x08, 0x00, 0x00, 0x00, // 0x02C: directory - file 2 length
                0x42, 0x00, 0x00, 0x00, // 0x030: directory - file 2 name
                0x00, 0x00, 0x00, 0x00, // 0x034: directory - file 2 name (cont)
            });

            var directory = new List<WadEntry>
            {
                new WadEntry {Name = "A", Offset = 0x0000000C, Length = 0x00000004},
                new WadEntry {Name = "B", Offset = 0x00000010, Length = 0x00000008}
            };

            entrySerializerMock.Setup(x => x.ReadStream(It.IsAny<Stream>(), It.IsAny<int>()))
                .Returns(directory);

            var expected = new Wad
            {
                Type = WadType.Iwad,
                Lumps = new List<Lump>
                {
                    new Lump {Name = "A", Data = new byte[] {0x01, 0x02, 0x03, 0x04}},
                    new Lump {Name = "B", Data = new byte[] {0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C}}
                }
            };

            // Act.
            var observed = subject.ReadStream(stream);

            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }
    }
}