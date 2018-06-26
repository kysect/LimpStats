using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ElimpParse
{
    public class ElimpUser
    {
        public string Login { get; set; }
        public string Title { get; set; }
        public TaskPack TaskPack { get; set; }
        public int CompletedTaskCount { get; set; }

        public ElimpUser()
        {

        }

        public ElimpUser(string login)
        {
            Login = login;
        }

        public ElimpUser(string login, string title)
        {
            Login = login;
            Title = title;
        }
    }
}