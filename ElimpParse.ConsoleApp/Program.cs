using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.DatabaseProvider.Models;
using ElimpParse.DatabaseProvider.Repositories;
using ElimpParse.Model;

namespace ElimpParse.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            MultiThreadParser.LoadProfiles(group.UserList);

            FinalTest(group);
            LoadTotalPoints(group);
            LoadAllPackInfo(group);
        }

        private static void FinalTest(StudyGroup group)
        {
            var res = group.GetPackResult(group.ProblemPackList.Last());
            Console.WriteLine(string.Join("\n", FormatPrint.GeneratePackResultData(res)));
        }

        private static void LoadTotalPoints(StudyGroup group)
        {
            var result = group.GetAllPackResult();


            var list = result.SelectMany(l => l)
                .GroupBy(l => l.Username)
                .Select(gr => (gr.Key, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);

            foreach (var tuple in list)
            {
                Console.WriteLine($"{tuple.Item1,-15}: {tuple.Item2,5}");
            }
        }

        private static void LoadAllPackInfo(StudyGroup group)
        {
            var result = group.GetAllPackResult();
            foreach (var results in group.GetAllPackResult())
            {
                Console.WriteLine(string.Join("\n", FormatPrint.GeneratePackResultData(results)));
                Console.WriteLine("\n\n");
            }
        }
    }
}
