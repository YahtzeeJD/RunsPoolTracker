using MySportsFeeds.Core;
using MySportsFeeds.Core.Enums;
using System;

namespace RunsPoolTrackerConsole
{
    public class Program
    {
        #region Members

        private static readonly string BASE_URL = "https://api.mysportsfeeds.com/";
        private static readonly string VERSION = "v1.2";
        private static readonly string USERNAME = "a764bfc4-56b8-48a9-a703-9a5007";
        private static readonly string PASSWORD = "CPGTQn2zPngu";

        #endregion Members

        static protected MySportsFeedsClient mySportsFeedsClient;

        private static void Main(string[] args)
        {
            mySportsFeedsClient = new MySportsFeedsClient(BASE_URL, League.MLB, VERSION, USERNAME, PASSWORD);
        }
    }
}