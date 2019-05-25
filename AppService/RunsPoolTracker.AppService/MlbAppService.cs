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

        public async Task<ScoreboardResponse> GetScoreboardData(string username, string password, string version, string forDate)
        {
            try
            {
                mySportsFeedsClient = new MySportsFeedsClient(BASE_URL, League.MLB, version, username, password);

                var reformattedForDate = $"{forDate.Substring(4, 2)}/{forDate.Substring(6, 2)}/{forDate.Substring(0, 4)}";
                var forDateYear = DateTime.Parse(reformattedForDate).Year;
                var requestOptions = new RequestOptions()
                {
                    ForDate = forDate
                };

                return await mySportsFeedsClient.ScoreboardDataRetriever.Get(forDateYear, SeasonType.Regular, requestOptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                throw ex;
            }
        }
    }
}
