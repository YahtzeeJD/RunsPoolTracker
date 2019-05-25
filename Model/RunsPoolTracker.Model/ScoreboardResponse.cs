using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    public class ScoreboardResponse
    {
        [JsonProperty("scoreboard")]
        public Scoreboard Scoreboard { get; set; }
    }
}