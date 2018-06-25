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

        //public TaskPack TaskPack { get; private set; }
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

        //public void LoadFromWeb()
        //{
        //    TaskPack = Parser.GetUserTaskList(Login);
        //    CompletedTaskCount = Parser.CompletedTaskCount(Login);
        //}

        //public void LoadFromJson()
        //{
        //    var data = File.ReadAllText($"data_{Login}.json");
        //    var obj = JsonConvert.DeserializeObject<ElimpUser>(data);

        //    TaskPack = obj.TaskPack;
        //    CompletedTaskCount = obj.CompletedTaskCount;

        //}

        //public void SaveToJson()
        //{
        //    File.WriteAllText($"data_{Login}.json", JsonConvert.SerializeObject(this));
        //}

    }
}