using Dapper.Contrib.Extensions;

namespace ElimpParse.DatabaseProvider.Models
{
    [Table("Members")]
    public class Member
    {
        public string Group { get; set; }
        public string Username { get; set; }
    }
}