namespace LimpStats.Client.Models
{
    public class ProfilePreviewData
    {
        public string Username { get; }
        public int Points { get; }
        public ProfilePreviewData(string username, int points)
        {
            Username = username;
            Points = points;
        }

        public override string ToString()
        {
            return $"{Username} [{Points}]";
        }
    }
}