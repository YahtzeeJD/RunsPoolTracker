using MySportsFeeds.Core.DataRetrievers.DTO.Common;
using MySportsFeeds.Core.Enums;
using MySportsFeeds.Core.Helpers;
using MySportsFeeds.Core.Workers;
using RunsPoolTracker.Model;
using System.Threading.Tasks;

namespace MySportsFeeds.Core.DataRetrievers
{
    public class ScoreboardDataRetriever
    {
        /// <summary>
        /// The URL
        /// </summary>
        private const string Url = "/pull/mlb/{0}/scoreboard.json";

        /// <summary>
        /// The HTTP worker
        /// </summary>
        private HttpCommunicationWorker _httpWorker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScoreboardDataRetriever"/> class.
        /// </summary>
        /// <param name="httpWorker">The HTTP worker.</param>
        internal ScoreboardDataRetriever(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        /// <summary>
        /// Gets the specified scoreboard.
        /// </summary>
        /// <param name="league">The league.</param>
        /// <param name="year">The year.</param>
        /// <param name="seasonType">Type of the season.</param>
        /// <param name="requestOptions">The request options.</param>
        /// <returns></returns>
        public async Task<ScoreboardResponseDto> Get(int year, SeasonType seasonType, RequestOptions requestOptions = null)
        {
            var url = string.Concat(_httpWorker.Version, Url);
            string requestUrl = UrlBuilder.FormatRestApiUrl(url, year, seasonType, requestOptions);

            return await _httpWorker.GetAsync<ScoreboardResponseDto>(requestUrl).ConfigureAwait(false);
        }
    }
}