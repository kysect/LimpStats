using System;
using System.Collections.Generic;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using ElimpParse.DatabaseProvider.repositories;

namespace ElimpParse.TelegramBot
{
    public class SummerSchoolBot
    {
        public static bool flag;
        private static readonly string _tMaze = "557358914:AAE03Faw9-BwKFygJHFMl530FiGH9sPvB6Y";
        private static string _SSB = "574977062:AAHC1xLdYfEtZ8EYQYuprhAi3DJYDhcgeGw";
        private readonly StudyGroup _group;
        public readonly TelegramBotClient Bot;
        private bool _flag = true;

        public SummerSchoolBot(StudyGroup group)
        {
            _group = group;
            Bot = new TelegramBotClient(_tMaze);
            Bot.OnMessage += OnNewMessage;

            Bot.StartReceiving();
        }

        public void OnNewMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text) return;

            var hour = 09;
            var minute = 30;
            if (hour == DateTime.Now.Hour && DateTime.Now.Minute - minute <= 10 && _flag)
            {
                _flag = false;
                var msg = "Ежедневное обновление списка \n" + GenerateMessage(_group.UserList, false);
                Bot.SendTextMessageAsync(-1001356694472, msg, ParseMode.Html);
                JsonBackupManager.SaveToJson(_group.UserList);
            }

            if (e.Message.Text == "/getinfo")
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_group.UserList, false), ParseMode.Html);
                Console.WriteLine("good");
            }

            if (e.Message.Text == "/gettaskpackinfo" || flag)
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
                if (e.Message.Text == "A" || e.Message.Text == "B" || e.Message.Text == "C" || e.Message.Text == "D" ||
                    e.Message.Text == "E" || e.Message.Text == "F")
                {
                    var s = e.Message.Text;
                    var a = _group.ProblemPackList.First(pack => pack.PackTitle == s);

                    //TODO: user Group.GetPackResult
                    //string g = "<code>" + NeedMoreInfo.GetMoreInfo(_users, a, s) + "</code>";
                    var g = string.Empty;

                    Bot.SendTextMessageAsync(e.Message.Chat.Id, g, ParseMode.Html).Wait();
                    Console.WriteLine("good");
                    flag = false;
                }
                // Bot.StopReceiving();

                //   Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Default, false, false, 0,  rkm);
                Console.WriteLine("good");
            }

            if (e.Message.Text.Contains("/adduser"))
            {
                var User = e.Message.Text.Replace("/adduser -", "");
                var elimpUser = new ElimpUser(User);
                Parser.LoadUserData(elimpUser);
                foreach (KeyValuePair<int, int> valuePair in elimpUser.UserProfileResult)
                {
                    UserRepositoriy.UpdateAllInfoDB(User, valuePair.Key, valuePair.Value);
                }

            }

        }


        public static string GenerateMessage(List<ElimpUser> users, bool isHtml)
        {
            foreach (var user in users)
                Parser.LoadUserData(user);
            //user = Parser.CompletedTaskCount(user.Login);


            var sorted = users.OrderByDescending(e => e.CompletedTaskCount()).ToList();
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
