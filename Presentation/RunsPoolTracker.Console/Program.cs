using MySportsFeeds.Core.Helpers;
using RunsPoolTracker.AppService;
using System;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        #region Members

        private const string VERSION = "v1.2";
        private const string USERNAME = "a764bfc4-56b8-48a9-a703-9a5007";
        private const string PASSWORD = "CPGTQn2zPngu";

        #endregion Members

        private static void Main(string[] args)
        {
            string FOR_DATE = "20190520";
            var appService = new MlbAppService();
            var scoreboardData = appService.GetScoreboardData(USERNAME, PASSWORD, VERSION, FOR_DATE).GetAwaiter().GetResult();

            foreach(var gameScore in scoreboardData.Scoreboard.GameScore)
            {
                Console.WriteLine($"{gameScore.Game.HomeTeam.Name}: {gameScore.HomeScore}");
                Console.WriteLine($"{gameScore.Game.AwayTeam.Name}: {gameScore.AwayScore}");
                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}