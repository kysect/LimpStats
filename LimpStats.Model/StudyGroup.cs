using System.Collections.Generic;

namespace LimpStats.Model
{
    public class StudyGroup
    {
        public StudyGroup()
        {
        }

        public StudyGroup(List<ElimpUser> userList)
        {
            UserList = userList;
            ProblemPackList = new List<ProblemPackInfo>();
        }

        public StudyGroup(List<string> usernameList)
        {
            UserList = new List<ElimpUser>(usernameList.Count);
            ProblemPackList = new List<ProblemPackInfo>();

            foreach (var username in usernameList)
            {
                UserList.Add(new ElimpUser(username));
            }
        }

        public List<ElimpUser> UserList { get; set; }
        public List<ProblemPackInfo> ProblemPackList { get; set; }
    }
}