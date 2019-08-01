using Newtonsoft.Json;

namespace RunsPoolTracker.Model
{
    public class ScoreboardGame : Game
    {
        public string ScheduleStatus { get; set; }
        public object OriginalDate { get; set; }
        public object OriginalTime { get; set; }
        public object DelayedOrPostponedReason { get; set; }
    }
}