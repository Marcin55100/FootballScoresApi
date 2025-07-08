namespace FootballScoresApi.Api.Model
{
    public class Fixtures
    {
        public string get { get; set; }
        public Parameters parameters { get; set; }
        public object[] errors { get; set; }
        public int results { get; set; }
        public Paging paging { get; set; }
        public Response[] response { get; set; }


        public class Parameters
        {
            public string league { get; set; }
            public string season { get; set; }
            public string date { get; set; }
            public string team { get; set; }
        }

        public class Paging
        {
            public int current { get; set; }
            public int total { get; set; }
        }

        public class Response
        {
            public Fixture fixture { get; set; }
            public League league { get; set; }
            public Teams teams { get; set; }
            public Goals goals { get; set; }
            public Score score { get; set; }
        }

        public class Fixture
        {
            public int id { get; set; }
            public object referee { get; set; }
            public string timezone { get; set; }
            public DateTime date { get; set; }
            public int timestamp { get; set; }
            public Periods periods { get; set; }
            public Venue venue { get; set; }
            public Status status { get; set; }
        }

        public class Periods
        {
            public object first { get; set; }
            public object second { get; set; }
        }

        public class Venue
        {
            public string id { get; set; }
            public string name { get; set; }
            public string city { get; set; }
        }

        public class Status
        {
            public string _long { get; set; }
            public string _short { get; set; }
            public object elapsed { get; set; }
        }

        public class League
        {
            public int id { get; set; }
            public string name { get; set; }
            public string country { get; set; }
            public string logo { get; set; }
            public string flag { get; set; }
            public int season { get; set; }
            public string round { get; set; }
        }

        public class Teams
        {
            public Home home { get; set; }
            public Away away { get; set; }
        }

        public class Home
        {
            public int id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
            public object winner { get; set; }
        }

        public class Away
        {
            public int id { get; set; }
            public string name { get; set; }
            public string logo { get; set; }
            public object winner { get; set; }
        }

        public class Goals
        {
            public object home { get; set; }
            public object away { get; set; }
        }

        public class Score
        {
            public Halftime halftime { get; set; }
            public Fulltime fulltime { get; set; }
            public Extratime extratime { get; set; }
            public Penalty penalty { get; set; }
        }

        public class Halftime
        {
            public object home { get; set; }
            public object away { get; set; }
        }

        public class Fulltime
        {
            public object home { get; set; }
            public object away { get; set; }
        }

        public class Extratime
        {
            public object home { get; set; }
            public object away { get; set; }
        }

        public class Penalty
        {
            public object home { get; set; }
            public object away { get; set; }
        }

    }
}
