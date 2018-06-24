using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ElimpParse
{
    public class ElimpUser
    {
        public string Login { get; }
        public string Title { get; }
        public TaskPack TaskPack { get; private set; }
        public int ComplitedTaskCount { get; set; }

        public ElimpUser(string login)
        {
            Login = login;
        }

        public ElimpUser(string login, string title)
        {
            Login = login;
            Title = title;
        }

        public override string ToString()
        {
            if (Title != null)
            {

                return $"{Login} [{Title}] {ComplitedTaskCount}";

            }
            else
            {
                return $"{Login}{getspace(Login.Length)} |{ComplitedTaskCount}";
            }
            string getspace(int a)
            {
                string s = "";
                for (int i = 0; i < 20 - a; i++)
                    s += " ";
                return s;
            }
        }

        //public void LoadFromWeb()
        //{
        //    TaskPack = Parser.GetUserTaskList(Login);
        //    ComplitedTaskCount = Parser.ComplitedTaskCount(Login);
        //}

        //public void LoadFromJson()
        //{
        //    var data = File.ReadAllText($"data_{Login}.json");
        //    var obj = JsonConvert.DeserializeObject<ElimpUser>(data);

        //    TaskPack = obj.TaskPack;
        //    ComplitedTaskCount = obj.ComplitedTaskCount;

        //}

        //public void SaveToJson()
        //{
        //    File.WriteAllText($"data_{Login}.json", JsonConvert.SerializeObject(this));
        //}

    }
}