using System.Collections.Generic;

namespace RunsPoolTracker.Model
{
    public class RemainingRunsForTeam
    {
        public string TeamName { get; set; }
        public List<string> RemainingRuns { get; set; }

        public RemainingRunsForTeam(TeamRuns teamRuns)
        {
            TeamName = teamRuns.TeamName;
            RemainingRuns = new List<string>();
            foreach (var runsDate in teamRuns.RunsDatesCollection)
            {
                if (runsDate.Dates.Count == 0)
                    RemainingRuns.Add(runsDate.Runs.ToString());
            }
        }
    }
}