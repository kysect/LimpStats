using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElimpParse.Model;

namespace ElimpParse.Core
{
    public static class MultiThreadParser
    {
        public static void LoadProfiles(IEnumerable<ElimpUser> userList)
        {
            void TryLoad(ElimpUser user, Action<ElimpUser> action)
            {
                try
                {
                    action.Invoke(user);
                }
                catch
                {
                    TryLoad(user, action);
                }
            }

            Parallel.ForEach(userList, u => TryLoad(u, Parser.LoadUserData));
        }

        public static List<(ElimpUser, int)> LoadProblemsCount(IEnumerable<ElimpUser> userList)
        {
            (ElimpUser, int) RecursiveExecute(ElimpUser user)
            {
                try
                {
                    return (user, Parser.CompletedTaskCount(user.Login));
                }
                catch
                {
                    return RecursiveExecute(user);
                }
            }

            return userList.AsParallel().Select(RecursiveExecute).ToList();
        }
    }
}