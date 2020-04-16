using System.Collections.Generic;
using LimpStats.Model;

namespace LimpStats.Database.Repositories
{
    public interface IUserGroupRepository
    {
        void Generate();
        void Create(UserGroup userGroup);
        List<UserGroup> ReadAll();
        UserGroup Read(string title);
        void Update(UserGroup userGroup);
        void Delete(UserGroup userGroup);
        void DeleteAll();
    }
}