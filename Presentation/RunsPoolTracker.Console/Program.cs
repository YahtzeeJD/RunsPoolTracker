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
            Initialize(out DateTime roundStartDate, out MlbAppService appService);

            var teamRunsCollection = await appService.ProcessDailyGamesForRound(roundStartDate);

            OutputRemainingRunsByTeam(teamRunsCollection);
            OutputRunsByTeamGrid(teamRunsCollection);

            ExitProgram();
        }

        private static void Initialize(out DateTime roundStartDate, out MlbAppService appService)
        {
            IConfigurationRoot configuration = InitializeConfiguration();
            Console.WriteLine("Enter the Round #");
            var roundNumber = Console.ReadLine();
            var roundStartDateFromConfig = $"round{roundNumber}_start_date";
            roundStartDate = Convert.ToDateTime(configuration[roundStartDateFromConfig]);
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
                Console.WriteLine($"{dateOfRunsForTeam.TeamName},{string.Join(", ", dateOfRunsForTeam.DatesOfRuns)}");
        }

        private static void ExitProgram()
        {
            Console.WriteLine("");
            Console.WriteLine("Press the any key to exit...");
            Console.ReadLine();
        }
    }
}