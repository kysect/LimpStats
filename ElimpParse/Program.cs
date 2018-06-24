using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;


namespace ElimpParse
{
    class Program
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient("574977062:AAHC1xLdYfEtZ8EYQYuprhAi3DJYDhcgeGw");
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
                if (isHtml)
                {
                    return $"<tr><td>{i}.</td><td>{sorted[i].Login}</td><td>{sorted[i].ComplitedTaskCount}</td></tr>";
                }
                else
                {
                    s += $"<code>{sorted[i]}</code>\n";
                    Console.WriteLine($"{sorted[i]}\n");
                }
            }
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
                new ElimpUser("Enosha")
            };
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (e.Message.Text == "/getinfo")
                {
                    Bot.SendTextMessageAsync(e.Message.Chat.Id, PrintResult(users, false), ParseMode.Html);
                    Console.WriteLine("good");
                }
            }

        }
    }
}
