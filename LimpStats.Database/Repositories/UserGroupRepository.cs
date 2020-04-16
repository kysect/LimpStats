using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database.Repositories
{
    internal class UserGroupRepository : IUserGroupRepository
    {
        private const string FilePath = "UserGroup.json";

        public void Generate()
        {
            string s = File.ReadAllText("DataTemplate.json");
            File.WriteAllText(FilePath, s);
        }

        public void Create(UserGroup userGroup)
        {
            List<UserGroup> groups = ReadAll();
            if (groups.Any(g => g.Title == userGroup.Title))
            {
                throw new LimpStatsException("Group with title already exist");
            }

            groups.Add(userGroup);
            WriteToJson(groups);
        }

        public UserGroup Read(string title)
        {
            return ReadAll().FirstOrDefault(userGroup => userGroup.Title == title);
        }

        public void Update(UserGroup userGroup)
        {
            Delete(userGroup);
            Create(userGroup);
        }

        public void Delete(UserGroup userGroup)
        {
            List<UserGroup> groups = ReadAll();

            int removedElementCount = groups.RemoveAll(g => g.Title == userGroup.Title);
            if (removedElementCount == 0)
            {
                throw new Exception("Group not found in json");
            }

            WriteToJson(groups);
        }

        public void DeleteAll()
        {
            File.Delete(FilePath);
        }

        public List<UserGroup> ReadAll()
        {
            if (File.Exists(FilePath) == false)
            {
                File.Create(FilePath).Close();
            }

            string jsonData = File.ReadAllText(FilePath);
            List<UserGroup> userGroups =
                JsonConvert.DeserializeObject<List<UserGroup>>(jsonData) ?? new List<UserGroup>();
            return userGroups;
        }

        private void WriteToJson(List<UserGroup> userGroups)
        {
            string jsonData = JsonConvert.SerializeObject(userGroups);
            File.WriteAllText(FilePath, jsonData);
        }
    }
}