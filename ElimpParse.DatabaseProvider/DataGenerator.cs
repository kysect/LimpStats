using System.Collections.Generic;
using ElimpParse.Model;

namespace ElimpParse.DatabaseProvider
{
    public class DataGenerator
    {
        public static StudyGroup GenerateTemplateGroup()
        {
            var users = new List<ElimpUser>
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
                new ElimpUser("cerepawka")
            };

            var group = new StudyGroup(users);
            group.TaskPackList.Add(new TaskGroupInfo("A", TaskPackStorage.TasksAGroup, 3));
            group.TaskPackList.Add(new TaskGroupInfo("B", TaskPackStorage.TasksBGroup, 3));
            group.TaskPackList.Add(new TaskGroupInfo("C", TaskPackStorage.TasksCGroup, 3));
            group.TaskPackList.Add(new TaskGroupInfo("D", TaskPackStorage.TasksDGroup, 3));
            group.TaskPackList.Add(new TaskGroupInfo("E", TaskPackStorage.TasksEGroup, 3));
            group.TaskPackList.Add(new TaskGroupInfo("F", TaskPackStorage.TasksFGroup, 3));

            return group;
        }
    }
}