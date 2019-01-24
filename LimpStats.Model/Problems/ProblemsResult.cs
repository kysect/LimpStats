using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class ProblemsResult
    {
        public static ProblemsResult Create(ProblemsPack problems, LimpUser user)
        {
            List<int> result = problems
                .Problems
                .Select(problem => problem.GetUserResult(user))
                .ToList();

            return new ProblemsResult()
            {
                Points =  result
            };
        }

        public List<int> Points { get; set; }
        public int SumOfPoint => Points.Sum();
    }
}