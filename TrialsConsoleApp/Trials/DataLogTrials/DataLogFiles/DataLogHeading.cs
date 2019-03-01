namespace TrialsConsoleApp.Trials.DataLogTrials.DataLogFiles
{
    public class DataLogHeading
    {
        public DataLogHeading(DataLogHeadings parentDataLogHeadings, int columnIndex)
        {
            ParentDataLogHeadings = parentDataLogHeadings;
            ColumnIndex = columnIndex;
        }

        public DataLogHeadings ParentDataLogHeadings { get; }
        public int ColumnIndex { get; }

        public string Text => ParentDataLogHeadings.HeadingsDataLogLine.GetString(ColumnIndex);
    }
}
