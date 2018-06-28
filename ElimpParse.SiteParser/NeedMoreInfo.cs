using System.Collections.Generic;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    //TODO: remove this
    public class NeedMoreInfo
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