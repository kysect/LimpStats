using System;
using System.Collections.Generic;
using ElimpParse.DatabaseProvider;
using ElimpParse.DatabaseProvider.repositories;
using Telegram.Bot.Types;
using ElimpParse.Core;
using ElimpParse.Model;

namespace ElimpParse.TelegramBot
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //users.InsertRange(0, oldPlayers);
            //string idGroup = 'A';
            var group = DataGenerator.GenerateTemplateGroup();
            var bot = new SummerSchoolBot(group);
            Console.ReadLine();
            bot.Bot.StopReceiving();
            //foreach (var elimpUser in users)
            //{
            //    elimpUser.CompletedTaskCount = Parser.CompletedTaskCount(elimpUser.Login);
            //}
            //users = users.OrderByDescending(u => u.CompletedTaskCount).ToList();
            //NeedMoreInfo.GetMoreInfo(users, tasks, idGroup);
            //       Console.WriteLine("Hello World!");
            //  UserRepositoriy.ConnectToDB();

            //var group = DataGenerator.GenerateTemplateGroup();
            //var res = group.LoadTaskCount();
            //foreach (var currentRes in res)
            //{
            //    UserRepositoriy.UpdateDB(currentRes.user.Login, currentRes.count);
            //}


            Console.Read();
        }
    }
}