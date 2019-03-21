using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials.TrialEvents
{
    public class DataLogTrialPictureEvent : DataLogTrialEvent
    {
        public const string EventId = "Picture";

        public const string StartTrialBoxPictureId = "Starttrial_Box";
        public const string EndTrialPictureId = "ITI";

        public const string noGoCode = "nogo";

        public DataLogTrialPictureEvent(DataLogTrial trial, DataLogLine dataLogLine)
            : base(trial, dataLogLine)
        {
        }

        public string PictureId => DataLogLine.Code;

        public bool IsStartTrialBoxPictureEvent => PictureId == StartTrialBoxPictureId;
        public bool IsEndTrialPictureEvent => PictureId == EndTrialPictureId;
        public bool IsNoGo => PictureId.Contains(noGoCode);
    }
}
