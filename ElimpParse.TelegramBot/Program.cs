using System;
using System.Collections.Generic;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;

namespace ElimpParse.TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            //users.InsertRange(0, oldPlayers);
            //string idGroup = 'A';
            List<ElimpUser> users = DataGenerator.GetUserList();
            //       List<int> tasks       = DataGenerator.GetTaskList(users, idGroup);
            var bot = new SummerSchoolBot(users);
            Console.ReadLine();
            bot.Bot.StopReceiving();
            //foreach (var elimpUser in users)
            //{
            //    elimpUser.CompletedTaskCount = Parser.CompletedTaskCount(elimpUser.Login);
            //}
            //users = users.OrderByDescending(u => u.CompletedTaskCount).ToList();
            //NeedMoreInfo.GetMoreInfo(users, tasks, idGroup);
            Console.WriteLine("Hello World!");
        }
    }
}
