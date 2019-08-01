using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    /// <summary>
    /// Base Team
    /// </summary>
    public class Team
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }

    public class AwayTeam : Team
    {
    }

    public class HomeTeam : Team
    {
    }
}