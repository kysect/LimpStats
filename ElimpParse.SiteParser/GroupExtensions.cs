using System.Collections.Generic;
using System.Linq;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class GroupExtensions
    {
        public static List<(ElimpUser user, int count)> GetEachUserTaskCount(this StudyGroup group)
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