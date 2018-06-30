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

        public static List<List<UserPackResult>> GetAllPackResult(this StudyGroup group)
        {
            var result = new List<List<UserPackResult>>();
            foreach (var taskPack in group.TaskPackList) result.Add(group.GetPackResult(taskPack));

            return result;
        }

        public static List<UserPackResult> GetPackResult(this StudyGroup group, TaskGroupInfo taskPack)
        {
            var allPackResult = new List<UserPackResult>();
            foreach (var user in group.UserList)
            {
                var userResult = new List<int>();
                foreach (var taskId in taskPack.TaskIdList) userResult.Add(user.UserProfileResult[taskId]);
                var isFull = userResult.All(res => res == 100);

                allPackResult.Add(new UserPackResult(user, userResult, isFull));
            }

            return allPackResult;
        }
    }
}