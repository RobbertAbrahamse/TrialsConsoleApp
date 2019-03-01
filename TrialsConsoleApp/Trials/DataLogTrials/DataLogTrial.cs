using System.Collections.Generic;
using System.Linq;
using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;
using TrialsConsoleApp.Trials.DataLogTrials.TrialEvents;

namespace TrialsConsoleApp.Trials.DataLogTrials
{
    public class DataLogTrial
    {
        public DataLogTrial(DataLogFile dataLogFile, int firstSourceLineIndex, int lastSourceLineIndex)
        {
            DataLogFile = dataLogFile;
            FirstSourceLineIndex = firstSourceLineIndex;
            LastSourceLineIndex = lastSourceLineIndex;

            Events = TrialDataLogLines.Select(line => DataLogTrialEvent.CreateEvent(this, line)).ToArray();
        }

        public string Id => TrialDataLogLines.First().TrialId;
        public DataLogFile DataLogFile { get; }
        public int FirstSourceLineIndex { get; }
        public int LastSourceLineIndex { get; }
        public int TrialLineCount => LastSourceLineIndex - FirstSourceLineIndex + 1;

        public IList<DataLogTrialEvent> Events { get; }

        public bool IsCorrect
        {
            get
            {
                var firstPictureEvent = Events.FirstOrDefault(e => e is DataLogTrialPictureEvent pictureEvent && pictureEvent.PictureId != "Starttrial_Box");
                var firstResponseEvent = Events.FirstOrDefault(e => e is DataLogTrialResponseEvent);
                
                // no picture and no response events were present: succes
                if (firstPictureEvent == null && firstResponseEvent == null)
                {
                    return true;
                }

                // picture and response events were found
                if (firstPictureEvent != null && firstResponseEvent != null)
                {
                    return firstPictureEvent.Time < firstResponseEvent.Time;
                }

                return false;
            }
        }

        // Een voorbeeldje waarom een query language gebruiken handig is.
        // Deze functie geeft een IEnumerable (dit is een type waar een enumerator aangevraagt kan worden die door alle elementen
        // in die IEnumerable zitten) waarover men kan itereren (enumereren) door alle elementen heen, e.g. met foreach.
        // Hier gebruik ik functies zoals Skip (deze slaat een x aantal elementen over) en geeft de IEnumerable terug waar dit wordt gedaan.
        // Deze IEnumerable wordt vervolgens gebruikt om middels Take over een x aantal elementen te enumereren.
        public IEnumerable<DataLogLine> TrialDataLogLines
            => DataLogFile.Lines.Skip(FirstSourceLineIndex).Take(TrialLineCount);
    }
}
