using System;
using System.Collections.Generic;
using System.Linq;

namespace RunsPoolTracker.Model
{
    public class TeamRunsCollection
    {
        public List<TeamRuns> ListOfTeamRuns { get; set; }

        #region teams

        string[] teams = new string[] {
                "Angels",
                "Astros",
                "Athletics",
                "Blue Jays",
                "Braves",
                "Brewers",
                "Cardinals",
                "Cubs",
                "Diamondbacks",
                "Dodgers",
                "Giants",
                "Indians",
                "Mariners",
                "Marlins",
                "Mets",
                "Nationals",
                "Orioles",
                "Padres",
                "Phillies",
                "Pirates",
                "Rangers",
                "Rays",
                "Red Sox",
                "Reds",
                "Rockies",
                "Royals",
                "Tigers",
                "Twins",
                "White Sox",
                "Yankees"
            };

        #endregion Teams

        public TeamRunsCollection()
        {
            ListOfTeamRuns = new List<TeamRuns>();
            TeamRuns teamRuns;

            foreach (var team in teams)
            {
                teamRuns = new TeamRuns { TeamName = team, RunsDatesCollection = PopulateAllRunsDates() };
                ListOfTeamRuns.Add(teamRuns);
            }
        }

        private List<RunsDates> PopulateAllRunsDates()
        {
            var runsDatesCollection = new List<RunsDates>();
            var runs = 0;

            while (runs < 14)
            {
                var runsDates = new RunsDates { Runs = runs, Dates = new List<DateTime>() };
                runsDatesCollection.Add(runsDates);
                runs += 1;
            }

            return runsDatesCollection;
        }

        public void AddRunsForTeamByDate(DateTime forDate, string teamName, string teamScore)
        {
            if (int.Parse(teamScore) > 13) teamScore = "13";

            RunsDates runsDate;
            runsDate = ListOfTeamRuns
                .Where(t => t.TeamName == teamName).SingleOrDefault()
                .RunsDatesCollection.Where(r => r.Runs == int.Parse(teamScore)).SingleOrDefault();
            runsDate.Dates.Add(Convert.ToDateTime(forDate));
        }

        public bool RoundIsOver {
            get
            {
                return ComputeRemainingRunsByTeam().Any(x => x.RemainingRuns.Count == 0);
            }
        }

        public List<RemainingRunsForTeam> ComputeRemainingRunsByTeam()
        {
            var remainingRuns = new List<RemainingRunsForTeam>();
            foreach (var teamRuns in ListOfTeamRuns)
            {
                var remainingRunsForTeam = new RemainingRunsForTeam(teamRuns);
                remainingRuns.Add(remainingRunsForTeam);
            }

            return remainingRuns;
        }

        public List<DatesOfRunsForTeam> ComputeDateOfRunsForTeams()
        {
            var dateOfRunsForTeams = new List<DatesOfRunsForTeam>();
            foreach (var teamRuns in ListOfTeamRuns)
            {
                var dateOfRunsForTeam = new DatesOfRunsForTeam(teamRuns);
                dateOfRunsForTeams.Add(dateOfRunsForTeam);
            }

            return dateOfRunsForTeams;
        }

    }
}
