using System.Collections.Generic;
using System.Linq;

namespace ElimpParse.Model
{
    public class UserGroupInfo
    {
        public List<ElimpUser> UserList { get; }
        public List<TaskGroupInfo> TaskPackList { get; }

        public UserGroupInfo(List<ElimpUser> userList)
        {
            UserList = userList;
            TaskPackList = new List<TaskGroupInfo>();
        }

        public UserGroupInfo(List<string> usernameList)
        {
            TaskPackList = new List<TaskGroupInfo>();
            UserList = new List<ElimpUser>(usernameList.Count);
            foreach (var username in usernameList)
            {
                UserList.Add(new ElimpUser(username));
            }
        }
        
        public List<(ElimpUser user, int count)> GetEachUserTaskCount()
        {
            List<(ElimpUser user, int count)> result = new List<(ElimpUser user, int count)>();
            foreach (var user in UserList)
            {
                result.Add((user, user.CompletedTaskCount));
            }
            return result;
        }
        
    }
}