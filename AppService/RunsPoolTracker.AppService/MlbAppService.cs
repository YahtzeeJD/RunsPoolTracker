using Microsoft.Extensions.Configuration;
using MySportsFeeds.Core;
using MySportsFeeds.Core.Enums;
using MySportsFeeds.Core.Helpers;
using RunsPoolTracker.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RunsPoolTracker.AppService
{
    public class MlbAppService
    {
        private const string BASE_URL = "https://api.mysportsfeeds.com/";
        private MySportsFeedsClient _mySportsFeedsClient;
        private readonly IConfigurationRoot _configuration;

        public MlbAppService()
        {
            _configuration = InitializeConfiguration();
            _mySportsFeedsClient = new MySportsFeedsClient(BASE_URL, League.MLB, _configuration["api_version"], _configuration["username"], _configuration["password"]);
        }

        public async Task<TeamRunsCollection> ProcessDailyGamesForRound(DateTime roundStartDate)
        {
            var teamRunsCollection = new TeamRunsCollection();
            var currentDate = roundStartDate;

            Console.WriteLine($"=> Start processing the daily games at {DateTime.Now}");
            while (currentDate <= DateTime.Now)
            {
                await UpdateTeamRunsCollectionForDate(teamRunsCollection, currentDate);
                if (teamRunsCollection.RoundIsOver) break;
                currentDate = currentDate.AddDays(1);
            }
            Console.WriteLine($"=> Completed processing the daily games at {DateTime.Now}");

            return teamRunsCollection;
        }

        private async Task<TeamRunsCollection> UpdateTeamRunsCollectionForDate(TeamRunsCollection teamRunsCollection, DateTime forDate)
        {
            try
            {
                var forDateYear = forDate.Year;
                var requestOptions = new RequestOptions() { ForDate = FormatForDateForApi(forDate) };
                var scoreboardResponseDto = await _mySportsFeedsClient.ScoreboardDataRetriever.Get(forDateYear, SeasonType.Regular, requestOptions);

                if (scoreboardResponseDto == null) return teamRunsCollection;
                if (scoreboardResponseDto.Scoreboard.GameScore == null) return teamRunsCollection;

                Console.WriteLine($"Processing {scoreboardResponseDto.Scoreboard.GameScore.Count} games for {forDate.ToShortDateString()}");

                foreach (var gameScore in scoreboardResponseDto.Scoreboard.GameScore)
                {
                    if (gameScore.IsCompleted == "false") continue;

                    var homeTeamName = gameScore.Game.HomeTeam.Name;
                    var homeTeamScore = gameScore.HomeScore;
                    teamRunsCollection.AddRunsForTeamByDate(forDate, homeTeamName, homeTeamScore);

                    var awayTeamName = gameScore.Game.AwayTeam.Name;
                    var awayTeamScore = gameScore.AwayScore;
                    teamRunsCollection.AddRunsForTeamByDate(forDate, awayTeamName, awayTeamScore);
                }

                return teamRunsCollection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw ex;
            }
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

        private static string FormatForDateForApi(DateTime forDate)
        {
            var year = forDate.Year.ToString();
            var monthWithLeadingZero = "0" + forDate.Month.ToString();
            var month = monthWithLeadingZero.Substring(monthWithLeadingZero.Length - 2);
            var day = forDate.Day.ToString();
            return $"{year}{month}{day}";
        }

        #endregion
    }
}