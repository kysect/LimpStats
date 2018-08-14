using Dapper.Contrib.Extensions;

namespace LimpStats.Database.Models
{
    [Table("Members")]
    public class Member
    {
        public string Group { get; set; }
        public string Username { get; set; }
    }
}