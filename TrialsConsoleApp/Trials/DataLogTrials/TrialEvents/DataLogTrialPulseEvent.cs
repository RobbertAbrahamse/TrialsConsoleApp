using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class DataLogTrialPulseEvent : DataLogTrialEvent
    {
        public const string EventId = "Pulse";

        public DataLogTrialPulseEvent(DataLogTrial trial, DataLogLine dataLogLine)
            : base(trial, dataLogLine)
        {
        }
    }
}
