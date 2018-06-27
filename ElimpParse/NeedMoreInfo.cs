using System;
using System.Collections.Generic;
using System.Text;
using ElimpParse.Model;
using ElimpParse.Tools;

namespace ElimpParse
{
    public static class NeedMoreInfo
    {
        public static string GetMoreInfo(List<ElimpUser> _users, List<int> tasksGroup, string idGroup)
        {
            string s = "";
            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
               s += FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksGroup, idGroup) + "\n";
            }
            return s;
        }
    }
}
