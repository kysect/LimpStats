using System;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Core.Parsers
{
    public interface IProblemParser
    {
        bool IsUserExist(string username);
        string GetProblemTitle(string problemIdentifier);
        void LoadUserData(LimpUser user);
    }

    public static class ProblemParserExtensions
    {
        public static IProblemParser GetForDomain(Domain domain)
        {
            switch (domain)
            {
                case Domain.EOlymp:
                    return new ElimpParser();
                case Domain.Codeforces:
                    return new CodeforcesProfileParser();
                default:
                    throw new ArgumentOutOfRangeException(nameof(domain), domain, null);
            }
        }
    }
}