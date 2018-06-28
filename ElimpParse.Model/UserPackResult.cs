using System.Collections.Generic;

namespace ElimpParse.Model
{
    public class UserPackResult
    {
        public UserPackResult()
        {
        }

        public UserPackResult(ElimpUser user, List<int> taskResultList, bool isFullCorrect)
        {
            User = user;
            TaskResultList = taskResultList;
            IsFullCorrect = isFullCorrect;
        }

        public ElimpUser User { get; set; }
        public List<int> TaskResultList { get; set; }
        public bool IsFullCorrect { get; set; }
    }
}