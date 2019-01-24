using System.Collections.Generic;
using LimpStats.Model.Problems;

namespace LimpStats.Model
{
    public class UserGroup
    {
        public UserGroup(string title) : this(title, new List<LimpUser>())
        {
        }

        public UserGroup(string title, List<LimpUser> users)
        {
            Title = title;
            Users = users;
            ProblemPacks = new List<ProblemsPack>();
        }

        public string Title { get; set; }
        public List<LimpUser> Users { get; set; }
        public List<ProblemsPack> ProblemPacks { get; set; }
    }
}