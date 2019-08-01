using Newtonsoft.Json;
using System.Collections.Generic;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    public class InningSummaryDto
    {
        [JsonProperty("inning")]
        public List<InningDto> Inning { get; set; }
    }
}