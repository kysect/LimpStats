using System.Collections.Generic;
using System.Linq;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.Models
{
    public class ProfilePreviewData
    {
        public string Username { get; }
        public int Points { get; }

        public ProfilePreviewData(string username, int points)
        {
            Username = username;
            Points = points;
        }

        public override string ToString()
        {
            return $"{Username} [{Points}]";
        }

        public static IEnumerable<ProfilePreviewData> GetProfilePreview(UserGroup group)
        {
            return group
                .GetTotalPoints()
                .Select(res => new ProfilePreviewData(res.Username, res.Points));
        }
        public static IEnumerable<ProfilePreviewData> GetProfilePackPreview(UserGroup group, string packTitle)
        {
            ProblemsPack pack = group.ProblemsPacks.Find(e => e.Title == packTitle);
            return group.Users
                   .Select(user => new ProfilePreviewData(user.Username, pack.GetResults(user).SumOfPoint));
        }
    }
}