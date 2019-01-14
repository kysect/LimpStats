using System;
using System.Collections.Generic;
using System.Linq;
using LimpStats.Core;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpParse.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            MultiThreadParser.LoadProfiles(group);

            FinalTest(group);
            LoadTotalPoints(group);
            LoadAllPackInfo(group);
        }

        private static void FinalTest(StudyGroup group)
        {
            List<ProblemPackResult> res = group.GetPackResult(group.ProblemPackList.Last());
            Console.WriteLine(string.Join("\n", FormatPrint.GeneratePackResults(res)));
        }

        private static void LoadTotalPoints(StudyGroup group)
        {
            IOrderedEnumerable<(string, int)> list = group
                .GetAllPackResult()
                .SelectMany(l => l)
                .GroupBy(l => l.Username)
                .Select(gr => (gr.Key, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);

            foreach ((string, int) tuple in list)
            {
                Console.WriteLine($"{tuple.Item1,-15}: {tuple.Item2,5}");
            }
        }

        private static void LoadAllPackInfo(StudyGroup group)
        {
            foreach (List<ProblemPackResult> results in group.GetAllPackResult())
            {
                Console.WriteLine(string.Join("\n", FormatPrint.GeneratePackResults(results)));
                Console.WriteLine("\n\n");
            }
        }
    }
}