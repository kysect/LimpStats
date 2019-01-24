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
            Parallel.ForEach(group.Users, TryLoad);
        }
        private static void TryLoad(LimpUser user)
        {
            for (var i = 0; i < MaxRequestPerUserCount; i++)
            {
                try
                {
                    Parser.LoadProfileData(user);
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