using System.ComponentModel.DataAnnotations.Schema;

namespace ElimpParse.DatabaseProvider.Models
{
    [Table("Participations")]
    public class Participation
    {
        public int ContestId { get; set; }
        public string Username { get; set; }
        public int Result { get; set; }
    }
}