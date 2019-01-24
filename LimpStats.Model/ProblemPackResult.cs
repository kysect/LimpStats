using System;
using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
{
    //TODO: remove
    public class ProblemPackResult
    {
        public ProblemPackResult(LimpUser user, ProblemPackInfo problemPack)
        {
            Username = user.Username;
            Problems = problemPack;


            var resultList = new List<int>();
            foreach (int taskId in Problems.ProblemIdList)
            {
                resultList.Add(user.EOlimpProblemsResult.ContainsKey(taskId) ? user.EOlimpProblemsResult[taskId] : 0);
            }

            ProblemResultList = resultList;
        }

        public string Username { get; }
        public List<int> ProblemResultList { get; }
        public ProblemPackInfo Problems { get; set; }

        public int TotalPoints => ProblemResultList.Sum();
    }
}