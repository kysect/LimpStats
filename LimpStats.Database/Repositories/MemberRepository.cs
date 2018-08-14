using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;
using LimpStats.Database.Models;

namespace LimpStats.Database.Repositories
{
    public class MemberRepository
    {
        private readonly string _connectionString;
        public MemberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Member member)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Insert(member);
            }
        }

        public IEnumerable<Member> Read()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .ExecuteReader("SELECT * FROM Members")
                    .Parse<Member>()
                    .ToList();
            }
        }

        public IEnumerable<Member> ReadByGroup(string group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .ExecuteReader("SELECT * FROM Members"
                                   + $" WHERE [Group]='{group}'")
                    .Parse<Member>()
                    .ToList();
            }
        }

        public void Delete(Member member)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection
                    .Execute("DELETE FROM Members"
                             + $" WHERE [Group]='{member.Group}' AND [Username]='{member.Username}'");
            }
        }
    }
}