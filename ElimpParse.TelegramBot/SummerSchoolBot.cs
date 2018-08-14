using System;
using System.Collections.Generic;
using System.Linq;
using LimpStats.Core;
using LimpStats.Database;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using LimpStats.Model;

namespace ElimpParse.TelegramBot
{
    public class SummerSchoolBot
    {
        private readonly StudyGroup _group;
        public readonly TelegramBotClient Bot;
        public static bool flag;
        private bool _flag = true;

        public SummerSchoolBot(StudyGroup group)
        {
            _group = group;
            Bot = new TelegramBotClient(Config.MazeBotToken);
            Bot.OnMessage += OnNewMessage;

            Bot.StartReceiving();
        }

        private void OnNewMessage(object sender, MessageEventArgs e)
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
                if (e.Message.Text == "A"
                    || e.Message.Text == "B"
                    || e.Message.Text == "C"
                    || e.Message.Text == "D"
                    ||e.Message.Text == "E"
                    || e.Message.Text == "F")
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

                //   Bot.SendTextMessageAsync(e.Message.Chat.Id, GenerateMessage(_users, false), ParseMode.Default, false, false, 0,  rkm);
                Console.WriteLine("good");
            }

            if (e.Message.Text.Contains("/adduser"))
            {
                throw new NotImplementedException();
                var username = e.Message.Text.Replace("/adduser -", "");
                var elimpUser = new ElimpUser(username);
                Parser.LoadUserData(elimpUser);
                foreach (KeyValuePair<int, int> valuePair in elimpUser.UserProfileResult)
                {
                    //UserRepositoriy.UpdateAllInfoDB(User, valuePair.Key, valuePair.Value);
                }

            }

        }


        private static string GenerateMessage(List<ElimpUser> users, bool isHtml)
        {
            users.ForEach(Parser.LoadUserData);
            var res = users
                .OrderByDescending(e => e.CompletedTaskCount())
                .Select(FormatPrint.TelegramFormat);
            return $"<code>{string.Join("\n", res)}</code>";
        }
    }
}
