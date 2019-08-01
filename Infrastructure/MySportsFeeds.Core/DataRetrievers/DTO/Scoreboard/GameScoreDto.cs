using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class GameScoreDto
    {
        [JsonProperty("game")]
        public ScoreboardGameDto Game { get; set; }

        [JsonProperty("isUnplayed")]
        public string IsUnplayed { get; set; }

        [JsonProperty("isInProgress")]
        public string IsInProgress { get; set; }

        [JsonProperty("isCompleted")]
        public string IsCompleted { get; set; }

        [JsonProperty("playStatus")]
        public object PlayStatus { get; set; }

        [JsonProperty("awayScore")]
        public string AwayScore { get; set; }

        [JsonProperty("homeScore")]
        public string HomeScore { get; set; }

        [JsonProperty("inningSummary")]
        public InningSummaryDto InningSummary { get; set; }
    }
}