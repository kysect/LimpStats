using System.Collections.Generic;
using System.Linq;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Core
{
    //TODO: move/delete
    public static class FormatPrint
    {
        public static IEnumerable<string> GenerateCountResults(StudyGroup group)
        {
            return group
                .UserList
                .OrderByDescending(u => u.CompletedTaskCount())
                .Select(u => $"{u.Username,-40} ({u.CompletedTaskCount()})");
        }

        public static IEnumerable<string> GeneratePackResults(IEnumerable<ProblemPackResult> results)
        {
            var output = new List<string>();

            foreach (ProblemPackResult result in results.OrderByDescending(user => user.ProblemResultList.Sum()))
            {
                string taskString = string.Join(" ", result.ProblemResultList.Select(value => $"{value,5}"));
                string additionalPoints = $"| (+{result.AdditionalPoints,3})";
                string totalCount = $" | {result.ProblemResultList.Sum() + result.AdditionalPoints,5}";
                string fullString = $"{result.Username,-15}:{taskString}{additionalPoints}{totalCount}";


                output.Add(fullString);
            }

            return output;
        }

        public static string GenerateDayResults(LimpUser user)
        {
            List<LimpUser> list = JsonBackupManager.LoadFromJson();
            LimpUser currentUser = list.FirstOrDefault(u => u.Username == user.Username);

            int completed = user.CompletedTaskCount() -
                            (currentUser?.CompletedTaskCount() ?? 0);

            return $"{user.Username,-14} |{user.CompletedTaskCount(),-3} ({completed})";
        }
    }
}