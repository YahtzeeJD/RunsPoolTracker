using MySportsFeeds.Core.DataRetrievers;
using MySportsFeeds.Core.Workers;

namespace MySportsFeeds.Core
{
    public class MySportsFeedsClient
    {
        /// <summary>
        /// The HTTP worker
        /// </summary>
        private HttpCommunicationWorker _httpWorker;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySportsFeedsClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="league">The league.</param>
        /// <param name="version">The version.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public MySportsFeedsClient(string baseUrl, Enums.League league, string version, string username, string password)
        {
            _httpWorker = new HttpCommunicationWorker(baseUrl, version, username, password);
            InjectDependencies(league);
        }

        /// <summary>
        /// Injects the dependencies.
        /// </summary>
        private void InjectDependencies(Enums.League league)
        {
            switch (league)
            {
                case Enums.League.MLB:
                    ScoreboardDataRetriever = new ScoreboardDataRetriever(_httpWorker);
                    break;

                case Enums.League.NFL:
                    // TODO: Add NFL.
                    break;

                case Enums.League.NBA:
                    // TODO: Add NBA.
                    break;

                case Enums.League.NHL:
                    // TODO: Add NHL.
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Gets the scoreboard.
        /// </summary>
        /// <value>
        /// The scoreboard.
        /// </value>
        public ScoreboardDataRetriever ScoreboardDataRetriever { get; private set; }
    }
}