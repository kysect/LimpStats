using System.Collections.Generic;
using LimpStats.Model;

namespace LimpStats.Database
{
    public static class DataGenerator
    {
        public static StudyGroup GenerateTemplateGroup()
        {
            var users = new List<LimpUser>
            {
                new LimpUser("Andrey2005"),
                new LimpUser("DDsov"),
                new LimpUser("Den4758"),
                new LimpUser("Gladtoseeyou"),
                new LimpUser("Koteika"),
                new LimpUser("liza.898"),
                new LimpUser("Mr.Hovik"),
                new LimpUser("NastyaVadko284"),
                new LimpUser("papercut6820"),
                new LimpUser("Pozitiv4ik"),
                new LimpUser("prostoroma"),
                new LimpUser("Swoop"),
                new LimpUser("v_7946"),
                new LimpUser("Versuzzz"),
                new LimpUser("Xsqten"),
                new LimpUser("Enosha"),
                new LimpUser("tur4ik"),
                new LimpUser("DiMaNsKi"),
                new LimpUser("cerepawka")
            };
            users.AddRange(GetOldGeneration());

            var group = new StudyGroup(users);
            group.ProblemPackList.Add(new ProblemPackInfo("A", TaskPackStorage.TasksAGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("B", TaskPackStorage.TasksBGroup, 200));
            group.ProblemPackList.Add(new ProblemPackInfo("C", TaskPackStorage.TasksCGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("D", TaskPackStorage.TasksDGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("E", TaskPackStorage.TasksEGroup, 300));
            group.ProblemPackList.Add(new ProblemPackInfo("F", TaskPackStorage.TasksFGroup, 500));

            return group;
        }

        private static List<LimpUser> GetOldGeneration()
        {
            return new List<LimpUser>
            {
                //new LimpUser("Strannik", "II место на области"),
                //new LimpUser("iNooByX", "III место на области"),
                //new LimpUser("Maxkolpak", "III место на городе"),
                //new LimpUser("krab397", "III место на облати"),
                //new LimpUser("i4happy", "I место на городе"),
                //new LimpUser("vlad986523", "II место на городе")
            };
        }
    }
}