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
            group.LoadStudentsResultMultiThread();
            var result = group.GetAllPackResult();


            var list = result.SelectMany(l => l.results)
                .GroupBy(l => l.User)
                .Select(gr => (gr.Key.Login, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);

            foreach (var tuple in list)
            {
                Console.WriteLine($"{tuple.Item1, -15}: {tuple.Item2, -5}");
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
            }
        }

        private static void LoadStudentsCountInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();

            //watch.Start();
            //group.LoadStudentsResults();
            //Console.WriteLine(watch.Elapsed);
            //watch.Restart();

            group.LoadStudentsResultMultiThread();

            foreach (var elimpUser in group.UserList.OrderByDescending(u => u.CompletedTaskCount()))
            {
                Console.WriteLine(FormatPrint.GenerateCountResultData(elimpUser));
            }
        }
    }
}
