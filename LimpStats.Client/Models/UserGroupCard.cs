using System.Collections.Generic;
using LimpStats.Model;

namespace LimpStats.Client.Models
{
    public class UserGroupCard
    {
        public UserGroupCard(string name, List<ProblemPackInfo> problemPacks, List<ElimpUser> usersInGroup)
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
        public List<ProblemPackInfo> ProblemPacks { get; }
        public List<ElimpUser> UsersInGroup { get; }
    }
}
