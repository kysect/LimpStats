using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class ProblemPackResult
    {
        public static ProblemPackResult Create(ProblemsPack problems, LimpUser user)
        {
            List<int> result = problems
                .Problems
                .Select(problem => problem.GetUserResult(user))
                .ToList();

            return new ProblemPackResult()
            {
                Points =  result,
                Username = user.Username
            };
        }

        public string Username { get; set; }
        public List<int> Points { get; set; }
        public int SumOfPoint => Points.Sum();
    }
}