using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimpStats.Model;

namespace LimpStats.Core.Parsers
{
    public static class MultiThreadParser
    {
        private const int MaxDepthLevel = 15;

        public static void LoadProfiles(this StudyGroup group)
        {
            void TryLoad(ElimpUser user, int depthLevel)
            {
                depthLevel++;
                if (depthLevel == MaxDepthLevel)
                {
                    throw new ParserException($"Can't load {user.Username}");
                }

                try
                {
                    Parser.LoadProfileData(user);
                }
                catch (ParserException)
                {
                    throw;
                }
                catch
                {
                    TryLoad(user, depthLevel);
                }
            }

            Parallel.ForEach(group.UserList, u => TryLoad(u, 0));
        }

        public static List<(ElimpUser, int)> LoadSolutionCount(IEnumerable<ElimpUser> userList)
        {
            (ElimpUser, int) RecursiveExecute(ElimpUser user, int depthLevel)
            {
                depthLevel++;
                if (depthLevel == MaxDepthLevel)
                {
                    throw new ParserException($"Can't parse {user.Username}");
                }

                try
                {
                    return (user, Parser.LoadSolutionCount(user.Username));
                }
                catch (ParserException)
                {
                    throw;
                }
                catch
                {
                    return RecursiveExecute(user, depthLevel);
                }
            }

            return userList.AsParallel().Select(RecursiveExecute).ToList();
        }
    }
}