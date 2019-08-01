using Newtonsoft.Json;

namespace MySportsFeeds.Core.DataRetrievers.DTO.Common
{
    /// <summary>
    /// Base Team
    /// </summary>
    public class TeamDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [JsonProperty("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        /// <value>
        /// The abbreviation.
        /// </value>
        [JsonProperty("Abbreviation")]
        public string Abbreviation { get; set; }
    }

    public class AwayTeamDto : TeamDto
    {
    }

    public class HomeTeamDto : TeamDto
    {
    }
}