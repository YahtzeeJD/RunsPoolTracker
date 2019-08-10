using System.Collections.Generic;
using System.Linq;

namespace RunsPoolTracker.Model
{
    public class DatesOfRunsForTeam
    {
        public string TeamName { get; set; }
        public List<string> DatesOfRuns { get; set; }

        public DatesOfRunsForTeam(TeamRuns teamRuns)
        {
            TeamName = teamRuns.TeamName;
            DatesOfRuns = new List<string>();
            foreach (var runsDate in teamRuns.RunsDatesCollection)
            {
                if (runsDate.Dates.Count == 0)
                    DatesOfRuns.Add(string.Empty);
                else
                    DatesOfRuns.Add($"{ runsDate.Dates.Min().ToString("MM/dd")}");
            }
        }
    }
}
