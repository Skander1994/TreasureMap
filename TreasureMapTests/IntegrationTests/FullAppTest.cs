using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Application;

namespace TreasureMapTests.IntegrationTests
{
    public class FullAppTest
    {
        public string OutputFilePath { get; set; }
        public string OutputExpectedFilePath { get; set; }
        public string[] InputFiles { get; set; }

        [SetUp]
        public void Setup()
        {
            InputFiles = new string[2]
            {
                 "TreasureMap.txt",
                 "TreasureMapResult.txt"
            };
            OutputExpectedFilePath = Path.Combine(Directory.GetCurrentDirectory(), "ExpectedTreasureMapResult.txt");
            OutputFilePath = Path.Combine(Directory.GetCurrentDirectory(), InputFiles[1]);
            
            if (File.Exists(OutputFilePath))
                File.Delete(OutputFilePath);

        }

        [Test]
        public async Task TreasureMap_FullApplicationTestAsync()
        {
            var treasureCollector = new TreasureCollector();
            await treasureCollector.ProceedTreasureCollectionAsync(InputFiles);

            Assert.IsTrue(File.Exists(OutputFilePath));
            FileAssert.AreEqual(new FileInfo(OutputExpectedFilePath), new FileInfo(OutputFilePath));
        }
    }
}
