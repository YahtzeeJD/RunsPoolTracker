using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class GameDto
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("awayTeam")]
        public AwayTeamDto AwayTeam { get; set; }

        [JsonProperty("homeTeam")]
        public HomeTeamDto HomeTeam { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}