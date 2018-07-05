using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadStudentsCountInfo();
            //AllPackInfo();
            LoadAllPackInfo();
        }

        private static void AllPackInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResultMultiThread();
            var result = group.GetAllPackResult();


            var list = result.SelectMany(l => l.results)
                .GroupBy(l => l.User)
                .Select(gr => (gr.Key.Login, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);

            foreach (var tuple in list)
            {
                Console.WriteLine($"{tuple.Item1,-15}: {tuple.Item2,5}");
            }
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

        private static void LoadStudentsCountInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResultMultiThread();

            foreach (var elimpUser in group.UserList.OrderByDescending(u => u.CompletedTaskCount()))
            {
                Console.WriteLine(FormatPrint.GenerateCountResultData(elimpUser));
            }
        }
    }
}
