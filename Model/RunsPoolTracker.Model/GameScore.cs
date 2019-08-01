using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunsPoolTracker.Model
{
    public class GameScore
    {
        public ScoreboardGame Game { get; set; }
        public string IsUnplayed { get; set; }
        public string IsInProgress { get; set; }
        public string IsCompleted { get; set; }
        public object PlayStatus { get; set; }
        public string AwayScore { get; set; }
        public string HomeScore { get; set; }
        public InningSummary InningSummary { get; set; }
    }
}
