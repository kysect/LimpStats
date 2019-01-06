using System.Collections.Generic;
using System.Linq;
using LimpStats.Model;

namespace LimpStats.Core
{
    public static class StudyGroupExtensions
    {
        //TODO: refactoring
        public static List<List<ProblemPackResult>> GetAllPackResult(this StudyGroup group)
        {
            return group
                .ProblemPackList
                .Select(pack => GetPackResult(group, pack))
                .ToList();
        }

        public static List<ProblemPackResult> GetPackResult(this StudyGroup group, ProblemPackInfo taskPack)
        {
            return group
                .UserList
                .Select(u => new ProblemPackResult(u, taskPack))
                .OrderByDescending(res => res.TotalPoints)
                .ToList();
        }

        public static IEnumerable<(string Username, int Points)> GetTotalPoints(this StudyGroup group)
        {
            IOrderedEnumerable<(string, int)> result = group.GetAllPackResult()
                .SelectMany(l => l)
                .GroupBy(l => l.Username)
                .Select(gr => (gr.Key, gr.Sum(g => g.TotalPoints)))
                .OrderByDescending(t => t.Item2);
            return result;
        }
    }
}