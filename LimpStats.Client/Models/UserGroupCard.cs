using System.Collections.Generic;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.Models
{
    public class UserGroupCard
    {
        public UserGroupCard(string name, List<ProblemsPack> problemPacks, List<LimpUser> usersInGroup)
        {
            Name = name;
            ProblemPacks = problemPacks;
            UsersInGroup = usersInGroup;
        }

        public UserGroupCard(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public List<ProblemsPack> ProblemPacks { get; }
        public List<LimpUser> UsersInGroup { get; }
    }
}
