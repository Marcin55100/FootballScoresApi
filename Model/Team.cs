namespace FootballScoresApi.Model
{
    public class Team
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Team(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
