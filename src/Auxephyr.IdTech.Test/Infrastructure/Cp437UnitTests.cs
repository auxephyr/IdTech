using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Infrastructure
{
    [TestFixture]
    public class Cp437UnitTests : IdTechTestBase
    {
        [Test]
        public void Decode_DecodesStandardCharacters()
        {
            var observed = Cp437.Decode(new byte[] {0x41, 0x42, 0x63, 0x64});
            observed.Should().Be("ABcd");
        }

        [Test]
        public void Encode_EncodesStandardCharacters()
        {
            var observed = Cp437.Encode("ABcd");
            observed.Should().BeEquivalentTo(new byte[] {0x41, 0x42, 0x63, 0x64});
        }

        [Test]
        public void Pad_ShouldExtendString_WhenStringIsShorterThanCount()
        {
            var observed = Cp437.Pad(new byte[] {0x41, 0x42, 0x43}, 6);
            observed.Should().BeEquivalentTo(new byte[] {0x41, 0x42, 0x43, 0x00, 0x00, 0x00});
        }

        [Test]
        public void Pad_ShouldTrimString_WhenStringIsLongerThanCount()
        {
            var observed = Cp437.Pad(new byte[] {0x41, 0x42, 0x43, 0x44, 0x45, 0x46}, 3);
            observed.Should().BeEquivalentTo(new byte[] {0x41, 0x42, 0x43});
        }

        [Test]
        public void UnPad_ShouldRemoveNullBytes()
        {
            var observed = Cp437.UnPad(new byte[] {0x41, 0x42, 0x00, 0x00, 0x00});
            observed.Should().BeEquivalentTo(new byte[] {0x41, 0x42});
        }
    }
}