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
        public static readonly TelegramBotClient Bot = new TelegramBotClient("557358914:AAE03Faw9-BwKFygJHFMl530FiGH9sPvB6Y"); // 574977062:AAHC1xLdYfEtZ8EYQYuprhAi3DJYDhcgeGw");
        static bool flag = true;
        static void Main(string[] args)
        {

            /* var oldPlayers = new List<ElimpUser>
             {
                 new ElimpUser("Strannik", "II место на области"),
                 new ElimpUser("iNooByX", "III место на области"),
                 new ElimpUser("Maxkolpak", "III место на городе"),
                 new ElimpUser("krab397", "III место на облати"),
                 new ElimpUser("i4happy", "I место на городе"),
                 new ElimpUser("vlad986523", "II место на городе")
             };
             */
            //   users.InsertRange(0, oldPlayers);
            Bot.OnMessage += Bot_OnMassage;
            Bot.StartReceiving();

            Console.ReadLine();
            Bot.StopReceiving();
        }

        public static string PrintResult(List<ElimpUser> users, bool isHtml)
        {
            foreach (var user in users)
            {
                user.ComplitedTaskCount = Parser.ComplitedTaskCount(user.Login);
            }

            var sorted = users.OrderByDescending(e => e.ComplitedTaskCount).ToList();
            string s = "";
            for (var i = 0; i < sorted.Count; i++)
            {
                s += $"<code>{sorted[i]}</code>\n"; // s += $"<code>{sorted[i]} ({Parsetxt(sorted[i])})</code>\n";
                //    Console.WriteLine($"{sorted[i]}\n");

            }

            return s;
        }
        public static string Parsetxt()
        {
            string s = "";
            File.ReadAllLines("Info.txt");
            return s;
        }

        
        public static void Bot_OnMassage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var users = new List<ElimpUser>()
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
                new ElimpUser("tur4ik"),
                new ElimpUser("Enosha")
            };
    
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                int Hour = 13;
                int Minute = 43;
                if ((Hour == System.DateTime.Now.Hour) && (Minute - System.DateTime.Now.Minute <= 10) && flag)
                {
                    flag = false;
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, "Ежедневное обновление списка \n" + PrintResult(users, false), ParseMode.Html);
                    File.Delete("Info.txt");
                    File.WriteAllText("Info.txt", PrintResult(users, false));

                }
                if (e.Message.Text == "/getinfo")
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, PrintResult(users, false), ParseMode.Html);
                    Console.WriteLine("good");
                }
            }            
        }
    }

}


