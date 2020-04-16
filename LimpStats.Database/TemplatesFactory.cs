using System;
using System.Collections.Generic;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Database
{
    public static class TemplatesFactory
    {
        public static List<LimpUser> UserList()
        {
            //TODO: Add feature: achievements
            return new List<LimpUser>
            {
                //new LimpUser("Andrey2005"),
                //new LimpUser("DDsov"),
                //new LimpUser("Den4758"),
                //new LimpUser("Gladtoseeyou"),
                //new LimpUser("Koteika"),
                //new LimpUser("liza.898"),
                //new LimpUser("Mr.Hovik"),
                //new LimpUser("NastyaVadko284"),
                //new LimpUser("papercut6820"),
                //new LimpUser("Pozitiv4ik"),
                //new LimpUser("prostoroma"),
                //new LimpUser("Swoop"),
                //new LimpUser("v_7946"),
                //new LimpUser("Versuzzz"),
                //new LimpUser("Xsqten"),
                //new LimpUser("Enosha"),
                //new LimpUser("tur4ik"),
                //new LimpUser("DiMaNsKi"),
                //new LimpUser("cerepawka")
                //new LimpUser("Strannik", "II место на области"),
                //new LimpUser("iNooByX", "III место на области"),
                //new LimpUser("Maxkolpak", "III место на городе"),
                //new LimpUser("krab397", "III место на облати"),
                //new LimpUser("i4happy", "I место на городе"),
                //new LimpUser("vlad986523", "II место на городе")
            };
        }

        public static List<ProblemsPack> EOlimpPacks()
        {
            List<Problem> firstPackProblems = Problem.CreateEOlympFromList(TaskPackStorage.TasksAGroup);
            List<Problem> secondPackProblems = Problem.CreateEOlympFromList(TaskPackStorage.TasksBGroup);
            return new List<ProblemsPack>
            {
                new ProblemsPack("Test pack A", firstPackProblems),
                new ProblemsPack("Test pack A", secondPackProblems)
            };
        }

        public static UserGroup UserGroup()
        {
            //TODO: remove?
            throw new NotSupportedException();
            //return new UserGroup("Testing User Group", UserList(), EOlimpPacks());
        }
    }
}