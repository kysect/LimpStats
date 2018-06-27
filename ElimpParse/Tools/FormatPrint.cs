using System.Collections.Generic;
using System.Linq;
using ElimpParse.Model;

namespace ElimpParse.Tools
{
    public static class FormatPrint
    {
        public static string ConsoleTaskCountFormat(ElimpUser user)
        {
            if (user.Title != null)
            {
                return $"{user.Login + " [" + user.Title + "]",-40} ({user.CompletedTaskCount})";
            }
            return $"{user.Login,-30} ({user.CompletedTaskCount})";

        }

        public static string ConsoleTaskListFormat(ElimpUser user, List<int> taskList)
        {
            string result ="";
            foreach (var taskId in taskList)
            {
                result += (user.TaskPack.GetTaskResult(taskId) == 100) ? "1" : "0";
            }
            return $"{user.Login, -15} | {result}";
        }

        public static string ConsoleTaskSumFormat(ElimpUser user, List<int> taskList, char idGroup)
        {
            int result = 0;
            foreach (var taskId in taskList)
            {
                result += (user.TaskPack.GetTaskResult(taskId) == 100) ? 1 : 0;
            }
            return $"`{user.Login, -15} Group {idGroup} Solutions {result}`";
        }

        public static string WebFormat(ElimpUser user)
        {
            return $"{user.Login}|{user.CompletedTaskCount}";
        }

        public static string TelegramFormat(ElimpUser user)
        {
            var list = BackUpManager.LoadFromJson();
            var currentUser = list.FirstOrDefault(u => u.Login == user.Login);

            if(currentUser == null)
                return $"{user.Login,-14} |{user.CompletedTaskCount,-3} ({user.CompletedTaskCount})";
            
            return $"{user.Login,-14} |{user.CompletedTaskCount,-3} ({user.CompletedTaskCount - currentUser.CompletedTaskCount})";
        }
    }
}