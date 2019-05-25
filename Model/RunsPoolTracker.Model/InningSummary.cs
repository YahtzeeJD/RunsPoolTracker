using Newtonsoft.Json;
using System.Collections.Generic;

namespace RunsPoolTracker.Model
{
    public class InningSummary
    {
        [JsonProperty("inning")]
        public List<Inning> Inning { get; set; }
    }


}