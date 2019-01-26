using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class ProblemsPack
    {
        public ProblemsPack(string title = null, List<Problem> problems = null)
        {
            Title = title;
            Problems = problems ?? new List<Problem>();
        }

        public List<Problem> Problems { get; set; }
        public string Title { get; set; }

        public ProblemPackResult GetResults(LimpUser user)
        {
            return ProblemPackResult.Create(this, user);
        }

        public List<ProblemPackResult> GetResults(List<LimpUser> users)
        {
            return users.Select(GetResults).ToList();
        }
    }
}