using System;
using System.IO;
using System.Text.Json;
using Auxephyr.IdTech.Infrastructure;
using Moq;
using NUnit.Framework;
using Saxxon.TestInfrastructure;

namespace Auxephyr.IdTech
{
    public abstract class IdTechTestBase : TestBase
    {
        [SetUp, Obsolete]
        public void SetUpIdTechTestBase()
        {
            Mock<ILog>()
                .Setup(x => x.Write(It.IsAny<object[]>()))
                .Callback<object[]>(objs =>
                {
                    foreach (var obj in objs)
                        TestContext.Out.WriteLine(obj is string s ? s : JsonSerializer.Serialize(obj));
                });
        }

        protected ILog Log => Mock<ILog>().Object;
        
        protected Stream OpenData(params string[] path)
        {
            var fileName = Path.Combine(
                TestContext.CurrentContext.TestDirectory, 
                "Data",
                Path.Combine(path));
            return File.OpenRead(fileName);
        }
    }
}