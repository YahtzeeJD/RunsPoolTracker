using MySportsFeeds.Core;
using MySportsFeeds.Core.Enums;
using MySportsFeeds.Core.Helpers;
using RunsPoolTracker.Model;
using System;
using System.Threading.Tasks;

namespace RunsPoolTracker.AppService
{
    public class MlbAppService
    {
        private const string BASE_URL = "https://api.mysportsfeeds.com/";
        private MySportsFeedsClient mySportsFeedsClient;

        public async Task<ScoreboardResponse> GetScoreboardData(string username, string password, string version, DateTime forDate)
        {
            try
            {
                mySportsFeedsClient = new MySportsFeedsClient(BASE_URL, League.MLB, version, username, password);

                var forDateYear = forDate.Year;
                var requestOptions = new RequestOptions() { ForDate = FormatForDateForApi(forDate) };

                return await mySportsFeedsClient.ScoreboardDataRetriever.Get(forDateYear, SeasonType.Regular, requestOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw ex;
            }
        }

        private static string FormatForDateForApi(DateTime forDate)
        {
            var year = forDate.Year.ToString();
            var monthWithLeadingZero = "0" + forDate.Month.ToString();
            var month = monthWithLeadingZero.Substring(monthWithLeadingZero.Length - 2);
            var day = forDate.Day.ToString();
            return $"{year}{month}{day}";
        }
    }
}