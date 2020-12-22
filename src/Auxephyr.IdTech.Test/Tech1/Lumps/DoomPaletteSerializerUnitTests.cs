using System.Drawing;
using System.Linq;
using Auxephyr.IdTech.Tech1.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomPaletteSerializerUnitTests : IdTechTestBase
    {
        [Test]
        public void Decode_ShouldDecodeNodes()
        {
            // Arrange.
            var subject = Create<DoomPlaypalSerializer>();
            var data = CreateMany<byte>(1536).ToArray();

            // Act.
            var observed = subject.Decode(data);

            // Assert.
            observed.Should().HaveCount(2);
            observed[0].Colors[0].Should().BeEquivalentTo(Color.FromArgb(data[0], data[1], data[2]));
            observed[0].Colors[1].Should().BeEquivalentTo(Color.FromArgb(data[3], data[4], data[5]));
            observed[1].Colors[0].Should().BeEquivalentTo(Color.FromArgb(data[768], data[769], data[770]));
            observed[1].Colors[1].Should().BeEquivalentTo(Color.FromArgb(data[771], data[772], data[773]));
        }

        [Test]
        public void Encode_ShouldEncodeNodes()
        {
            // Arrange.
            var subject = Create<DoomPlaypalSerializer>();
            var palettes = CreateMany<DoomPalette>(2).ToArray();

            // Act.
            var observed = subject.Encode(palettes);
            
            // Assert.
            observed[0].Should().Be(palettes[0].Colors[0].R);
            observed[1].Should().Be(palettes[0].Colors[0].G);
            observed[2].Should().Be(palettes[0].Colors[0].B);
            observed[3].Should().Be(palettes[0].Colors[1].R);
            observed[4].Should().Be(palettes[0].Colors[1].G);
            observed[5].Should().Be(palettes[0].Colors[1].B);
            observed[768].Should().Be(palettes[1].Colors[0].R);
            observed[769].Should().Be(palettes[1].Colors[0].G);
            observed[770].Should().Be(palettes[1].Colors[0].B);
            observed[771].Should().Be(palettes[1].Colors[1].R);
            observed[772].Should().Be(palettes[1].Colors[1].G);
            observed[773].Should().Be(palettes[1].Colors[1].B);
        }
    }
}