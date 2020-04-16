using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LimpStats.Model;

namespace LimpStats.Core.Parsers
{
    public class ElimpParser : IProblemParser
    {
        public const string DomainUrl = "https://www.e-olymp.com/ru";

        public bool IsUserExist(string username)
        {
            var client = new HtmlWeb();
            string link = DomainUrl + $"/users/{username}";

            return client.Load(link).Text.Contains($"{username}");
        }

        public string GetProblemTitle(string problemIdentifier)
        {
            int id = int.Parse(problemIdentifier);

            string url = DomainUrl + $"/problems/{id}";
            var web = new HtmlWeb();
            HtmlNode doc = web.Load(url)
                .DocumentNode
                .SelectSingleNode("//*[contains(@class,'eo-paper__content')]");

            return doc.ChildNodes[0].InnerHtml ?? throw new ParserException($"Task with id={id} wasn't found");
        }

        public void LoadUserData(LimpUser user)
        {
            var client = new HtmlWeb();
            string link = ElimpParser.DomainUrl + $"/users/{user.EOlympLogin}/punchcard";

            Dictionary<int, int> userResult = client.Load(link)
                .GetElementbyId("punch-card")
                .ChildNodes
                .Where(n => n.GetAttributeValue("href", "empty") != "empty")
                .Where(n => n.Attributes["href"].Value.Substring(0, 13) == "/ru/problems/")
                .Select(n => (TaskIdFromLink(n.Attributes["href"].Value), TitleToResult(n.Attributes["title"].Value)))
                .ToDictionary(pair => pair.Item1, pair => pair.Item2);

            user.EOlimpProblemsResult = userResult;
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
            string stringRes = link
                .Split('/')
                .Last();
            return int.Parse(stringRes);
        }

        public static int LoadSolutionCount(string username)
        {
            var client = new HtmlWeb();
            string link = DomainUrl + $"/users/{username}";

            HtmlNode floatRow = client.Load(link)
                .DocumentNode
                .SelectSingleNode("//*[contains(@class,'eo-flex-row')]");

            return int.Parse(floatRow
                .ChildNodes[1]
                .ChildNodes[0]
                .ChildNodes[1]
                .InnerText);
        }
    }
}