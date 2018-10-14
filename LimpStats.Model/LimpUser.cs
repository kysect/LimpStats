using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
{
    public class LimpUser
    {
        public LimpUser(string username)
        {
            Username = username;
            UserProfileResult = new Dictionary<int, int>(10000);
            for (int i = 0; i < 10000; i++)
            {
                UserProfileResult.Add(i, 0);
            }
        }


        public string Username { get; set; }
        public Dictionary<int, int> UserProfileResult { get; set; }

        public int CompletedTaskCount()
        {
            return UserProfileResult.Count(t => t.Value == 100);
        }

        #region inheritance

        public override string ToString()
        {
            return $"{Username}";
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }

        #endregion
    }
}