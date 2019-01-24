using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class EOlympProblem : IProblem
    {
        private readonly int _problemNumber;

        public EOlympProblem(int problemNumber)
        {
            _problemNumber = problemNumber;
        }

        public static List<IProblem> CreateFromList(IEnumerable<int> problemsNumber)
        {
            return problemsNumber.Select(num => new EOlympProblem(num) as IProblem).ToList();
        }

        public string Title => _problemNumber.ToString();

        public ProblemType Type => ProblemType.EOlymp;

        public int GetUserResult(LimpUser user)
        {
            user.EOlimpProblemsResult.TryGetValue(_problemNumber, out int points);
            return points;
        }
    }
}