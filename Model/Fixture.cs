namespace FootballScoresApi.Model
{
    public class Fixture
    {
        public Fixture(string team, string opponent, bool isHome, DateTime date)
        {
            Team = team;
            Opponent = opponent;
            IsHome = isHome;
            Date = date;
        }

        public string Team { get; set; }
        public string Opponent { get; set; }
        public bool IsHome { get; set; }
        public DateTime Date { get; set; }
    }
}
