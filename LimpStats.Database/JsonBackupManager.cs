using System.Collections.Generic;
using System.IO;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database
{
    public static class JsonBackupManager
    {
        private const string FilePath = "E:\\coding\\summer_school_bot\\E-olymp_Parser\\Info.json";
        private const string FilePathUserGroup = "Cards.json";

        public static void SaveToJson(List<ElimpUser> users)
        {
            var jsonString = JsonConvert.SerializeObject(users);
            File.WriteAllText(FilePath, jsonString);
        }
        public static void SaveToJsonOne(ElimpUser newuser, int id)
        {
            if (File.Exists(FilePath))
            {
                var jsonString = File.ReadAllText(FilePath);
                var userList = JsonConvert.DeserializeObject<List<ElimpUser>>(jsonString);


                var user = userList.Find(e => e.Login == newuser.Login);
                if (user != null)
                {
                    userList.Remove(user);
                    user.GridConteinsId.Add(id);
                    userList.Add(user);
                }
                else
                {
                    userList = new List<ElimpUser>();
                    newuser.GridConteinsId.Add(id);
                    userList.Add(newuser);
                }

                jsonString = JsonConvert.SerializeObject(userList);
                File.WriteAllText(FilePath, jsonString);
            }
            else
            {
                var userList = new List<ElimpUser>();
                newuser.GridConteinsId.Add(id);
                userList.Add(newuser);
                string jsonString = JsonConvert.SerializeObject(userList);
                File.WriteAllText(FilePath, jsonString);
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