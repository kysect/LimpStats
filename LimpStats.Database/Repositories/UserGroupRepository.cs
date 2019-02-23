using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using LimpStats.Model;
using Newtonsoft.Json;

namespace LimpStats.Database.Repositories
{
    public class UserGroupRepository
    {
        private const string FilePath = "UserGroup.json";

        public void Create(UserGroup userGroup)
        {
            List<UserGroup> groups = ReadAll();
            if (groups.Any(g => g.Title == userGroup.Title))
            {
                throw new Exception("Group with title already exist");
            }

            groups.Add(userGroup);
            WriteToJson(groups);
        }

        public UserGroup Read(string title)
        {
            var groups = ReadAll();
            //TODO знаю что дичь, пока так
            if(groups.Capacity == 0)
                return null;
            return ReadAll().First(userGroup => userGroup.Title == title);
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