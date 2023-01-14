namespace FootballScoresApi.Model
{
    public class TeamData
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }

        public TeamData(string name, int points)
        {
            Name = name;
            Points = points;
        }

        public TeamData(string name, int rank, int points)
        {
            Name = name;
            Rank = rank;
            Points = points;
        }
    }
}
