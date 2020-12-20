using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomLinedefSerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void Decode_ShouldDecodeLinedefs()
        {
            // Arrange.
            var subject = Create<DoomLinedefsSerializer>();
            var data = new byte[]
            {
                0x00, 0x01, 
                0x02, 0x03, 
                0x05, 0x00,
                0x03, 0x00,
                0x05, 0x06,
                0x07, 0x08,
                0x09, 0x0A,
                
                0x0B, 0x0C, 
                0x0D, 0x0E, 
                0x0A, 0x00,
                0x04, 0x00,
                0x0F, 0x10,
                0x11, 0x12,
                0x13, 0x14,
                
                0x00
            };
            var expected = new List<DoomLinedef>
            {
                new DoomLinedef
                {
                    StartVertex = 0x0100,
                    EndVertex = 0x0302,
                    Flags = DoomLinedefFlags.Impassible | DoomLinedefFlags.TwoSided,
                    Special = DoomLinedefSpecial.W1_CloseDoor,
                    Tag = 0x0605,
                    RightSidedef = 0x0807,
                    LeftSidedef = 0x0A09
                },
                new DoomLinedef
                {
                    StartVertex = 0x0C0B,
                    EndVertex = 0x0E0D,
                    Flags = DoomLinedefFlags.BlockMonsters | DoomLinedefFlags.UpperUnpegged,
                    Special = DoomLinedefSpecial.W1_OpenDoorAndWaitThenClose,
                    Tag = 0x100F,
                    RightSidedef = 0x1211,
                    LeftSidedef = 0x1413
                },
            };
            
            // Act.
            var observed = subject.Decode(data);
            
            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Encode_ShouldEncodeLinedefs()
        {
            
        }
    }
}