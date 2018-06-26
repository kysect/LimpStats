using System.Linq;
﻿using System.Collections.Generic;
namespace ElimpParse
{
    public static class FormatPrint
    {
        public static string ConsoleFormat(ElimpUser user)
        {
            if (user.Title != null)
            {
                return $"{user.Login + " [" + user.Title + "]",-30} ({user.CompletedTaskCount})";
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
            var list = BackUpManager.LoadJson().Where(u => u.Login == user.Login).First();
            return $"{user.Login, -14} |{user.CompletedTaskCount, -3} ({user.CompletedTaskCount - list.CompletedTaskCount})";
        }
    }
}