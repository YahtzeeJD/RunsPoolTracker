using Newtonsoft.Json;
using System.Collections.Generic;

namespace RunsPoolTracker.Model
{
    public class Scoreboard
    {
        public string LastUpdatedOn { get; set; }
        public List<GameScore> GameScore { get; set; }
    }
}