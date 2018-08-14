using Dapper.Contrib.Extensions;

namespace LimpStats.Database.Models
{
    [Table("UserResults")]
    public class UserResult
    {
        [Key]
        public string Username { get; set; }
        public int SolvedProblemCount { get; set; }
    }
}