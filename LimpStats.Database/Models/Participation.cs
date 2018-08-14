using Dapper.Contrib.Extensions;

namespace LimpStats.Database.Models
{
    [Table("Participations")]
    public class Participation
    {
        public int ContestId { get; set; }
        public string Username { get; set; }
        public int Result { get; set; }
    }
}