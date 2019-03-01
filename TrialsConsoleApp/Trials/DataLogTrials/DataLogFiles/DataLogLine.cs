using System;
using System.Collections.Generic;
using System.Linq;

namespace TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles
{
    public class DataLogLine
    {
        public string[] LineDataSeparators = { ";" };

        public DataLogLine(DataLogFile dataLogFile, int sourceLineIndex, string line)
        {
            DataLogFile = dataLogFile;
            SourceLineIndex = sourceLineIndex;
            SplittedLineDataValues = line.Split(LineDataSeparators, StringSplitOptions.None);
        }

        // Properties
        public DataLogFile DataLogFile { get; }
        public int SourceLineIndex { get; }
        public int ColumnCount => SplittedLineDataValues.Count;
        public IList<string> SplittedLineDataValues { get; }

        public bool IsEmpty => SplittedLineDataValues.All(v => v == string.Empty);

        public string Subject => GetString(DataLogFile.Headings.SubjectHeading.ColumnIndex);
        public string TrialId => GetString(DataLogFile.Headings.TrialHeading.ColumnIndex);
        public string EventType => GetString(DataLogFile.Headings.EventTypeHeading.ColumnIndex);
        public string Code => GetString(DataLogFile.Headings.CodeHeading.ColumnIndex);
        public int Time => GetInt(DataLogFile.Headings.TimeHeading.ColumnIndex);


        // Functions (methods)
        public string GetString(int columnIndex) => SplittedLineDataValues[columnIndex];
        public int GetInt(int columnIndex) => int.Parse(SplittedLineDataValues[columnIndex]);
    }
}
