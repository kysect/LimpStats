using System.Collections.Generic;
using System.IO;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database
{
    public static class JsonBackupManager
    {
        private const string FilePath = @"Info.json";
        private const string FilePathUserGroup = "Cards.json";

        private static void CheckFileExist(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                using (FileStream file = File.Create(filePath))
                {
                    file.Close();
                }
            }
        }
    
        public static void SaveToJson(List<ElimpUser> users)
        {
            CheckFileExist(FilePath);

            string jsonString = JsonConvert.SerializeObject(users);
            File.WriteAllText(FilePath, jsonString);
        }

        public static void SaveToJsonOne(ElimpUser newUser, int id)
        {
            CheckFileExist(FilePath);

            string jsonData = File.ReadAllText(FilePath);
            List<ElimpUser> userList = JsonConvert.DeserializeObject<List<ElimpUser>>(jsonData) ?? new List<ElimpUser>();
            ElimpUser user = userList.Find(e => e.Login == newUser.Login);

            if (user != null)
            {
                userList.Remove(user);
                user.GridConteinsId.Add(id);
                userList.Add(user);
            }
            else
            {
                newUser.GridConteinsId.Add(id);
                userList.Add(newUser);
            }

            jsonData = JsonConvert.SerializeObject(userList);
            File.WriteAllText(FilePath, jsonData);
        }

        public static List<ElimpUser> LoadFromJson()
        {
            CheckFileExist(FilePath);

            string jsonString = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<ElimpUser>>(jsonString) ?? new List<ElimpUser>();
        }
     }
}