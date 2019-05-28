using System.Collections.Generic;

namespace RunsPoolTracker.Model
{
    public class TeamRuns
    {
        public string TeamName { get; set; }
        public List<RunsDates> RunsDatesCollection { get; set; }
    }
}
