namespace LimpStats.Model.Problems
{
    public class CodeforcesProblem : IProblem
    {
        private readonly string _problemLetter;
        private readonly int _roundNumber;

        public CodeforcesProblem(int roundNumber, string problemLetter)
        {
            _roundNumber = roundNumber;
            _problemLetter = problemLetter;
        }

        public string Title => $"{_roundNumber}{_problemLetter}";

        public ProblemType Type => ProblemType.Codeforces;

        public int GetUserResult(LimpUser user)
        {
            return user.CodeforcesSubmissions.Contains(Title) ? 100 : 0;
        }
    }
}