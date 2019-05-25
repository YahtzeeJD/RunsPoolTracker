using Newtonsoft.Json;
using System.Collections.Generic;

namespace RunsPoolTracker.Model
{
    public class Scoreboard
    {
        [JsonProperty("lastUpdatedOn")]
        public string LastUpdatedOn { get; set; }

        [JsonProperty("gameScore")]
        public List<GameScore> GameScore { get; set; }
    }


}