using System.Collections.Generic;
using System.Linq;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class StudyGroupExtensions
    {
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
    }
}