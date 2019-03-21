using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class DataLogTrialEvent
    {
        public DataLogTrialEvent(DataLogTrial trial, DataLogLine dataLogLine)
        {
            Trial = trial;
            DataLogLine = dataLogLine;
        }

        public DataLogTrial Trial { get; }
        public DataLogLine DataLogLine { get; }

        public int WorldTime => DataLogLine.WorldTime;
        public int TrialTime => DataLogLine.TrialTime;

        public static DataLogTrialEvent CreateEvent(DataLogTrial dataLogTrial, DataLogLine dataLogLine)
        {
            switch (dataLogLine.EventType)
            {
                case DataLogTrialResponseEvent.EventId: return new DataLogTrialResponseEvent(dataLogTrial, dataLogLine);
                case DataLogTrialPictureEvent.EventId: return new DataLogTrialPictureEvent(dataLogTrial, dataLogLine);
                case DataLogTrialPulseEvent.EventId: return new DataLogTrialPulseEvent(dataLogTrial, dataLogLine);
            }

            return new DataLogTrialEvent(dataLogTrial, dataLogLine);
        }
    }
}
