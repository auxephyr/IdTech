using System.IO.Compression;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomSharewareIntegrationTests : IdTechTestBase
    {
        [Test]
        public void TestMaps()
        {
            using var zipStream = OpenData("doom1.zip");
            using var zipArchive = new ZipArchive(zipStream);
            using var wadStream = zipArchive.Entries.Single().Open();
            var wad = WadSerializer.Default.ReadStream(wadStream);

            var mapSerializer = DoomMapSerializer.Default;
            var mapNames = mapSerializer.GetMapLumpNames(wad.Lumps);
            mapNames.Should().BeEquivalentTo("E1M1", "E1M2", "E1M3", "E1M4", "E1M5", "E1M6", "E1M7", "E1M8", "E1M9");

            var lumps = wad.ReadLumpGroup(mapSerializer.GetLumpNames("E1M1"));
            var map = mapSerializer.Decode(lumps);
            map.Things.Should().HaveCount(138);
            map.Linedefs.Should().HaveCount(475);
            map.Sidedefs.Should().HaveCount(648);
            map.Vertices.Should().HaveCount(467);
            map.Segs.Should().HaveCount(732);
            map.SubSectors.Should().HaveCount(237);
            map.Nodes.Should().HaveCount(236);
            map.Sectors.Should().HaveCount(85);
            map.Rejects.Should().HaveCount(942);
            map.Blocks.Should().HaveCount(865);
        }

        [Test]
        public void TestTextures()
        {
            using var zipStream = OpenData("doom1.zip");
            using var zipArchive = new ZipArchive(zipStream);
            using var wadStream = zipArchive.Entries.Single().Open();
            var wad = WadSerializer.Default.ReadStream(wadStream).AsDoom();

            var textures = wad.LoadTextures();
            textures.Keys.Should().BeEquivalentTo("TEXTURE1");
            textures["TEXTURE1"].Should().HaveCount(125);
            textures["TEXTURE1"].First().Name.Should().Be("AASTINKY");
        }
    }
}