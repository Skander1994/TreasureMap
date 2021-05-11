using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TreasureMap.Helpers;
using TreasureMap.Models;

namespace TreasureMap.Application
{
    public class TreasureCollector
    {
        public TreasureCollector()
        {
            MapInfo = new MapModel();
            Builder = new MapBuilder();
            Transformer = new MapTransformer();
            ResultBuilder = new MapResultBuilder();
        }
        public MapModel MapInfo { get; set; }
        public MapBuilder Builder { get; set; }
        public MapTransformer Transformer { get; set; }
        public MapResultBuilder ResultBuilder { get; set; }

        /// <summary>
        /// Lire le fihcier input, execute la logique de collecte de trésors et écrire dans le fichier output
        /// </summary>
        /// <param name="fileNames">le nom des fichiers input et output</param>
        public async Task ProceedTreasureCollectionAsync(string[] fileNames)
        {
            if (fileNames == null || fileNames.Length != 2)
            {
                Console.WriteLine("Please enter input and output file names");
                return;
            }
            MapInfo.InputFileContent = await FileHelper.ReadInputFile(fileNames[0]);
            var transformationResult = ExecuteTreasureCollection();
            await FileHelper.WriteOutputFileAsync(fileNames[1], transformationResult);
        }
        private List<string> ExecuteTreasureCollection()
        {
            Builder.BuildInputMap(MapInfo);
            Transformer.ExecuteAllMovements(MapInfo);
            return ResultBuilder.GetMapTransformationResult(MapInfo);
        }
    }
}
