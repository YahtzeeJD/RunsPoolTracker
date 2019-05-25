using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    public class Inning
    {
        [JsonProperty("@number")]
        public string Number { get; set; }

        [JsonProperty("awayScore")]
        public string AwayScore { get; set; }

        [JsonProperty("homeScore")]
        public string HomeScore { get; set; }
    }


}