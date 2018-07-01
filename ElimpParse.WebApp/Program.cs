using System;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.WebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadAllPackInfo();
        }

        private static void LoadAllPackInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResultMultiThread();

            var result = group.GetAllPackResult();
            foreach (var (pack, results) in group.GetAllPackResult())
            {
                Console.WriteLine(string.Join("\n", FormatPrint.GeneratePackResultData(pack, results)));
                Console.WriteLine("\n\n");
            }
        }
    }
}
