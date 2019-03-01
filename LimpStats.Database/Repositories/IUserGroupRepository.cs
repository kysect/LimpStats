using System.Collections.Generic;
using LimpStats.Model;

namespace LimpStats.Database.Repositories
{
    public interface IUserGroupRepository
    {
        void Create(UserGroup userGroup);
        UserGroup Read(string title);
        void Update(UserGroup userGroup);
        void Delete(UserGroup userGroup);
        void DeleteAll();
        List<UserGroup> ReadAll();
    }
}