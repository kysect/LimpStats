using System.Collections.Generic;
using System.Linq;
using LimpStats.Model.Problems;

namespace LimpStats.Model
{
    public class UserGroup
    {
        public UserGroup(string title = null)
        {
            Title = title;
            Users = new List<LimpUser>();
            ProblemsPacks = new List<ProblemsPack>();
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