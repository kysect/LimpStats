using System.Collections.Generic;
using System.Linq;
using LimpStats.Model.Problems;

namespace LimpStats.Model
{
    public class UserGroup
    {
        public UserGroup()
        {
        }

        public UserGroup(string title) : this(title, new List<LimpUser>(), new List<ProblemsPack>())
        {
        }


        public UserGroup(string title, List<LimpUser> users, List<ProblemsPack> problemsPacks)
        {
            Title = title;
            Users = users;
            ProblemsPacks = problemsPacks;
        }

        public string Title { get; set; }
        public List<LimpUser> Users { get; set; }
        public List<ProblemsPack> ProblemsPacks { get; set; }

        public List<(string Username, int Points)> GetTotalPoints()
        {
            var result = new List<(string username, int points)>();

            foreach (LimpUser user in Users)
            {
                int totalPoints = ProblemsPacks.Select(p => p.GetResults(user).SumOfPoint).Sum();
                result.Add((user.Username, totalPoints));
            }

            return result;
        }
    }
}