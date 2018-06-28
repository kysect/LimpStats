using System;
using System.Collections.Generic;
using System.IO;
using ElimpParse.Model;
using Newtonsoft.Json;

namespace ElimpParse.DatabaseProvider
{
    public static class JsonBackupManager
    {
        private const string FilePath = "Info.json";

        public static void SaveToJson(List<ElimpUser> users)
        {
            var jsonString = JsonConvert.SerializeObject(users);

#if DEBUG
            Console.WriteLine(jsonString);
#endif

            using (var streamWriter = new StreamWriter(FilePath, false))
            {
                streamWriter.WriteLine(jsonString);
            }
        }

        public static List<ElimpUser> LoadFromJson()
        {
            var jsonString = File.ReadAllText(FilePath);
            var userList = JsonConvert.DeserializeObject<List<ElimpUser>>(jsonString);
            return userList;
        }
    }
}