using System.Collections.Generic;
using System.Linq;

namespace TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles
{
    public class DataLogHeadings
    {
        // Constructors
        public DataLogHeadings(DataLogLine headingsDataLogLine)
        {
            HeadingsDataLogLine = headingsDataLogLine;

            // Create the headings
            Headings = new DataLogHeading[headingsDataLogLine.ColumnCount];
            for (var columnIndex = 0; columnIndex < headingsDataLogLine.ColumnCount; columnIndex++)
            {
                Headings[columnIndex] = new DataLogHeading(this, columnIndex);
            }

            // Cache often specifically used headings
            TrySetOftenUsedHeadings();
        }


        // Properties
        public DataLogLine HeadingsDataLogLine { get; }
        public IList<DataLogHeading> Headings { get; }

        // Properties: often needed headings
        public DataLogHeading SubjectHeading { get; private set; }
        public DataLogHeading TrialHeading { get; private set; }
        public DataLogHeading EventTypeHeading { get; private set; }
        public DataLogHeading CodeHeading { get; private set; }
        public DataLogHeading TimeHeading { get; private set; }


        // Methods
        private void TrySetOftenUsedHeadings()
        {
            SubjectHeading = Headings.FirstOrDefault(h => h.Text == "Subject");
            TrialHeading = Headings.FirstOrDefault(h => h.Text == "Trial");
            EventTypeHeading = Headings.FirstOrDefault(h => h.Text == "Event Type");
            CodeHeading = Headings.FirstOrDefault(h => h.Text == "Code");
            TimeHeading = Headings.FirstOrDefault(h => h.Text == "Time");
        }

        // Static methods

        // Try to find the line in the log file that contains the headings, this is based on the fixed data DefaultKnownHeadings
        public static DataLogHeadings TryFindDataLogFileHeadings(IList<DataLogLine> dataLogLines)
        {
            foreach (var dataLogLine in dataLogLines)
            {
                foreach (var lineDataValue in dataLogLine.SplittedLineDataValues)
                {
                    if (DefaultKnownHeadings.Contains(lineDataValue))
                    {
                        return new DataLogHeadings(dataLogLine);
                    }
                }
            }

            // return null if no headings were found
            return null;
        }


        // Some fixed data

        public static readonly HashSet<string> DefaultKnownHeadings = new HashSet<string>   // Een HashSet is een type waar o.a. Contains snel op is.
        {
            // Subject	Trial	Event Type	Code	Time	TTime	Uncertainty	Duration	Uncertainty	ReqTime	ReqDur
            // Event Type	Code	Type	Response	RT	RT Uncertainty	Time	Uncertainty	Duration	Uncertainty	ReqTime
            "Subject",
            "Trial",
            "Event Type",
            "Code",
            "Response",
            "Time",
            "TTime",
            "Uncertainty",
            "Duration",
            "ReqTime",
            "ReqDur",
        };
    }
}
