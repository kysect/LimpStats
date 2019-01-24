using System;
using System.Collections.Generic;
using System.Linq;
using LimpStats.Core;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;
using LimpStats.Model.Problems;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace LimpParse.TelegramBot
{
    public class SummerSchoolBot
    {
        public static bool flag;
        private readonly UserGroup _group;
        public readonly TelegramBotClient Bot;
        private bool _flag = true;

        public SummerSchoolBot(UserGroup group)
        {
            _group = group;
            Bot = new TelegramBotClient(Config.TelegramBotToken);
            Bot.OnMessage += OnNewMessage;

            Bot.StartReceiving();
        }

        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type != MessageType.Text)
            {
                return;
            }

            var hour = 09;
            var minute = 30;
            if (hour == DateTime.Now.Hour && DateTime.Now.Minute - minute <= 10 && _flag)
            {
                _flag = false;
                string msg = "Ежедневное обновление списка \n" + GenerateMessage(_group.Users);
                Bot.SendTextMessageAsync(-1001356694472, msg, ParseMode.Html);
                JsonBackupManager.SaveToJson(_group.Users);
            }

            if (e.Message.Text == "/getinfo")
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_group.Users), ParseMode.Html);
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
                if (e.Message.Text == "A"
                    || e.Message.Text == "B"
                    || e.Message.Text == "C"
                    || e.Message.Text == "D"
                    || e.Message.Text == "E"
                    || e.Message.Text == "F")
                {
                    string s = e.Message.Text;
                    ProblemsPack a = _group.ProblemsPacks.First(pack => pack.Title == s);

                    //TODO: user Group.GetPackResult
                    //string g = "<code>" + NeedMoreInfo.GetMoreInfo(_users, a, s) + "</code>";
                    string g = string.Empty;

                    Bot.SendTextMessageAsync(e.Message.Chat.Id, g, ParseMode.Html).Wait();
                    Console.WriteLine("good");
                    flag = false;
                }

                //   Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Default, false, false, 0,  rkm);
                Console.WriteLine("good");
            }

            if (e.Message.Text.Contains("/adduser"))
            {
                throw new NotImplementedException();
                string username = e.Message.Text.Replace("/adduser -", "");
                var elimpUser = new LimpUser(username);
                Parser.LoadProfileData(elimpUser);
                foreach (KeyValuePair<int, int> valuePair in elimpUser.EOlimpProblemsResult)
                {
                    //UserRepositoriy.UpdateAllInfoDB(User, valuePair.Key, valuePair.Value);
                }
            }
        }


        private static string GenerateMessage(List<LimpUser> users)
        {
            users.ForEach(Parser.LoadProfileData);
            IEnumerable<string> res = users
                .OrderByDescending(e => e.CompletedTaskCount())
                .Select(GenerateDayResults);
            return $"<code>{string.Join("\n", res)}</code>";
        }

        private static string GenerateDayResults(LimpUser user)
        {
            List<LimpUser> list = JsonBackupManager.LoadFromJson();
            LimpUser currentUser = list.FirstOrDefault(u => u.Username == user.Username);

            int completed = user.CompletedTaskCount() -
                            (currentUser?.CompletedTaskCount() ?? 0);

            return $"{user.Username,-14} |{user.CompletedTaskCount(),-3} ({completed})";
        }
    }
}