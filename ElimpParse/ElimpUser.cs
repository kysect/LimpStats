using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ElimpParse
{
    public class ElimpUser
    {
        public string Login { get; }
        public TaskPack TaskPack { get; private set; }
        public int ComplitedTaskCount { get; private set; }

        public ElimpUser(string login)
        {
            Login = login;
        }

        public void LoadFromWeb()
        {
            TaskPack = Parser.GetUserTaskList(Login);
            ComplitedTaskCount = Parser.ComplitedTaskCount(Login);
        }

        public void LoadFromJson()
        {
            var data = File.ReadAllText($"data_{Login}.json");
            var obj = JsonConvert.DeserializeObject<ElimpUser>(data);

            TaskPack = obj.TaskPack;
            ComplitedTaskCount = obj.ComplitedTaskCount;

        }

        public void SaveToJson()
        {
            File.WriteAllText($"data_{Login}.json", JsonConvert.SerializeObject(this));
        }

        public string GetResult(List<int> taskList)
        {
            var result = taskList.Select(t => TaskPack.GetTaskResult(t));
            return string.Join(" ", result);
        }

    }
}