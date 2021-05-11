using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TreasureMap.Helpers
{
    public static class FileHelper
    {
        public static async Task<IEnumerable<string>> ReadInputFile(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return await File.ReadAllLinesAsync(path);
        }
        public static async Task WriteOutputFileAsync(string fileName, List<string> output)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            await File.WriteAllLinesAsync(path, output);
        }
    }
}
