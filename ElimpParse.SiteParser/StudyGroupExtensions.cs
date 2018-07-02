using System.Collections.Generic;
using System.Linq;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class StudyGroupExtensions
    {
        public static void LoadStudentsResults(this StudyGroup group)
        {
            foreach (var user in group.UserList)
            {
                Parser.LoadUserData(user);
            }
        }

        public static void LoadStudentsResultMultiThread(this StudyGroup group)
        {
            MultiThreadParser.MakeMultiThreadExecute(group.UserList, Parser.LoadUserData);
        }

        public static List<(ElimpUser, int)> GetTaskCountMultiThread(this StudyGroup group)
        {
            return MultiThreadParser.GetCountParallel(group.UserList);
        }

        public static List<(ElimpUser user, int count)> LoadTaskCount(this StudyGroup group)
        {
            var result = new List<(ElimpUser user, int count)>();
            foreach (var user in group.UserList)
            {
                result.Add((user, Parser.CompletedTaskCount(user.Login)));
            }

            return result;
        }

        public static List<(ElimpUser user, int count)> GetTaskCount(this StudyGroup group)
        {
            var result = new List<(ElimpUser user, int count)>();
            foreach (var user in group.UserList) result.Add((user, user.CompletedTaskCount()));
            return result;
        }

        public static List<(ProblemPackInfo pack, List<ProblemPackResult> results)> GetAllPackResult(this StudyGroup group)
        {
            var result = new List<(ProblemPackInfo, List<ProblemPackResult>)>();
            foreach (var taskPack in group.ProblemPackList)
            {
                result.Add((taskPack, group.GetPackResult(taskPack).OrderByDescending(r => r.TotalPoints).ToList()));
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