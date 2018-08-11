using System.Collections.Generic;
using System.Linq;

namespace ElimpParse.Model
{
    public class ProblemPackResult
    {
        public ProblemPackResult()
        {
        }

        public ProblemPackResult(ElimpUser user, ProblemPackInfo problemPack)
        {
            Username = user.Login;
            Problems = problemPack;

            var resultList = new List<int>();
            foreach (var taskId in Problems.ProblemIdList)
            {
                resultList.Add(user.UserProfileResult[taskId]);
            }

            ProblemResultList = resultList;
        }

        //TODO: возможно заменить ElimpUser на Username
        public string Username { get; set; }
        public List<int> ProblemResultList { get; set; }
        public ProblemPackInfo Problems { get; set; }
        public int AdditionalPoints => ProblemResultList.All(res => res == 100) ? Problems.FullSolutionPoints : 0;
        public int TotalPoints => ProblemResultList.Sum() + (AdditionalPoints);
    }
}