using System;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpParse.TelegramBot
{
    internal static class Program
    {
        private static void Main()
        {
            UserGroup group = DataGenerator.GenerateTemplateGroup();
            var bot = new SummerSchoolBot(group);
            Console.ReadLine();
            bot.Bot.StopReceiving();

            Console.Read();
        }
    }
}