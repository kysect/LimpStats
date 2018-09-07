using System.Collections.Generic;
using System.Linq;
using LimpStats.Model;
using  LimpStats.Database;
using LimpStats.Database.Models;

namespace LimpStats.Client.Services
{
    public static class InstanceGenerator
    {
        public static StudyGroup GenerateTemplateGroup(int groupid)
        {
           var a =  new StudyGroup(JsonBackupManager
                                .LoadFromJson()
                                .Where(e => e.GridConteinsId.Contains(groupid))
                                .OrderByDescending(user => user.UserProfileResult.Keys)
                                .ToList());
            a.ProblemPackList = new List<ProblemPackInfo>{new ProblemPackInfo("name", TaskPackStorage.TasksAGroup)};
            return a;
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