using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LimpStats.Model;

namespace LimpStats.Core
{
    public static class Parser
    {
        public static int CompletedTaskCount(string username)
        {
            var client = new HtmlWeb();
            var link = $"https://www.e-olymp.com/ru/users/{username}";

            var floatRow = client.Load(link)
                .DocumentNode
                .SelectSingleNode("//*[contains(@class,'eo-flex-row')]");

            return int.Parse(floatRow
                .ChildNodes[1]
                .ChildNodes[0]
                .ChildNodes[1]
                .InnerText);
        }
        public static bool LoginValidation(string username)
        {
            var client = new HtmlWeb();
            var link = $"https://www.e-olymp.com/ru/users/{username}";

            if(client.Load(link).Text.Contains($"{username}"))
                return true;
            return false;
        }

        public static void LoadUserData(ElimpUser user)
        {
            var client = new HtmlWeb();
            var link = $"https://www.e-olymp.com/ru/users/{user.Login}/punchcard";

            Dictionary<int, int> userResult = client.Load(link)
                .GetElementbyId("punch-card")
                .ChildNodes
                .Where(n => n.GetAttributeValue("href", "empty") != "empty")
                .Where(n => n.Attributes["href"].Value.Substring(0, 13) == "/ru/problems/")
                .Select(n => (TaskIdFromLink(n.Attributes["href"].Value),TitleToResult(n.Attributes["title"].Value)))
                .ToDictionary(pair => pair.Item1, pair => pair.Item2);

            user.UserProfileResult = userResult;
        }

        private static int TitleToResult(string taskTitle)
        {
            var stringRes = taskTitle.Split(',') //["title"], ["{count}%"]
                .Last() //"{count}%"
                .Replace("%", "");
            return int.Parse(stringRes);
        }

        private static int TaskIdFromLink(string link)
        {
            var stringRes = link.Split('/')
                .Last();
            return int.Parse(stringRes);
        }
    }
}