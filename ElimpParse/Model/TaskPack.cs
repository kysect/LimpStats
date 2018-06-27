using System;
using System.Linq;
using HtmlAgilityPack;

namespace ElimpParse.Model
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
            foreach (var node in _nodes)
            {
                try
                {

                    if (node.Attributes["href"].Value == $"/ru/problems/{taskId}")
                        return TitleToResult(node.Attributes["title"].Value);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }

            return -1;
        }

        private int TitleToResult(string title)
        {
            var res = title.Split(", ").Last();
            return int.Parse(res.Substring(0, res.Length - 1));
        }
    }
}