using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class DataLogTrialResponseEvent : DataLogTrialEvent
    {
        public const string EventId = "Response";

        public DataLogTrialResponseEvent(DataLogTrial trial, DataLogLine dataLogLine)
            : base(trial, dataLogLine)
        {
        }
    }
}
