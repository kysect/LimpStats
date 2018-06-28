using System.Collections.Generic;
using System.Linq;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public class FormatPrint
    {
        //TODO: rename (and remake) to GenerateTaskCountData
        public static string ConsoleTaskCountFormat(ElimpUser user)
        {
            if (user.Title != null)
                return $"{user.Login + " [" + user.Title + "]",-40} ({user.CompletedTaskCount()})";
            return $"{user.Login,-40} ({user.CompletedTaskCount()})";
        }

        //TODO: rename (and remake) to GenerateTaskPackResultData
        public static List<string> ConsoleTaskListFormat(StudyGroup group, TaskGroupInfo taskPack)
        {
            var output = new List<string>();
            var userResultList = group.GetPackResult(taskPack);

            foreach (var result in userResultList)
            {
                var taskString = string.Join(" ", result.TaskResultList);
                var fullString = $"{result.User.Login,-15}: {taskString}";
                if (result.IsFullCorrect) fullString += $"| (+{taskPack.FullSolutionPoints})";
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