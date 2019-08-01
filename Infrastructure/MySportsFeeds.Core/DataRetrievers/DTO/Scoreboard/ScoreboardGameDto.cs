using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class ScoreboardGameDto : GameDto
    {
        [JsonProperty("scheduleStatus")]
        public string ScheduleStatus { get; set; }

        [JsonProperty("originalDate")]
        public object OriginalDate { get; set; }

        [JsonProperty("originalTime")]
        public object OriginalTime { get; set; }

        [JsonProperty("delayedOrPostponedReason")]
        public object DelayedOrPostponedReason { get; set; }
    }
}