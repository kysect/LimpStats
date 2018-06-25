using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ElimpParse
{
    public class SummerSchoolBot
    {
        private List<ElimpUser> _users;
        private static string _firstToken = "557358914:AAE03Faw9-BwKFygJHFMl530FiGH9sPvB6Y";
        private static string _secondToken = "574977062:AAHC1xLdYfEtZ8EYQYuprhAi3DJYDhcgeGw";
        public readonly TelegramBotClient Bot;
        private bool flag = true;

        public SummerSchoolBot(List<ElimpUser> users)
        {
            _users = users;
            Bot = new TelegramBotClient(_secondToken);
            Bot.OnMessage += OnNewMessage;
            Bot.StartReceiving();
        }

        public void OnNewMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text) return;

            int Hour = 09;
            int Minute = 30;
            if ((Hour == System.DateTime.Now.Hour) && (System.DateTime.Now.Minute - Minute <= 10) && flag)
            {
                flag = false;
                string msg = "Ежедневное обновление списка \n" + GenerateMessage(_users, false);
                Bot.SendTextMessageAsync(-1001356694472, msg, ParseMode.Html);
                BackUpManager.SaveJson(_users);               
            }
            if (e.Message.Text == "/getinfo")
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Html);
                Console.WriteLine("good");
            }

        }

        public static string GenerateMessage(List<ElimpUser> users, bool isHtml)
        {
            foreach (var user in users)
            {
                user.CompletedTaskCount = Parser.CompletedTaskCount(user.Login);
            }

            
            var sorted = users.OrderByDescending(e => e.CompletedTaskCount).ToList();
            var res = sorted.Aggregate("", (s1, user) => s1 + $"<code>{FormatPrint.TelegramFormat(user)}</code>\n");
            //string s = "";
            //for (var i = 0; i < sorted.Count; i++)
            //{
            //    s += $"<code>{sorted[i]}</code>\n"; // s += $"<code>{sorted[i]} ({Parsetxt(sorted[i])})</code>\n";
            //    //    Console.WriteLine($"{sorted[i]}\n");

            //}

            return res;
        }
        //public static string Parsetxt()
        //{
        //    string s = "";
        //    File.ReadAllLines("Info.txt");
        //    return s;
        //}
    }
}