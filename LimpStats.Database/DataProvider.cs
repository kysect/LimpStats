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

        public static IUserGroupRepository UserGroupRepository { get; }
        public static IProblemsPackRepository ProblemsPackRepository { get; }

        public static void ClearCache()
        {
            UserGroupRepository.DeleteAll();
        }
    }
}