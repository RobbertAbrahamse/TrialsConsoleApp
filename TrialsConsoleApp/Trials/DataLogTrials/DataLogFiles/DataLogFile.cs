using System.Collections.Generic;
using System.IO;

namespace TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles
{
    public class DataLogFile
    {
        // Constructor
        public DataLogFile(string sourceDataLogFilePath)
        {
            SourceDataLogFilePath = sourceDataLogFilePath;

            Lines = ReadTrialDataLogLinesFromFile(sourceDataLogFilePath);

            Headings = DataLogHeadings.TryFindDataLogFileHeadings(Lines);
        }


        // Properties
        public string SourceDataLogFilePath { get; }
        public IList<DataLogLine> Lines { get; }
        public DataLogHeadings Headings { get; }

        // Functions (methods)
        private IList<DataLogLine> ReadTrialDataLogLinesFromFile(string sourceDataLogFilePath)
        {
            var trialDataLogLines = new List<DataLogLine>();
            int lineIndex = 0;
            foreach (var textLine in File.ReadLines(sourceDataLogFilePath)) // https://www.mathworks.com/matlabcentral/answers/4434-for-each-object-in-object
            {
                trialDataLogLines.Add(new DataLogLine(this, lineIndex, textLine));
                lineIndex++;
            }

            return trialDataLogLines;
        }
    }
}
