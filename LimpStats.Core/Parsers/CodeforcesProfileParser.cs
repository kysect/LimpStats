using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesApiWrapper;
using CodeforcesApiWrapper.Types;
using LimpStats.Model;

namespace LimpStats.Core.Parsers
{
    public class CodeforcesProfileParser : IProblemParser
    {
        private static readonly Codeforces CodeforcesClient = new Codeforces();

        public bool IsUserExist(string username)
        {
            //TODO: implement
            return true;
        }

        public string GetProblemTitle(string problemIdentifier)
        {
            int contestId = Int32.Parse(problemIdentifier.Remove(problemIdentifier.Length - 1));
            string letter = problemIdentifier[problemIdentifier.Length - 1].ToString();

            return CodeforcesClient
                .Contest
                .Standings(contestId)
                .Result
                .Result
                .Problems
                .Find(e => e.Index == letter)
                .Name;
        }

        public void LoadUserData(LimpUser user)
        {
            ResponseContainer<List<Submission>> response = CodeforcesClient
                .User
                .Status(user.CodeforcesHandle)
                .Result;

            response.ThrowExceptionIfFailed();

            IEnumerable<Submission> okSubmission = response
                .Result
                .Where(s => s.Verdict == SubmissionVerdictEnum.Ok);

            IEnumerable<string> solvedProblems = okSubmission
                .Select(s => $"{s.Problem.ContestId}{s.Problem.Index}")
                .Distinct();

            user.CodeforcesSubmissions = solvedProblems.ToList();
        }
    }

    public static class CodeforcesParserExtensions
    {
        public static void ThrowExceptionIfFailed<T>(this ResponseContainer<T> response)
        {
            if (response.Status == "FAILED")
            {
                throw new LimpStatsException(response.Comment);
            }
        }
    }
}
