namespace FootballScoresApi.Model
{
    public class Fixture
    {
        public Fixture(bool isHome, string opponent)
        {
            IsHome = isHome;
            Opponent = opponent;
        }

        public bool IsHome { get; set; }
        public string Opponent { get; set; }
    }
}
