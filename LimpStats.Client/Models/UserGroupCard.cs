using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimpStats.Model;

namespace LimpStats.Client.Models
{
    class UserGroupCard
    {
        public UserGroupCard(string name, List<ProblemPackInfo> problemPacks, List<UserCard> usersInGroup)
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
        public List<UserCard> UsersInGroup { get; }
    }
}
