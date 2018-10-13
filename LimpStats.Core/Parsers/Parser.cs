using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LimpStats.Model;

namespace LimpStats.Core.Parsers
{
    public static class Parser
    {
        public static bool IsUserExist(string username)
        {
            var client = new HtmlWeb();
            string link = $"https://www.e-olymp.com/ru/users/{username}";

            return client.Load(link).Text.Contains($"{username}");
        }

        public static int LoadSolutionCount(string username)
        {
            var client = new HtmlWeb();
            string link = $"https://www.e-olymp.com/ru/users/{username}";

            HtmlNode floatRow = client.Load(link)
                .DocumentNode
                .SelectSingleNode("//*[contains(@class,'eo-flex-row')]");

            return int.Parse(floatRow
                .ChildNodes[1]
                .ChildNodes[0]
                .ChildNodes[1]
                .InnerText);
        }

        public static void LoadProfileData(ElimpUser user)
        {
            var client = new HtmlWeb();
            string link = $"https://www.e-olymp.com/ru/users/{user.Username}/punchcard";

            Dictionary<int, int> userResult = client.Load(link)
                .GetElementbyId("punch-card")
                .ChildNodes
                .Where(n => n.GetAttributeValue("href", "empty") != "empty")
                .Where(n => n.Attributes["href"].Value.Substring(0, 13) == "/ru/problems/")
                .Select(n => (TaskIdFromLink(n.Attributes["href"].Value), TitleToResult(n.Attributes["title"].Value)))
                .ToDictionary(pair => pair.Item1, pair => pair.Item2);

            user.UserProfileResult = userResult;
        }

        private static int TitleToResult(string taskTitle)
        {
            string stringRes = taskTitle.Split(',') //["title"], ["{count}%"]
                .Last() //"{count}%"
                .Replace("%", "");
            return int.Parse(stringRes);
        }

        private static int TaskIdFromLink(string link)
        {
            string stringRes = link.Split('/')
                .Last();
            return int.Parse(stringRes);
        }
    }
}