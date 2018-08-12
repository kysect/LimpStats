using System.Collections.Generic;
using System.Linq;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;
using Newtonsoft.Json;
using  System;
using System.IO;

namespace ElimpParse.Core
{
    public static class FormatPrint
    {
        public static string GenerateCountResultData(StudyGroup group)
        {
            var data = group
                .UserList
                .OrderByDescending(u => u.CompletedTaskCount())
                .Select(u => $"{u.Login,-40} ({u.CompletedTaskCount()})");
            return string.Join("\n", data);
        }

        public static List<string> GeneratePackResultData(List<ProblemPackResult> results)
        {
            var output = new List<string>();

            foreach (var result in results.OrderByDescending(user => user.ProblemResultList.Sum()))
            {
                var taskString = string.Join(" ", result.ProblemResultList.Select(value => $"{value, 5}"));
                var additionalPoints = $"| (+{result.AdditionalPoints, 3})";
                var totalCount = $" | {result.ProblemResultList.Sum() + result.AdditionalPoints, 5}";
                var fullString = $"{result.Username,-15}:{taskString}{additionalPoints}{totalCount}";


                output.Add(fullString);
            }
            return output;
        }
        //TODO: удалить
        public static string TelegramFormat(ElimpUser user)
        {
            //TODO: refactoring
            var list = JsonBackupManager.LoadFromJson();
            var currentUser = list.FirstOrDefault(u => u.Login == user.Login);

            var completed = user.CompletedTaskCount() -
                            (currentUser?.CompletedTaskCount() ?? 0);

            return $"{user.Login,-14} |{user.CompletedTaskCount(),-3} ({completed})";
        }
    }
}