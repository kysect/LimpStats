using System.Collections.Generic;
using System.Linq;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public class FormatPrint
    {
        public static string ConsoleTaskCountFormat(ElimpUser user)
        {
            if (user.Title != null)
            {
                return $"{user.Login + " [" + user.Title + "]",-40} ({user.CompletedTaskCount})";
            }
            return $"{user.Login,-40} ({user.CompletedTaskCount})";

        }

        public static string ConsoleTaskListFormat(ElimpUser user, List<int> taskList)
        {
            string result = "";
            foreach (var taskId in taskList)
            {
                result += (user.TaskPack.GetTaskResult(taskId) == 100) ? "1" : "0";
            }
            return $"{user.Login,-15} | {result}";
        }

        public static string ConsoleTaskSumFormat(ElimpUser user, List<int> taskList, string idGroup)
        {
            int result = 0;
            foreach (var taskId in taskList)
            {
                result += (user.TaskPack.GetTaskResult(taskId) == 100) ? 1 : 0;
            }
            return $"{user.Login,-15} Group {idGroup} Solutions" + $"{"",-2}" + (result == taskList.Count ? "Complited" : $"{result,-2}| {taskList.Count}");
        }

        public static string WebFormat(ElimpUser user)
        {
            return $"{user.Login}|{user.CompletedTaskCount}";
        }

        public static string TelegramFormat(ElimpUser user)
        {
            //TODO: remove
            var list = JsonBackupManager.LoadFromJson();
            var currentUser = list.FirstOrDefault(u => u.Login == user.Login);

            if (currentUser == null)
                return $"{user.Login,-14} |{user.CompletedTaskCount,-3} ({user.CompletedTaskCount})";

            return $"{user.Login,-14} |{user.CompletedTaskCount,-3} ({user.CompletedTaskCount - currentUser.CompletedTaskCount})";
        }
    }
}