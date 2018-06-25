using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace ElimpParse
{
    public static class BackUpManager
    {
        static string filepath = "Info.json";
        public static void SaveJson(List <ElimpUser> users)
        {
            var s = JsonConvert.SerializeObject(users);
            Console.WriteLine(s);
            File.Delete(filepath);
            StreamWriter file = new StreamWriter(filepath);
            file.WriteLine(s);
            file.Close();
        }
        public static List<ElimpUser> LoadJson()
        {
            string s = File.ReadAllText(filepath);
            var list = JsonConvert.DeserializeObject<List<ElimpUser>>(s);
            return list;
        }
    }
}
