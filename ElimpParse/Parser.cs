using System.Collections.Generic;
using HtmlAgilityPack;

namespace ElimpParse
{
    public class Parser
    {
        private static HtmlWeb _client = new HtmlWeb();

        public static TaskPack GetUserTaskList(string username)
        {
            string link = $"https://www.e-olymp.com/ru/users/{username}/punchcard";
            var userNodeList = _client.Load(link)
                .GetElementbyId("punch-card")
                .ChildNodes;
            return new TaskPack(userNodeList);
        }

        public static int ComplitedTaskCount(string username)
        {
            string link = $"https://www.e-olymp.com/ru/users/{username}";
            var floatRow = _client.Load(link)
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