using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class ScoreboardResponseDto
    {
        [JsonProperty("scoreboard")]
        public ScoreboardDto Scoreboard { get; set; }
    }
}