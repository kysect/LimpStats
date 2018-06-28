using System.Collections.Generic;

namespace ElimpParse.Model
{
    public class StudyGroup
    {
        public StudyGroup()
        {
        }

        public StudyGroup(List<ElimpUser> userList)
        {
            UserList = userList;
            TaskPackList = new List<TaskGroupInfo>();
        }

        public StudyGroup(List<string> usernameList)
        {
            TaskPackList = new List<TaskGroupInfo>();
            UserList = new List<ElimpUser>(usernameList.Count);

            foreach (var username in usernameList) UserList.Add(new ElimpUser(username));
        }

        public List<ElimpUser> UserList { get; set; }
        public List<TaskGroupInfo> TaskPackList { get; set; }
    }
}