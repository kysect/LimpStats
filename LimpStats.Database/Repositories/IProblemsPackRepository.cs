using LimpStats.Model.Problems;

namespace LimpStats.Database.Repositories
{
    public interface IProblemsPackRepository
    {
        void Create(string userGroupTitle, ProblemsPack problemsPack);
        ProblemsPack Read(string userGroupTitle, string title);
        void Update(string userGroupTitle, ProblemsPack problemsPack);
        void Delete(string userGroupTitle, ProblemsPack problemsPack);
    }
}