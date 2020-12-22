using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomBlockMapSerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void Decode_ShouldDecodeBlockMap()
        {
            // Arrange.
            var subject = Create<DoomBlockmapSerializer>();
            var data = new byte[]
            {
                0x00, 0x01,
                0x02, 0x03,
                0x04, 0x05,
                0x06, 0x07,

                0x08, 0x09,
                0x0A, 0x0B,
                0x0C, 0x0D,
                0x0E, 0x0F,

                0x00
            };
            var expected = new List<DoomBlock>
            {
                new() {X = 0x0100, Y = 0x0302, Width = 0x0504, Height = 0x0706},
                new() {X = 0x0908, Y = 0x0B0A, Width = 0x0D0C, Height = 0x0F0E}
            };
            
            // Act.
            var observed = subject.Decode(data);
            
            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Encode_ShouldEncodeBlockMap()
        {
            // Arrange.
            var subject = Create<DoomBlockmapSerializer>();
            var blocks = new List<DoomBlock>
            {
                new() {X = 0x0100, Y = 0x0302, Width = 0x0504, Height = 0x0706},
                new() {X = 0x0908, Y = 0x0B0A, Width = 0x0D0C, Height = 0x0F0E}
            };
            var expected = new byte[]
            {
                0x00, 0x01,
                0x02, 0x03,
                0x04, 0x05,
                0x06, 0x07,

                0x08, 0x09,
                0x0A, 0x0B,
                0x0C, 0x0D,
                0x0E, 0x0F
            };
            
            // Act.
            var observed = subject.Encode(blocks);
            
            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }
    }
}