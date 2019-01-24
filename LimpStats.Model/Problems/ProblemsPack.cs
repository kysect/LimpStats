using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class ProblemsPack
    {
        public List<IProblem> Problems { get; set; } = new List<IProblem>();
        public string Title { get; set; }

        public ProblemsPack(string title)
        {
            Title = title;
        }

        public ProblemsResult GetResults(LimpUser user)
        {
            return ProblemsResult.Create(this, user);
        }

        public List<ProblemsResult> GetResults(List<LimpUser> users)
        {
            return users.Select(GetResults).ToList();
        }
    }
}