using System.Collections.Generic;
using Auxephyr.IdTech.Tech1.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomTextureSerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void Decode_ShouldDecodeThings()
        {
            // Arrange.
            var subject = Create<DoomTextureSerializer>();
            var data = new byte[]
            {
                // texture 1 "ABC"

                0x41, 0x42, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x01, 0x02,
                0x03, 0x04,
                0x05, 0x06,
                0x07, 0x08,
                0x09, 0x0A,
                0x0B, 0x0C,

                0x02, 0x00,

                // patch 1-1

                0x0D, 0x0E,
                0x0F, 0x10,
                0x11, 0x12,
                0x13, 0x14,
                0x15, 0x16,

                // patch 1-2

                0x17, 0x18,
                0x19, 0x1A,
                0x1B, 0x1C,
                0x1D, 0x1E,
                0x1F, 0x20,

                // texture 2 "DEFGHIJK"

                0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B,
                0x21, 0x22,
                0x23, 0x24,
                0x25, 0x26,
                0x27, 0x28,
                0x29, 0x2A,
                0x2B, 0x2C,

                0x02, 0x00,

                // patch 2-1

                0x2D, 0x2E,
                0x2F, 0x30,
                0x31, 0x32,
                0x33, 0x34,
                0x35, 0x36,

                // patch 2-2

                0x37, 0x38,
                0x39, 0x3A,
                0x3B, 0x3C,
                0x3D, 0x3E,
                0x3F, 0x40,

                // padding for test - not required but it will ideally be ignored

                0x00, 0x00
            };
            var expected = new List<DoomTexture>
            {
                new()
                {
                    Name = "ABC",
                    Reserved0 = 0x0201,
                    Reserved1 = 0x0403,
                    Width = 0x0605,
                    Height = 0x0807,
                    Reserved4 = 0x0A09,
                    Reserved5 = 0x0C0B,
                    Patches = new List<DoomPatch>
                    {
                        new()
                        {
                            X = 0x0E0D,
                            Y = 0x100F,
                            PNameIndex = 0x1211,
                            StepDir = 0x1413,
                            ColorMap = 0x1615
                        },
                        new()
                        {
                            X = 0x1817,
                            Y = 0x1A19,
                            PNameIndex = 0x1C1B,
                            StepDir = 0x1E1D,
                            ColorMap = 0x201F
                        }
                    }
                },
                new()
                {
                    Name = "DEFGHIJK",
                    Reserved0 = 0x2221,
                    Reserved1 = 0x2423,
                    Width = 0x2625,
                    Height = 0x2827,
                    Reserved4 = 0x2A29,
                    Reserved5 = 0x2C2B,
                    Patches = new List<DoomPatch>
                    {
                        new()
                        {
                            X = 0x2E2D,
                            Y = 0x302F,
                            PNameIndex = 0x3231,
                            StepDir = 0x3433,
                            ColorMap = 0x3635
                        },
                        new()
                        {
                            X = 0x3837,
                            Y = 0x3A39,
                            PNameIndex = 0x3C3B,
                            StepDir = 0x3E3D,
                            ColorMap = 0x403F
                        }
                    }
                }
            };

            // Act.
            var observed = subject.Decode(data);

            // Assert.
            observed.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Encode_ShouldEncodeThings()
        {
        }
    }
}