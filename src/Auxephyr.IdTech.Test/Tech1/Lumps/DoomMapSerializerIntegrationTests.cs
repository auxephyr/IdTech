using System.IO.Compression;
using System.Linq;
using Auxephyr.IdTech.Infrastructure;
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

            var subject = DoomMapSerializer.Default;
            var mapNames = subject.GetAllMapNames(wad.Lumps);
            Log.Write(mapNames);

            var map = subject.Read(wad.Lumps, mapNames[0]);
        }
    }
}