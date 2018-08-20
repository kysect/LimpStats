namespace LimpStats.Client.Models
{
    public class GridCard
    {
        public GridCard(string name, string nickname, int totalPoints)
        {
            Name = name;
            Nickname = nickname;
            TotalPoints = totalPoints;
        }

        public string Name { get; }
        public string Nickname { get; }
        public int TotalPoints { get; }
    }
}