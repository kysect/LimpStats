using System;

namespace LimpStats.Model.Problems
{
    public enum Domain
    {
        EOlymp = 1,
        Codeforces = 2
    }

    public static class DomainExtensions
    {
        public static string ToUiString(this Domain domain)
        {
            switch (domain)
            {
                case Domain.EOlymp:
                    return "E-Olymp";
                case Domain.Codeforces:
                    return "Codeforces";
                default:
                    throw new ArgumentOutOfRangeException(nameof(domain), domain, null);
            }
        }

        public static Domain Parse(string domain)
        {
            if (!Enum.TryParse(domain, true, out Domain parsed))
                throw new LimpStatsException($"Unsupported Domain type: {domain}");

            return parsed;
        }
    }
}