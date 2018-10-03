using System.Collections.Generic;
using System.IO;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database
{
    public static class JsonBackupManager
    {
        //TODO: fix path
        private const string FilePath = @"Info.json";
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
                var userList = JsonConvert.DeserializeObject<List<ElimpUser>>(jsonString) ?? new List<ElimpUser>();


                var user = userList.Find(e => e.Login == newuser.Login);
                if (user != null)
                {
                    userList.Remove(user);
                    user.GridConteinsId.Add(id);
                    userList.Add(user);
                }
                else
                {
//                    userList = new List<ElimpUser>();
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

                if (File.Exists(FilePath) == false)
                {
                    //TODO: Фиксить работу с файлами
                    File.Create(FilePath).Dispose();
                }

                File.WriteAllText(FilePath, jsonString);
            }

        }

        public static List<ElimpUser> LoadFromJson()
        {
            if (File.Exists(FilePath) == false)
            {
                return new List<ElimpUser>();
            }
            var jsonString = File.ReadAllText(FilePath);
            var userList = JsonConvert.DeserializeObject<List<ElimpUser>>(jsonString);
            return userList;
        }
     }
}