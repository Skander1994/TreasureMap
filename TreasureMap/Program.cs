using System;
using System.Threading.Tasks;
using TreasureMap.Application;

namespace TreasureMap
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var treasureCollector = new TreasureCollector();
                await treasureCollector.ProceedTreasureCollectionAsync(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une exception a été levée:");
                Console.WriteLine("Message: " + ex.Message);
            }
        }
    }
}
