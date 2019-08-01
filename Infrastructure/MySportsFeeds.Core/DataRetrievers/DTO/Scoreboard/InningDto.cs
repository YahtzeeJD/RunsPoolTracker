using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class InningDto
    {
        [JsonProperty("@number")]
        public string Number { get; set; }

        [JsonProperty("awayScore")]
        public string AwayScore { get; set; }

        [JsonProperty("homeScore")]
        public string HomeScore { get; set; }
    }
}