using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using ElimpParse.Model;
namespace ElimpParse
{
    class UserGroup
    {
        public static List<ElimpUser> GetUserList()
        {
            List<ElimpUser> users = new List<ElimpUser>
            {
                new ElimpUser("Andrey2005"),
                new ElimpUser("DDsov"),
                new ElimpUser("Den4758"),
                new ElimpUser("Gladtoseeyou"),
                new ElimpUser("Koteika"),
                new ElimpUser("liza.898"),
                new ElimpUser("Mr.Hovik"),
                new ElimpUser("NastyaVadko284"),
                new ElimpUser("papercut6820"),
                new ElimpUser("Pozitiv4ik"),
                new ElimpUser("prostoroma"),
                new ElimpUser("Swoop"),
                new ElimpUser("v_7946"),
                new ElimpUser("Versuzzz"),
                new ElimpUser("Xsqten"),
                new ElimpUser("Enosha", "Так и не взял всеукр"),
                new ElimpUser("tur4ik"),
                new ElimpUser("DiMaNsKi"),
                new ElimpUser("cerepawka"),
            };
            return users;
        }

        public static List<int> GetTaskList(List<ElimpUser> users, string idGroup)
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
            List<int> tasksFGroup = new List<int>
            {
                922,
                2098,
                4760,
                1952,
                1965,
                7402
            };
            switch (idGroup)
            {
                case "A": return tasksAGroup;
                case "B": return tasksBGroup;
                case "C": return tasksCGroup;
                case "D": return tasksDGroup;
                case "E": return tasksEGroup;
                case "F": return tasksFGroup;
                default: return tasksAGroup; 
            }
        }
    }
}

