using System.Collections.Generic;
using System.Linq;
using LimpStats.Model;

namespace LimpStats.Core
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
            //TODO: возможно это фикс
            group.LoadProfiles();
            return group
                .UserList
                .Select(u => new ProblemPackResult(u, taskPack))
                .OrderByDescending(res => res.TotalPoints)
                .ToList();
        }
    }
}