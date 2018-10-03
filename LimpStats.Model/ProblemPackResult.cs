using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
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
            //TODO: загружать тут данные

            var resultList = new List<int>();
            foreach (var taskId in Problems.ProblemIdList)
            {
                resultList.Add(user.UserProfileResult[taskId]);
            }

            ProblemResultList = resultList;
        }

        public string Username { get; set; }
        public List<int> ProblemResultList { get; set; }
        public ProblemPackInfo Problems { get; set; }
        public int AdditionalPoints => ProblemResultList.All(res => res == 100) ? Problems.FullSolutionPoints : 0;
        public int TotalPoints => ProblemResultList.Sum() + (AdditionalPoints);
    }
}