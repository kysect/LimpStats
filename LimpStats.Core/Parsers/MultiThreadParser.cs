using System.Diagnostics;
using System.Threading.Tasks;
using LimpStats.Model;

namespace LimpStats.Core.Parsers
{
    public static class MultiThreadParser
    {
        private const int MaxRequestPerUserCount = 15;

        public static void LoadProfiles(UserGroup group)
        {
            var elimpParser = new ElimpParser();
            var cfParser = new CodeforcesProfileParser();
            Parallel.ForEach(group.Users, u =>
            {
                TryLoad(u, elimpParser);
                TryLoad(u, cfParser);
            });
        }

        private static void TryLoad(LimpUser user, IProblemParser parser)
        {
            for (var i = 0; i < MaxRequestPerUserCount; i++)
            {
                try
                {
                    parser.LoadUserData(user);
                    return;
                }
                catch (ParserException)
                {
                    Debug.WriteLine($"Failed for {user.Username}");
                }
            }
            throw new ParserException($"Can't load user data for {user.Username}");
        }
    }
}