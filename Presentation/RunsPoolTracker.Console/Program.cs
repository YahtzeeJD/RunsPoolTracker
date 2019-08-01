using Microsoft.Extensions.Configuration;
using RunsPoolTracker.AppService;
using RunsPoolTracker.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        static TeamRunsCollection TeamRunsCollection;

        private static async Task Main(string[] args)
        {
            TeamRunsCollection = new TeamRunsCollection();
            IConfigurationRoot configuration = InitializeConfiguration();

            DateTime startDate, currentDate;

            Console.WriteLine("Enter the Round #");
            var roundNumber = Console.ReadLine();
            var roundStartDate = $"round{roundNumber}_start_date";
            startDate = currentDate = Convert.ToDateTime(configuration[roundStartDate]);
            var endDate = DateTime.Now.AddDays(-1);

            var appService = new MlbAppService();
            Console.WriteLine($"=> Start processing the daily games at {DateTime.Now}");
            await ProcessDailyGames(currentDate, endDate, appService);
            Console.WriteLine($"=> Completed processing the daily games at {DateTime.Now}");

            OutputRemainingRunsByTeam();
            OutputRunsByTeamGrid();

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

        private static async Task ProcessDailyGames(DateTime currentDate, DateTime endDate, MlbAppService appService)
        {
            while (currentDate <= endDate)
            {
                var scoreboardData = await appService.GetScoreboardData(currentDate);

                if (scoreboardData == null) continue;
                if (scoreboardData.Scoreboard.GameScore == null) continue;

                Console.WriteLine($"Processing {scoreboardData.Scoreboard.GameScore.Count} games for {currentDate}");

                foreach (var gameScore in scoreboardData.Scoreboard.GameScore.Where(x => x.IsCompleted == "true"))
                    TeamRunsCollection.ComputeRunsForDailyGames(gameScore);

                currentDate = currentDate.AddDays(1);
            }
        }

        #region By Teams

        private static void OutputRemainingRunsByTeam()
        {
            Console.WriteLine("");
            List<RemainingRunsForTeam> remainingRuns = ComputeRemainingRunsByTeam();
            foreach (var rr in remainingRuns.OrderBy(x => x.RemainingRuns.Count))
                Console.WriteLine($"{rr.TeamName.PadRight(12, ' ')} \t {rr.RemainingRuns.Count} ({string.Join(", ", rr.RemainingRuns)})");
        }

        private static List<RemainingRunsForTeam> ComputeRemainingRunsByTeam()
        {
            var remainingRuns = new List<RemainingRunsForTeam>();
            foreach (var teamRuns in TeamRunsCollection.ListOfTeamRuns)
            {
                var remainingRunsForTeam = new RemainingRunsForTeam();
                remainingRunsForTeam.TeamName = teamRuns.TeamName; // == "Athletics" ? "A's" : teamRuns.TeamName;
                remainingRunsForTeam.RemainingRuns = new List<string>();
                foreach (var runsDate in teamRuns.RunsDatesCollection)
                {
                    if (runsDate.Dates.Count == 0)
                        remainingRunsForTeam.RemainingRuns.Add(runsDate.Runs.ToString());
                }
                remainingRuns.Add(remainingRunsForTeam);
            }

            return remainingRuns;
        }

        #endregion

        #region By Teams Grid

        private static void OutputRunsByTeamGrid()
        {
            Console.WriteLine("");
            List<RemainingRunsForTeam> remainingRunsGrid = ComputeRemainingRunsGridByTeam();
            foreach (var rr in remainingRunsGrid)
                Console.WriteLine($"{rr.TeamName},{string.Join(", ", rr.RemainingRuns)}");
        }

        private static List<RemainingRunsForTeam> ComputeRemainingRunsGridByTeam()
        {
            var remainingRuns = new List<RemainingRunsForTeam>();
            foreach (var teamRuns in TeamRunsCollection.ListOfTeamRuns)
            {
                var remainingRunsForTeam = new RemainingRunsForTeam();
                remainingRunsForTeam.TeamName = teamRuns.TeamName == "Athletics" ? "A's" : teamRuns.TeamName;
                remainingRunsForTeam.RemainingRuns = new List<string>();
                foreach (var runsDate in teamRuns.RunsDatesCollection)
                {
                    if (runsDate.Dates.Count == 0)
                        remainingRunsForTeam.RemainingRuns.Add("1");
                    else
                        remainingRunsForTeam.RemainingRuns.Add($"'{ runsDate.Dates.Min().ToString("MM/dd")}");
                }
                remainingRuns.Add(remainingRunsForTeam);
            }

            return remainingRuns;
        }

        #endregion

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