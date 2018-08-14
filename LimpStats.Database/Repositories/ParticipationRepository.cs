using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using LimpStats.Database.Models;

namespace LimpStats.Database.Repositories
{
    public class ParticipationRepository
    {
        private readonly string _connectionString;
        public ParticipationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Insert(userResult);
            }
        }

        public UserResult Read(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Get<UserResult>(username);
            }
        }

        public void Update(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Update(userResult);
            }
        }

        public void Delete(UserResult userResult)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Delete(userResult);
            }
        }
    }
}