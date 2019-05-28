using Microsoft.Extensions.Configuration;
using RunsPoolTracker.AppService;
using RunsPoolTracker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IConfigurationRoot configuration = InitializeConfiguration();

            DateTime startDate, currentDate;
            startDate = currentDate = Convert.ToDateTime(configuration["round_start_date"]);
            var endDate = DateTime.Now;

            var appService = new MlbAppService();
            var teamRunsCollection = InitalizeTeamRunsCollection();
            ProcessDailyGames(configuration, currentDate, endDate, appService, teamRunsCollection);

            OutputRemainingRunsByTeam(teamRunsCollection);
            OutputRunsByTeamGrid(teamRunsCollection);

            Console.WriteLine("");
            Console.WriteLine("Press the any key to exit...");
            Console.ReadLine();
        }

        #region Helpers

        private static IConfigurationRoot InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration;
        }

        private static List<TeamRuns> InitalizeTeamRunsCollection()
        {
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

            var teamRunsCollection = new List<TeamRuns>();
            TeamRuns teamRuns;

            foreach (var team in teams)
            {
                teamRuns = new TeamRuns { TeamName = team, RunsDatesCollection = PopulateAllRunsDates() };
                teamRunsCollection.Add(teamRuns);
            }

            return teamRunsCollection;
        }

        private static List<RunsDates> PopulateAllRunsDates()
        {
            var runsDatesCollection = new List<RunsDates>();
            var runs = 0;

            while (runs < 14)
            {
                var runsDates = new RunsDates { Runs = runs, Dates = new List<DateTime>() };
                runsDatesCollection.Add(runsDates);
                runs = runs + 1;
            }

            return runsDatesCollection;
        }

        private static void ProcessDailyGames(IConfigurationRoot configuration, DateTime currentDate, DateTime endDate, MlbAppService appService, List<TeamRuns> teamRunsCollection)
        {
            while (currentDate <= endDate)
            {
                var scoreboardData = appService.GetScoreboardData(
                                        configuration["username"],
                                        configuration["password"],
                                        configuration["api_version"],
                                        currentDate
                                    ).GetAwaiter().GetResult();
                ComputeRunsForDailyGames(currentDate, teamRunsCollection, scoreboardData);
                currentDate = currentDate.AddDays(1);
            }
        }

        private static void ComputeRunsForDailyGames(DateTime currentDate, List<TeamRuns> teamRunsCollection, ScoreboardResponse scoreboardData)
        {
            if (scoreboardData == null) return;

            Console.WriteLine($"Processing {currentDate.ToShortDateString()} ({currentDate.DayOfWeek.ToString().Substring(0,3)}) \t => {scoreboardData.Scoreboard.GameScore.Count} completed games");
            foreach (var gameScore in scoreboardData.Scoreboard.GameScore.Where(x => x.IsCompleted == "true"))
            {
                if (int.Parse(gameScore.HomeScore) > 13) gameScore.HomeScore = "13";
                if (int.Parse(gameScore.AwayScore) > 13) gameScore.AwayScore = "13";

                RunsDates teamRuns;
                teamRuns = teamRunsCollection
                    .Where(t => t.TeamName == gameScore.Game.HomeTeam.Name).SingleOrDefault()
                    .RunsDatesCollection.Where(r => r.Runs == int.Parse(gameScore.HomeScore)).SingleOrDefault();
                teamRuns.Dates.Add(currentDate);

                teamRuns = teamRunsCollection
                    .Where(t => t.TeamName == gameScore.Game.AwayTeam.Name).SingleOrDefault()
                    .RunsDatesCollection.Where(r => r.Runs == int.Parse(gameScore.AwayScore)).SingleOrDefault();
                teamRuns.Dates.Add(currentDate);
            }
        }

        private static List<RemainingRunsForTeam> ComputeRemainingRunsGridByTeam(List<TeamRuns> teamRunsCollection)
        {
            var remainingRuns = new List<RemainingRunsForTeam>();
            foreach (var teamRuns in teamRunsCollection)
            {
                var remainingRunsForTeam = new RemainingRunsForTeam();
                remainingRunsForTeam.TeamName = teamRuns.TeamName == "Athletics" ? "A's" : teamRuns.TeamName;
                remainingRunsForTeam.RemainingRuns = new List<string>();
                foreach (var t in teamRuns.RunsDatesCollection)
                {
                    if (t.Dates.Count == 0)
                        remainingRunsForTeam.RemainingRuns.Add("1");
                    else
                        remainingRunsForTeam.RemainingRuns.Add($"'{ t.Dates.Min().ToString("MM/dd")}");
                }
                remainingRuns.Add(remainingRunsForTeam);
            }

            return remainingRuns;
        }

        private static List<RemainingRunsForTeam> ComputeRemainingRunsByTeam(List<TeamRuns> teamRunsCollection)
        {
            var remainingRuns = new List<RemainingRunsForTeam>();
            foreach (var teamRuns in teamRunsCollection)
            {
                var remainingRunsForTeam = new RemainingRunsForTeam();
                remainingRunsForTeam.TeamName = teamRuns.TeamName == "Athletics" ? "A's" : teamRuns.TeamName;
                remainingRunsForTeam.RemainingRuns = new List<string>();
                foreach (var t in teamRuns.RunsDatesCollection)
                {
                    if (t.Dates.Count == 0)
                        remainingRunsForTeam.RemainingRuns.Add(t.Runs.ToString());
                }
                remainingRuns.Add(remainingRunsForTeam);
            }

            return remainingRuns;
        }

        private static void OutputRemainingRunsByTeam(List<TeamRuns> teamRunsCollection)
        {
            Console.WriteLine("");
            List<RemainingRunsForTeam> remainingRuns = ComputeRemainingRunsByTeam(teamRunsCollection);
            foreach (var rr in remainingRuns.OrderBy(x => x.RemainingRuns.Count))
                Console.WriteLine($"{rr.TeamName.PadRight(12, ' ')} \t {rr.RemainingRuns.Count} ({string.Join(", ", rr.RemainingRuns)})");
        }

        private static void OutputRunsByTeamGrid(List<TeamRuns> teamRunsCollection)
        {
            Console.WriteLine("");
            List<RemainingRunsForTeam> remainingRunsGrid = ComputeRemainingRunsGridByTeam(teamRunsCollection);
            foreach (var rr in remainingRunsGrid)
                Console.WriteLine($"{rr.TeamName},{string.Join(", ", rr.RemainingRuns)}");
        }

        #endregion Helpers
    }

    #region Data class

    public class RemainingRunsForTeam
    {
        public string TeamName { get; set; }
        public List<string> RemainingRuns { get; set; }
    }

    #endregion Data class
}