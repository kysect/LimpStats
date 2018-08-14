using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using LimpStats.Database;

namespace ElimpParse.TelegramBot
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var group = DataGenerator.GenerateTemplateGroup();
            var bot = new SummerSchoolBot(group);
            Console.ReadLine();
            bot.Bot.StopReceiving();

            Console.Read();
        }
    }
}