using System.Collections.Generic;
using System.Linq;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public class FormatPrint
    {
        public static string GenerateCountResultData(ElimpUser user)
        {
            if (user.Title != null)
                return $"{user.Login + " [" + user.Title + "]",-40} ({user.CompletedTaskCount()})";
            return $"{user.Login,-40} ({user.CompletedTaskCount()})";
        }

        public static List<string> GeneratePackResultData(ProblemPackInfo pack, List<ProblemPackResult> results)
        {
            var output = new List<string>();

            foreach (var result in results.OrderByDescending(user => user.ProblemResultList.Sum()))
            {
                var taskString = string.Join(" ", result.ProblemResultList.Select(value => $"{value, 3}"));
                var additionalPoints = $"| (+{result.AdditionalPoints, 3})";
                var totalCount = $" | {result.ProblemResultList.Sum() + result.AdditionalPoints, 5}";
                var fullString = $"{result.User.Login,-15}:{taskString}{additionalPoints}{totalCount}";


                output.Add(fullString);
            }

            return output;
        }

        public static string WebFormat(ElimpUser user)
        {
            //TODO: refactoring
            return $"{user.Login}|{user.CompletedTaskCount()}";
        }

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