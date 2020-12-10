using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Saxxon.TestInfrastructure;

namespace Auxephyr.IdTech.Tech1
{
    [TestFixture]
    public class WadTest : TestBase
    {
        [Test]
        public void Test1()
        {
            var steamDoomPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Steam",
                "steamapps",
                "common",
                "Ultimate Doom",
                "base",
                "DOOM.WAD"
            );

            if (!File.Exists(steamDoomPath))
            {
                Ignore("Steam DOOM.WAD does not exist for this test.");
                return;
            }

            var wad = Wad.ReadFile(steamDoomPath);
        }
    }
}