using System;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpParse.TelegramBot
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            var bot = new SummerSchoolBot(group);
            Console.ReadLine();
            bot.Bot.StopReceiving();

            Console.Read();
        }
    }
}