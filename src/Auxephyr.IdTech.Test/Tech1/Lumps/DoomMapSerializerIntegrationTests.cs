using System.IO.Compression;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace Auxephyr.IdTech.Tech1.Lumps
{
    [TestFixture]
    public class DoomMapSerializerIntegrationTests : IdTechTestBase
    {
        [Test]
        public void Test1()
        {
            using var zipStream = OpenData("doom1.zip");
            using var zipArchive = new ZipArchive(zipStream);
            using var wadStream = zipArchive.Entries.Single().Open();
            var wad = WadSerializer.Default.ReadStream(wadStream);

            var mapSerializer = DoomMapSerializer.Default;
            var mapGroupSerializer = MapGroupSerializer.Default;
            var mapNames = mapSerializer.GetAllMapNames(wad.Lumps);
            mapNames.Should().BeEquivalentTo("E1M1", "E1M2", "E1M3", "E1M4", "E1M5", "E1M6", "E1M7", "E1M8", "E1M9");

            var lumps = mapGroupSerializer.Read(wad.Lumps, mapSerializer.GetLumpNames(mapNames.First()));
            var map = mapSerializer.Decode(lumps);
            map.Blocks.Should().HaveCount(865);
            map.Linedefs.Should().HaveCount(475);
        }
    }
}