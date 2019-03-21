using System;
using System.Collections.Generic;
using System.IO;

namespace TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles
{
    public class DataLogFile
    {
        // Constructor
        public DataLogFile(string sourceDataLogFilePath, string[] lineDataSeparators)
        {
            SourceDataLogFilePath = sourceDataLogFilePath;

            Lines = ReadTrialDataLogLinesFromFile(sourceDataLogFilePath, lineDataSeparators);

            Headings = DataLogHeadings.TryFindDataLogFileHeadings(Lines);
        }


        // Properties
        public string SourceDataLogFilePath { get; }
        public IList<DataLogLine> Lines { get; }
        public DataLogHeadings Headings { get; }

        // Functions (methods)
        private IList<DataLogLine> ReadTrialDataLogLinesFromFile(string sourceDataLogFilePath, string[] lineDataSeparators)
        {
            var trialDataLogLines = new List<DataLogLine>();
            int lineIndex = 0;
            foreach (var textLine in File.ReadLines(sourceDataLogFilePath)) // https://www.mathworks.com/matlabcentral/answers/4434-for-each-object-in-object
            {
                trialDataLogLines.Add(
                    new DataLogLine(this, lineIndex, textLine.Split(lineDataSeparators, StringSplitOptions.None)));
                lineIndex++;
            }

            return trialDataLogLines;
        }
    }
}
