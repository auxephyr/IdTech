using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1
{
    [TestFixture]
    public class WadEntrySerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void ReadStream_ShouldReadEntriesCorrectly()
        {
            // Arrange.
            var subject = Create<WadEntrySerializer>();

            var stream = new MemoryStream(new byte[]
            {
                0x74, 0xBB, 0x16, 0x01, 0x68, 0x01, 0x00, 0x00,
                0x54, 0x57, 0x41, 0x56, 0x43, 0x30, 0x00, 0x00,
                0xDC, 0xBC, 0x16, 0x01, 0xE8, 0x03, 0x00, 0x00,
                0x54, 0x48, 0x49, 0x54, 0x41, 0x30, 0x00, 0x00,
                0xC4, 0xC0, 0x16, 0x01, 0xD4, 0x07, 0x00, 0x00,
                0x54, 0x48, 0x49, 0x54, 0x42, 0x30, 0x00, 0x00
            });

            var expected = new List<WadEntry>
            {
                new() {Name = "TWAVC0", Offset = 0x0116BB74, Length = 0x00000168},
                new() {Name = "THITA0", Offset = 0x0116BCDC, Length = 0x000003E8},
                new() {Name = "THITB0", Offset = 0x0116C0C4, Length = 0x000007D4}
            };

            // Act.
            var observed = subject.ReadStream(stream, 3);

            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }
    }
}