using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    public class Inning
    {
        public string Number { get; set; }
        public string AwayScore { get; set; }
        public string HomeScore { get; set; }
    }
}