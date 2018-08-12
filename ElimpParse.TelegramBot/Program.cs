using System;
using System.Collections.Generic;
using ElimpParse.DatabaseProvider;
using ElimpParse.DatabaseProvider.Repositories;
using Telegram.Bot.Types;
using ElimpParse.Core;
using ElimpParse.Model;

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