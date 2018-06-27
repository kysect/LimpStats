using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using System.Text;
using System.IO;
using ElimpParse.Model;
using Newtonsoft.Json;
namespace ElimpParse
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
            //users.InsertRange(0, oldPlayers);
     //       string idGroup = 'A';
            List<ElimpUser> users = UserGroup.GetUserList();
     //       List<int> tasks       = UserGroup.GetTaskList(users, idGroup);
            var bot = new SummerSchoolBot(users);
            Console.ReadLine();
            bot.Bot.StopReceiving();
            //foreach (var elimpUser in users)
            //{
            //    elimpUser.CompletedTaskCount = Parser.CompletedTaskCount(elimpUser.Login);
            //}
            //users = users.OrderByDescending(u => u.CompletedTaskCount).ToList();
          //  NeedMoreInfo.GetMoreInfo(users, tasks, idGroup);
        }
    }
}


