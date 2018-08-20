using System.Collections.Generic;
using System.Linq;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client.Services
{
    public static class MainWindowService
    {
        public static IEnumerable<(string Username, int Points)> LoadTotalPoints(StudyGroup group)
        {
            var result = group.GetAllPackResult()
                .SelectMany(l => l)
                .GroupBy(l => l.Username)
                .Select(gr => (gr.Key, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);
            return result;
        }

        public static IEnumerable<GridCard> GetCardWithEnosha()
        {
            var group = InstanceGenerator.GenerateTemplateGroup();
            group.UserList.Add(new ElimpUser { Login = "Enosha" });
            MultiThreadParser.LoadProfiles(group.UserList);
            var res = LoadTotalPoints(group)
                .Select(item => new GridCard(item.Username, item.Username, item.Points));
            return res;
        }
    }
}