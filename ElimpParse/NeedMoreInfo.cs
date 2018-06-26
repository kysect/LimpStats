using System;
using System.Collections.Generic;
using System.Text;

namespace ElimpParse
{
    public static class NeedMoreInfo
    {
        public static void GetMoreInfo(List<ElimpUser> _users)
        {
            List<int> tasksAGroup = new List<int>
            {
                5133,
                7401,
                7944,
                4716,
                133,
                138,
                923,
                108,
                1623,
                927,
                1118,
                542,
                4538
            };
            List<int> tasksBGroup = new List<int>
            { 
                4717,
                7943,
                519,
                902,
                8242,
                248,
                20,
                903,
                1312,
                7548
            };
            List<int> tasksCGroup = new List<int>
            {
                7293,
                7336,
                2391,
                67,
                905,
                7460,
                518,
                421,
                407,
                904,
                2218,
                11,
                125
            };
            List<int> tasksDGroup = new List<int>
            {
                2,
                4805,
                280,
                340,
                7813,
                920,
                2862,
                910,
                382,
                473,
                500,
                7410
            };
            List<int> tasksEGroup = new List<int>
            {
                922,
                2098,    
                4760,
                1952,
                1965,
                7402
            };

            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
                Console.WriteLine(FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksAGroup, 'A'));
            }
            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
                Console.WriteLine(FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksBGroup, 'B'));
            }
            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
                Console.WriteLine(FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksCGroup, 'C'));
            }
            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
                Console.WriteLine(FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksDGroup, 'D'));
            }
            foreach (var elimpUser in _users)
            {
                elimpUser.TaskPack = Parser.GetUserTaskList(elimpUser.Login);
                Console.WriteLine(FormatPrint.ConsoleTaskSumFormat(elimpUser, tasksEGroup, 'E'));
            }
        }
    }
}
