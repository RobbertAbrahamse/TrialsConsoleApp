using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class DataLogTrialPictureEvent : DataLogTrialEvent
    {
        public const string EventId = "Picture";

        public DataLogTrialPictureEvent(DataLogTrial trial, DataLogLine dataLogLine)
            : base(trial, dataLogLine)
        {
        }

        public string PictureId => DataLogLine.Code;
    }
}
