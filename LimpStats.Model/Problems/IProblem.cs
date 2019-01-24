namespace LimpStats.Model.Problems
{
    public interface IProblem
    {
        string Title();
        ProblemType Type();
        int GetUserResult(LimpUser user);
    }
}