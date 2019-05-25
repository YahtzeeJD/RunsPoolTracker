using MySportsFeeds.Core.Enums;
using MySportsFeeds.Core.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace MySportsFeeds.Core.Tests
{
    public class RunsPoolTrackerTests
    {
        #region Members

        /// <summary>
        /// The base URL
        /// </summary>
        protected readonly string BASE_URL = "https://api.mysportsfeeds.com/";

        /// <summary>
        /// The version
        /// </summary>
        protected readonly string VERSION = "v1.2";

        /// <summary>
        /// The username
        /// </summary>
        protected readonly string USERNAME = "a764bfc4-56b8-48a9-a703-9a5007";

        /// <summary>
        /// The password
        /// </summary>
        protected readonly string PASSWORD = "CPGTQn2zPngu";

        #endregion Members

        /// <summary>
        /// My sports feeds client
        /// </summary>
        protected MySportsFeedsClient mySportsFeedsClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBase"/> class.
        /// </summary>
        public RunsPoolTrackerTests()
        {
            mySportsFeedsClient = new MySportsFeedsClient(BASE_URL, League.MLB, VERSION, USERNAME, PASSWORD);
        }

        [Fact]
        public async Task Can_Get_All_Scoreboards()
        {
            // Arrange
            string FOR_DATE = "20190520";

            var requestOptions = new RequestOptions()
            {
                ForDate = FOR_DATE
            };

            // Act
            var response = await mySportsFeedsClient.ScoreboardDataRetriever.Get(2019, SeasonType.Regular, requestOptions);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(10, response.Scoreboard.GameScore.Count);
        }
    }
}