using System.Collections.Generic;

namespace LimpStats.Model
{
    public class StudyGroup
    {
        public StudyGroup()
        {
            UserList = new List<LimpUser>();
            ProblemPackList = new List<ProblemPackInfo>();
        }

        public StudyGroup(List<LimpUser> userList)
        {
            UserList = userList;
            ProblemPackList = new List<ProblemPackInfo>();
        }

        //TODO: remove?
        public string GroupTitle { get; set; }
        public List<LimpUser> UserList { get; set; }
        public List<ProblemPackInfo> ProblemPackList { get; set; }
    }
}