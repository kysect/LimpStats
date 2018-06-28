using System;
using System.Collections.Generic;
using System.Linq;

namespace ElimpParse.Model
{
    public class UserProfileResult
    {
        //TODO: 0 - 100 value for contest (convert to list<int>)
        public Dictionary<int, bool> TaskResultList;

        public UserProfileResult(Dictionary<int, bool> taskResultList)
        {
            TaskResultList = taskResultList;
        }

        public int CompletedTaskCount => TaskResultList.Count(t => t.Value);

        //public bool GetTaskResult(int taskId)
        //{
        //    return TaskResultList[taskId];
        //}
    }
}