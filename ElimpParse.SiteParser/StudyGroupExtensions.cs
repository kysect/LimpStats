using System.Collections.Generic;
using System.Linq;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class StudyGroupExtensions
    {
        public static void LoadStudentsResult(this StudyGroup group)
        {
            MultiThreadParser.MakeMultiThreadExecute(group.UserList, Parser.LoadUserData);
        }

        public static List<(ElimpUser, int)> LoadTaskCountMultiThread(this StudyGroup group)
        {
            return MultiThreadParser.LoadProblemsCount(group.UserList);
        }

        public static List<List<ProblemPackResult>> GetAllPackResult(this StudyGroup group)
        {
            var result = new List<List<ProblemPackResult>>();
            foreach (var taskPack in group.ProblemPackList)
            {
                result.Add(group.GetPackResult(taskPack).OrderByDescending(r => r.TotalPoints).ToList());
            }
            return result;
        }

        public static List<ProblemPackResult> GetPackResult(this StudyGroup group, ProblemPackInfo taskPack)
        {
            var allPackResult = new List<ProblemPackResult>();
            foreach (var user in group.UserList)
            {
                allPackResult.Add(new ProblemPackResult(user, taskPack));
            }
            return allPackResult;
        }
    }
}