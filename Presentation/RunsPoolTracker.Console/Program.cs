using RunsPoolTracker.AppService;
using System;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        #region Constants

        private const string VERSION = "v1.2";
        private const string USERNAME = "a764bfc4-56b8-48a9-a703-9a5007";
        private const string PASSWORD = "CPGTQn2zPngu";

        #endregion Constants

        private static void Main(string[] args)
        {
            DateTime startDate, currentDate;
            startDate = currentDate = new DateTime(2019, 5, 3);
            var endDate = DateTime.Now.AddDays(-1);

            var appService = new MlbAppService();

            while (currentDate <= endDate)
            {
                Console.WriteLine($"Current Date: {currentDate.ToShortDateString()}");
                Console.WriteLine();

                var scoreboardData = appService.GetScoreboardData(USERNAME, PASSWORD, VERSION, currentDate).GetAwaiter().GetResult();

                foreach (var gameScore in scoreboardData.Scoreboard.GameScore)
                {
                    Console.WriteLine($"{gameScore.Game.HomeTeam.Name}: {gameScore.HomeScore}");
                    Console.WriteLine($"{gameScore.Game.AwayTeam.Name}: {gameScore.AwayScore}");
                    Console.WriteLine("");
                }
                Console.WriteLine("-------------------------------");

                currentDate = currentDate.AddDays(1);
            }

            Console.WriteLine("Press the any key to exit...");
            Console.ReadLine();
        }
    }
}