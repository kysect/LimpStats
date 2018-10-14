using System.Collections.Generic;

namespace LimpStats.Model
{
    public class StudyGroup
    {
        public StudyGroup()
        {

        }

        public StudyGroup(List<LimpUser> userList)
        {
            UserList = userList;
            ProblemPackList = new List<ProblemPackInfo>();
        }

        public List<LimpUser> UserList { get; set; }
        public List<ProblemPackInfo> ProblemPackList { get; set; }
    }
}