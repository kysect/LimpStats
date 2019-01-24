namespace LimpStats.Model.Problems
{
    public class CodeforcesProblem : IProblem
    {
        private readonly int _roundNumber;
        private readonly string _problemLetter;

        public CodeforcesProblem(int roundNumber, string problemLetter)
        {
            _roundNumber = roundNumber;
            _problemLetter = problemLetter;
        }

        private string ProblemTag => $"{_roundNumber}{_problemLetter}";

        public string Title()
        {
            return ProblemTag;
        }

        public ProblemType Type()
        {
            return ProblemType.Codeforces;
        }

        public int GetUserResult(LimpUser user)
        {
            if (user.CodeforcesSubmissions.Contains(ProblemTag))
                return 100;
            return 0;
        }
    }
}