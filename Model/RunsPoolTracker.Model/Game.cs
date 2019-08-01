using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    public class Game
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public HomeTeam HomeTeam { get; set; }
        public string Location { get; set; }
    }
}