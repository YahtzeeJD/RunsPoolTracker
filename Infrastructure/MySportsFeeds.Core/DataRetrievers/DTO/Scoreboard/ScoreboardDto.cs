using Newtonsoft.Json;
using System.Collections.Generic;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class ScoreboardDto
    {
        [JsonProperty("lastUpdatedOn")]
        public string LastUpdatedOn { get; set; }

        [JsonProperty("gameScore")]
        public List<GameScoreDto> GameScore { get; set; }
    }
}