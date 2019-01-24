namespace LimpStats.Model.Problems
{
    public class EOlympProblem : IProblem
    {
        private readonly int _problemNumber;
        public EOlympProblem(int problemNumber)
        {
            _problemNumber = problemNumber;
        }

        public string Title()
        {
            return _problemNumber.ToString();
        }

        public ProblemType Type()
        {
            return ProblemType.EOlymp;
        }

        public int GetUserResult(LimpUser user)
        {
            user.EOlimpProblemsResult.TryGetValue(_problemNumber, out int points);
            return points;
        }
    }
}