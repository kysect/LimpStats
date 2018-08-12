using Dapper.Contrib.Extensions;
using ElimpParse.Model;

namespace ElimpParse.DatabaseProvider.Models
{
    [Table("UserResults")]
    public class UserResult
    {
        [Key]
        public string Username { get; set; }
        public int SolvedProblemCount { get; set; }
    }
}