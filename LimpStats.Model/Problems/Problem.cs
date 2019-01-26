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

        public string Title { get; set; }
        public ProblemType Type { get; set; }

        public int GetUserResult(LimpUser user)
        {
            if (Type == ProblemType.EOlymp)
            {
                user.EOlimpProblemsResult.TryGetValue(int.Parse(Title), out int points);
                return points;
            }

            if (Type == ProblemType.Codeforces)
            {
                return user.CodeforcesSubmissions.Contains(Title) ? 100 : 0;
            }

            throw new ArgumentException(nameof(user));
        }

        public static List<Problem> CreateEOlympFromList(IEnumerable<int> problemsNumber)
        {
            return problemsNumber.Select(num => new Problem()
            {
                Title = num.ToString(),
                Type = ProblemType.EOlymp
            }).ToList();
        }
    }
}