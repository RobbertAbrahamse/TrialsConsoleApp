using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles;

namespace TrialsConsoleApp.Trials.DataLogTrials
{
    public class DataLogTrialCollection : IEnumerable<DataLogTrial>
    {
        public DataLogTrialCollection(DataLogFile dataLogFile)
        {
            Id = dataLogFile.Lines.First().SplittedLineDataValues.First();

            Trials = FindTrials(dataLogFile);
        }

        public string Id { get; }
        public IList<DataLogTrial> Trials { get; }

        private IList<DataLogTrial> FindTrials(DataLogFile dataLogFile)
        {
            var trials = new List<DataLogTrial>();

            // start searching after the headings found in the log
            for (var lineIndex = dataLogFile.Headings.HeadingsDataLogLine.SourceLineIndex + 1; lineIndex < dataLogFile.Lines.Count; lineIndex++)
            {
                var trialFirstLine = dataLogFile.Lines[lineIndex];

                // skip empty lines
                if (trialFirstLine.IsEmpty) { continue; }

                // try to find where this trial ends looking at the Trial column
                var lastFoundIdenticalTrialIdLineIndex = trialFirstLine.SourceLineIndex;
                for (var nextLineIndex = lastFoundIdenticalTrialIdLineIndex + 1; nextLineIndex < dataLogFile.Lines.Count; nextLineIndex++)
                {
                    if (dataLogFile.Lines[nextLineIndex].TrialId != trialFirstLine.TrialId)
                    {
                        break;
                    }

                    lastFoundIdenticalTrialIdLineIndex = nextLineIndex;
                }

                // create a new trial
                trials.Add(new DataLogTrial(dataLogFile, trialFirstLine.SourceLineIndex, lastFoundIdenticalTrialIdLineIndex));

                // set lineIndex to start searching after the found trial. (lineIndex++ will be executed as part of the loop so no need to do this at this end)
                lineIndex = lastFoundIdenticalTrialIdLineIndex;
            }

            return trials;
        }

        public IEnumerator<DataLogTrial> GetEnumerator() => Trials.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
