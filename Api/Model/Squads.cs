namespace FootballScoresApi.Api.Model
{
    public class Squads
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public Paging paging { get; set; }
        public Response[] response { get; set; }


        public class Parameters
        {
            public string team { get; set; }
        }

        public class Paging
        {
            public int current { get; set; }
            public int total { get; set; }
        }

        public class Response
        {
            public Team team { get; set; }
            public Player[] players { get; set; }
        }

        public class Team
        {
            public int id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
        }

        public class Player
        {
            public int id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
            public int? number { get; set; }
            public string position { get; set; }
            public string photo { get; set; }
        }

    }
}
