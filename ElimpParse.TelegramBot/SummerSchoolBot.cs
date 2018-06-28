using System;
using System.Collections.Generic;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace ElimpParse.TelegramBot
{
    public class SummerSchoolBot
    {
        public static bool flag = false;
        private readonly List<ElimpUser> _users;
        private static string _tMaze = "557358914:AAE03Faw9-BwKFygJHFMl530FiGH9sPvB6Y";
        private static string _SSB = "574977062:AAHC1xLdYfEtZ8EYQYuprhAi3DJYDhcgeGw";
        public readonly TelegramBotClient Bot;
        private bool _flag = true;

        public SummerSchoolBot(List<ElimpUser> users)
        {
            _users = users;
            Bot = new TelegramBotClient(_tMaze);
            Bot.OnMessage += OnNewMessage;

            Bot.StartReceiving();
        }

        public void OnNewMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text) return;

            var hour = 09;
            var minute = 30;
            if ((hour == System.DateTime.Now.Hour) && (System.DateTime.Now.Minute - minute <= 10) && _flag)
            {
                _flag = false;
                string msg = "Ежедневное обновление списка \n" + GenerateMessage(_users, false);
                Bot.SendTextMessageAsync(-1001356694472, msg, ParseMode.Html);
                JsonBackupManager.SaveToJson(_users);
            }
            if (e.Message.Text == "/getinfo")
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Html);
                Console.WriteLine("good");
            }

            if (e.Message.Text == "/gettaskpackinfo" || flag == true)
            {
                flag = true;
                /*             var rkm = new ReplyKeyboardMarkup();
                             rkm.Keyboard =
                                 new KeyboardButton[][]
                                 {
                                     new KeyboardButton[]
                                     {
                                         new KeyboardButton("A"),
                                         new KeyboardButton("B")
                                     },
                                     new KeyboardButton[]
                                     {
                                         new KeyboardButton("C"),
                                         new KeyboardButton("D")
                                     },
                                     new KeyboardButton[]
                                     {
                                     new KeyboardButton("E"),
                                     new KeyboardButton("F")
                                     }
                                 };*/
                //&     Bot.OnMessage += OnNewMessage;
                //   Bot.StartReceiving();
                if (e.Message.Text == "A" || e.Message.Text == "B" || e.Message.Text == "C" || e.Message.Text == "D" || e.Message.Text == "E" || e.Message.Text == "F")
                {
                    string s = e.Message.Text;
                    List<int> a = DataGenerator.GetTaskList(_users, s);
                    string g = "<code>" + NeedMoreInfo.GetMoreInfo(_users, a, s) + "</code>";

                    Bot.SendTextMessageAsync(e.Message.Chat.Id, g, ParseMode.Html).Wait();
                    Console.WriteLine("good");
                    flag = false;
                }
                // Bot.StopReceiving();

                //   Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Default, false, false, 0,  rkm);
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
    }
}