using Microsoft.Extensions.Configuration;
using RunsPoolTracker.AppService;
using RunsPoolTracker.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            Initialize(out DateTime currentDate, out TeamRunsCollection teamRunsCollection, out DateTime endDate, out MlbAppService appService);

            await ProcessDailyGames(teamRunsCollection, currentDate, endDate, appService);

            OutputRemainingRunsByTeam(teamRunsCollection);
            OutputRunsByTeamGrid(teamRunsCollection);

            ExitProgram();
        }

        private static void Initialize(out DateTime currentDate, out TeamRunsCollection teamRunsCollection, out DateTime endDate, out MlbAppService appService)
        {
            IConfigurationRoot configuration = InitializeConfiguration();
            Console.WriteLine("Enter the Round #");
            var roundNumber = Console.ReadLine();
            var roundStartDate = $"round{roundNumber}_start_date";
            currentDate = Convert.ToDateTime(configuration[roundStartDate]);
            endDate = DateTime.Now;
            teamRunsCollection = new TeamRunsCollection();
            appService = new MlbAppService();
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration;
        }

        private static async Task ProcessDailyGames(TeamRunsCollection teamRunsCollection, DateTime currentDate, DateTime endDate, MlbAppService appService)
        {
            Console.WriteLine($"=> Start processing the daily games at {DateTime.Now}");
            while (currentDate <= endDate)
            {
                Console.WriteLine($"Processing games for {currentDate.ToString("MM/dd/yyyy")}");

                await appService.UpdateTeamRunsCollectionForDate(teamRunsCollection, currentDate);
                if (teamRunsCollection.ComputeRemainingRunsByTeam().Any(x => x.RemainingRuns.Count == 0))
                    break;
                currentDate = currentDate.AddDays(1);
            }
            Console.WriteLine($"=> Completed processing the daily games at {DateTime.Now}");
        }

        private static void OutputRemainingRunsByTeam(TeamRunsCollection teamRunsCollection)
        {
            Console.WriteLine("");
            var remainingRuns = teamRunsCollection.ComputeRemainingRunsByTeam();
            foreach (var rr in remainingRuns.OrderBy(x => x.RemainingRuns.Count))
                Console.WriteLine($"{rr.TeamName.PadRight(12, ' ')} \t {rr.RemainingRuns.Count} ({string.Join(", ", rr.RemainingRuns)})");
        }

        private static void OutputRunsByTeamGrid(TeamRunsCollection teamRunsCollection)
        {
            Console.WriteLine("");
            var dateOfRunsForTeams = teamRunsCollection.ComputeDateOfRunsForTeams();
            foreach (var dateOfRunsForTeam in dateOfRunsForTeams)
                Console.WriteLine($"{dateOfRunsForTeam.TeamName},{string.Join(", ", dateOfRunsForTeam.DateOfRuns)}");
        }

        private static void ExitProgram()
        {
            Console.WriteLine("");
            Console.WriteLine("Press the any key to exit...");
            Console.ReadLine();
        }
    }
}