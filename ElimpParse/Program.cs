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
using Newtonsoft.Json;
namespace ElimpParse
{
    class Program
    {
        static void Main(string[] args)
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
            };

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


            //var bot = new SummerSchoolBot(users);
            //Console.ReadLine();
            //bot.Bot.StopReceiving();


            //foreach (var elimpUser in users)
            //{
            //    elimpUser.CompletedTaskCount = Parser.CompletedTaskCount(elimpUser.Login);
            //}
            //users = users.OrderByDescending(u => u.CompletedTaskCount).ToList();

    //     foreach (var elimpUser in users)
      //      {
             //   Console.WriteLine(elimpUser);
                NeedMoreInfo.GetMoreInfo(users);
        //    }
        }

    }

}


