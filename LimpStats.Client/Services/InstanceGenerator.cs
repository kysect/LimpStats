using System.Collections.Generic;
using System.Linq;
using LimpStats.Database;
using LimpStats.Model;

namespace LimpStats.Client.Services
{
    public static class InstanceGenerator
    {
        public static StudyGroup GenerateTemplateGroup(int groupId)
        {
            List<ElimpUser> studentList = JsonBackupManager
                .LoadFromJson()
                .Where(e => e.GridConteinsId.Contains(groupId))
                .OrderByDescending(user => user.UserProfileResult.Values.Sum())
                .ToList();
            return new StudyGroup(studentList)
            {
                ProblemPackList =
                    new List<ProblemPackInfo> {new ProblemPackInfo("name", TaskPackStorage.TasksAGroup)}
            };
        }

        public static class TaskPackStorage
        {
            public static readonly List<int> TasksAGroup = new List<int>
            {
                5133,
                7401
            };
        }
    }
}