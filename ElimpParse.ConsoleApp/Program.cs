using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var oldPlayers = new List<ElimpUser>
            // {
            //     new ElimpUser("Strannik", "II место на области"),
            //     new ElimpUser("iNooByX", "III место на области"),
            //     new ElimpUser("Maxkolpak", "III место на городе"),
            //     new ElimpUser("krab397", "III место на облати"),
            //     new ElimpUser("i4happy", "I место на городе"),
            //     new ElimpUser("vlad986523", "II место на городе")
            // };
            StudyGroup group = DataGenerator.GenerateTemplateGroup();

            foreach (var user in group.UserList)
            {
                Parser.CompletedTaskCount(user.Login);
            }
            group.GetTaskCountMultiThread();
        }

        private static void GetInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();

            //watch.Start();
            //group.LoadStudentsResults();
            //Console.WriteLine(watch.Elapsed);
            //watch.Restart();

            group.LoadStudentsResultMultiThread();

            foreach (var elimpUser in group.UserList.OrderByDescending(u => u.CompletedTaskCount()))
            {
                Console.WriteLine(FormatPrint.ConsoleTaskCountFormat(elimpUser));
            }
        }

        private static void TaskPackTimeTest(StudyGroup group)
        {
            Stopwatch watch = new Stopwatch();
            group.LoadStudentsResults();

            foreach (var taskPack in group.TaskPackList)
            {
                var res = FormatPrint.ConsoleTaskListFormat(group, taskPack);
                Console.WriteLine(string.Join("\n", res));
            }
        }
    }
}
