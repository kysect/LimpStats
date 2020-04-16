using System;
using System.Collections.Generic;
using System.Linq;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpParse.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            UserGroup group = TemplatesFactory.UserGroup();
            MultiThreadParser.LoadProfiles(group);

            LoadAllPackInfo(group);
        }

        private static void LoadAllPackInfo(UserGroup group)
        {
            foreach (ProblemsPack pack in group.ProblemsPacks)
            {
                IEnumerable<ProblemPackResult> results = group.Users.Select(u => pack.GetResults(u));
                Console.WriteLine(string.Join("\n", GenerateStringForResults(results)));
                Console.WriteLine("\n\n");
            }
        }

        public static IEnumerable<string> GenerateStringForResults(IEnumerable<ProblemPackResult> results)
        {
            var output = new List<string>();

            foreach (ProblemPackResult result in results.OrderByDescending(res => res.SumOfPoint))
            {
                string dataRow = $"{result.Username,-15}:"
                                 + string.Join(" ", result.Points.Select(value => $"{value,5}"))
                                 + $" | {result.SumOfPoint,5}";

                output.Add(dataRow);
            }

            return output;
        }
    }
}