using System;
using ElimpParse.DatabaseProvider;

namespace ElimpParse.TelegramBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //users.InsertRange(0, oldPlayers);
            //string idGroup = 'A';
            //List<ElimpUser> users = DataGenerator.GetUserList();
            var group = DataGenerator.GenerateTemplateGroup();
            //       List<int> tasks       = DataGenerator.GetTaskList(users, idGroup);
            var bot = new SummerSchoolBot(group);
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