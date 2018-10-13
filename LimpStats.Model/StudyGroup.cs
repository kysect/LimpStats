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

        public List<ElimpUser> UserList { get; set; }
        public List<ProblemPackInfo> ProblemPackList { get; set; }
    }
}