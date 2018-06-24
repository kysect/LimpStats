using System.Linq;
using HtmlAgilityPack;

namespace ElimpParse
{
    public class TaskPack
    {
        private readonly HtmlNodeCollection _nodes;

        public TaskPack(HtmlNodeCollection nodes)
        {
            _nodes = nodes;
        }

        public int GetTaskResult(int taskId)
        {
            var currentNode = _nodes.First(n => n.Attributes["href"].Value == $"/ru/problems/{taskId}");
            return TitleToResult(currentNode.Attributes["title"].Value);
        }

        private int TitleToResult(string title)
        {
            var res = title.Split(", ").Last();
            return int.Parse(res.Substring(0, res.Length - 1));
        }
    }
}