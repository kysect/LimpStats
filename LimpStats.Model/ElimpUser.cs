using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
{
    public class ElimpUser
    {
        public ElimpUser() : this(null, null)
        {
        }

        public ElimpUser(string login, string name=null)
        {
            Login = login;
            Name = login;
            UserProfileResult = new Dictionary<int, int>(10000);
            for (int i = 0; i < 10000; i++)
            {
                UserProfileResult.Add(i, 0);
            }
            GridConteinsId = new List<int>();
        }


        public string Login { get; set; }
        public string Name { get; set; }
        public List<int> GridConteinsId { get; set; }
        public Dictionary<int, int> UserProfileResult { get; set; }

        public int CompletedTaskCount()
        {
            return UserProfileResult.Count(t => t.Value == 100);
        }

        #region inheritance

        public override string ToString()
        {
            return $"{Login}";
        }

        public override int GetHashCode()
        {
            return Login.GetHashCode();
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj is ElimpUser user) return user.Login == Login;
        //    return false;
        //}

        #endregion
    }
}