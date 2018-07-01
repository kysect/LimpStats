using System.Collections.Generic;
using System.Linq;

namespace ElimpParse.Model
{
    public class ElimpUser
    {
        public ElimpUser() : this(null, null)
        {
        }

        public ElimpUser(string login, string title = null)
        {
            Login = login;
            Title = title;
            UserProfileResult = new Dictionary<int, int>();
        }

        public string Login { get; set; }
        public string Title { get; set; }
        public Dictionary<int, int> UserProfileResult { get; set; }

        public int CompletedTaskCount()
        {
            return UserProfileResult.Count(t => t.Value == 100);
        }

        public override string ToString()
        {
            return $"{Login}";
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ElimpUser user)
            {
                return user.Login == Login;
            }
            return false;
        }
    }
}