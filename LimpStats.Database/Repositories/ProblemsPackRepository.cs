using System;
using System.Linq;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Database.Repositories
{
    internal class ProblemsPackRepository : IProblemsPackRepository
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public ProblemsPackRepository(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public void Create(string userGroupTitle, ProblemsPack problemsPack)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);

            if (userGroup.ProblemsPacks.Any(p => p.Title == problemsPack.Title))
            {
                throw new LimpStatsException("Pack with title already exist");
            }

            userGroup.ProblemsPacks.Add(problemsPack);
            _userGroupRepository.Update(userGroup);
        }

        public ProblemsPack Read(string userGroupTitle, string title)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);
            return userGroup.ProblemsPacks.Single(p => p.Title == title);
        }

        public void Update(string userGroupTitle, ProblemsPack problemsPack)
        {
            Delete(userGroupTitle, problemsPack);
            Create(userGroupTitle, problemsPack);
        }

        public void Delete(string userGroupTitle, ProblemsPack problemsPack)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);

            int removedElementCount = userGroup.ProblemsPacks.RemoveAll(p => p.Title == problemsPack.Title);
            if (removedElementCount == 0)
            {
                throw new LimpStatsException("Group not found in json");
            }

            _userGroupRepository.Update(userGroup);
        }
        public void DeleteProblem(string userGroupTitle, ProblemsPack problemsPack, Problem problem)
        {
            UserGroup userGroup = _userGroupRepository.Read(userGroupTitle);

            userGroup.ProblemsPacks.Find(e => e.Title == problemsPack.Title).Problems.RemoveAll(e => e.Title == problem.Title);
            _userGroupRepository.Update(userGroup);
        }
    }
}