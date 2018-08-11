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
            //var oldPlayers = new List<ElimpUser>
            // {
            //     new ElimpUser("Strannik", "II место на области"),
            //     new ElimpUser("iNooByX", "III место на области"),
            //     new ElimpUser("Maxkolpak", "III место на городе"),
            //     new ElimpUser("krab397", "III место на облати"),
            //     new ElimpUser("i4happy", "I место на городе"),
            //     new ElimpUser("vlad986523", "II место на городе")
            // };
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResult();

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

        private static void LoadStudentsCountInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResult();

            foreach (var elimpUser in group.UserList.OrderByDescending(u => u.CompletedTaskCount()))
            {
                Console.WriteLine(FormatPrint.GenerateCountResultData(elimpUser));
            }
        }
    }
}
