using LimpStats.Database.Repositories;

namespace LimpStats.Database
{
    public static class DataProvider
    {
        static DataProvider()
        {
            UserGroupRepository = new UserGroupRepository();
            ProblemsPackRepository = new ProblemsPackRepository(UserGroupRepository);
        }

        public static UserGroupRepository UserGroupRepository { get; }
        public static ProblemsPackRepository ProblemsPackRepository { get; }
    }
}