using System;
using System.Collections.Generic;
using System.Linq;
using CodeforcesApiWrapper;
using CodeforcesApiWrapper.Types;

namespace LimpStats.Core.CodeforcesParser
{
    public static class CodeforcesProfileParser
    {
        private static readonly Codeforces CodeforcesClient = new Codeforces();

        public static List<string> GetUserSolvedProblem(string handle)
        {
            ResponseContainer<List<Submission>> response = CodeforcesClient
                .User
                .Status(handle)
                .Result;

            response.ThrowExceptionIfFailed();

            IEnumerable<Submission> okSubmission = response
                .Result
                .Where(s => s.Verdict == SubmissionVerdictEnum.Ok);

            IEnumerable<string> solvedProblems = okSubmission
                .Select(s => $"{s.Problem.ContestId}{s.Problem.Index}")
                .Distinct();

            return solvedProblems.ToList();
        }

        private static void ThrowExceptionIfFailed<T>(this ResponseContainer<T> response)
        {
            if (response.Status == "FAILED")
            {
                throw new Exception(response.Comment);
            }
        }
    }
}
