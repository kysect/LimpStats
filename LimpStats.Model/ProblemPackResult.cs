using System;
using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
{
    public class ProblemPackResult
    {
        public ProblemPackResult(ElimpUser user, ProblemPackInfo problemPack)
        {
            Username = user.Username;
            Problems = problemPack;
            if (user.UserProfileResult.Count == 0)
            {
                throw new Exception("Profile wasn't loaded");
            }


            var resultList = new List<int>();
            foreach (var taskId in Problems.ProblemIdList)
            {
                resultList.Add(user.UserProfileResult.ContainsKey(taskId) ? user.UserProfileResult[taskId] : 0);
            }

            ProblemResultList = resultList;
        }

        public string Username { get; }
        public List<int> ProblemResultList { get; }
        public ProblemPackInfo Problems { get; set; }
        public int AdditionalPoints => ProblemResultList.All(res => res == 100) ? Problems.FullSolutionPoints : 0;
        public int TotalPoints => ProblemResultList.Sum() + (AdditionalPoints);
    }
}