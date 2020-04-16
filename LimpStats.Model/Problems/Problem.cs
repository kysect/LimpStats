using System;
using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model.Problems
{
    public class Problem
    {
        public Problem()
        {
        }

        public Problem(string title, Domain type)
        {
            Title = title;
            Type = type;
        }

        public string Title { get; set; }
        public Domain Type { get; set; }

        public int GetUserResult(LimpUser user)
        {
            switch (Type)
            {
                case Domain.EOlymp:
                    //TODO: Handle case when problem was not found

                    user.EOlimpProblemsResult.TryGetValue(int.Parse(Title), out int points);
                    return points;

                case Domain.Codeforces:
                    return user.CodeforcesSubmissions.Contains(Title) ? 100 : 0;

                default:
                    throw new ArgumentException(nameof(user));
            }
        }

        public static List<Problem> CreateEOlympFromList(IEnumerable<int> problemsNumber)
        {
            return problemsNumber
                .Select(num => new Problem
                {
                    Title = num.ToString(),
                    Type = Domain.EOlymp
                })
                .ToList();
        }
    }
}