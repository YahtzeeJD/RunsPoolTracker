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

        private static List<RunsDates> PopulateAllRunsDates()
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

        public void ComputeRunsForDailyGames(GameScore gameScore)
        {
            if (int.Parse(gameScore.HomeScore) > 13) gameScore.HomeScore = "13";
            if (int.Parse(gameScore.AwayScore) > 13) gameScore.AwayScore = "13";

            RunsDates runsDate;
            runsDate = ListOfTeamRuns
                .Where(t => t.TeamName == gameScore.Game.HomeTeam.Name).SingleOrDefault()
                .RunsDatesCollection.Where(r => r.Runs == int.Parse(gameScore.HomeScore)).SingleOrDefault();
            runsDate.Dates.Add(Convert.ToDateTime(gameScore.Game.Date));

            runsDate = ListOfTeamRuns
                .Where(t => t.TeamName == gameScore.Game.AwayTeam.Name).SingleOrDefault()
                .RunsDatesCollection.Where(r => r.Runs == int.Parse(gameScore.AwayScore)).SingleOrDefault();
            runsDate.Dates.Add(Convert.ToDateTime(gameScore.Game.Date));
        }
    }
}
