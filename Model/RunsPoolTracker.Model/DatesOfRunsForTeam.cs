using System.Collections.Generic;
using System.Linq;

namespace RunsPoolTracker.Model
{
    public class DateOfRunsForTeam
    {
        public string TeamName { get; set; }
        public List<string> DateOfRuns { get; set; }

        public DateOfRunsForTeam(TeamRuns teamRuns)
        {
            TeamName = teamRuns.TeamName;
            DateOfRuns = new List<string>();
            foreach (var runsDate in teamRuns.RunsDatesCollection)
            {
                if (runsDate.Dates.Count == 0)
                    DateOfRuns.Add("1");
                else
                    DateOfRuns.Add($"'{ runsDate.Dates.Min().ToString("MM/dd")}");
            }
        }
    }
}
