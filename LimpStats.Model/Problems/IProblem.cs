namespace LimpStats.Model.Problems
{
    public interface IProblem
    {
        string Title { get; }
        ProblemType Type { get; }
        int GetUserResult(LimpUser user);
    }
}