using System.Linq;
using ElimpParse.Model;
using HtmlAgilityPack;

namespace ElimpParse.Core
{
    public static class Parser
    {
        //public static TaskPack GetUserTaskList(string username)
        //{
        //    string link = $"https://www.e-olymp.com/ru/users/{username}/punchcard";
        //    var userNodeList = _client.Load(link)
        //        .GetElementbyId("punch-card")
        //        .ChildNodes;
        //    return new TaskPack(userNodeList);
        //}

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

        public static void LoadUserData(ElimpUser user)
        {
            var client = new HtmlWeb();
            var link = $"https://www.e-olymp.com/ru/users/{user.Login}/punchcard";

            var nodeList = client.Load(link)
                .GetElementbyId("punch-card")
                .ChildNodes;

            //TODO: test
            //TODO: user regular-expression
            var userResult = nodeList.Where(n => n.GetAttributeValue("href", "empty") != "empty")
                .Where(n => n.Attributes["href"].Value.Substring(0, 13) == "/ru/problems/")
                .Select(n => (TaskIdFromLink(n.Attributes["href"].Value), TitleToResult(n.Attributes["title"].Value)))
                .ToDictionary(pair => pair.Item1, pair => pair.Item2);

            user.UserProfileResult = userResult;
        }

        private static int TitleToResult(string taskTitle)
        {
            var stringRes = taskTitle.Split(", ") //["title"], ["{count}%"]
                .Skip(1) //["{count}%"]
                .First() //"{count}%"
                .Replace("%", "");
            return int.Parse(stringRes);
        }

        private static int TaskIdFromLink(string link)
        {
            var stringRes = link.Split("/")
                .Last();
            return int.Parse(stringRes);
        }
    }
}