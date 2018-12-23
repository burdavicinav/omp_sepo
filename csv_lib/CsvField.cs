namespace csv_lib
{
    public class CsvField
    {
        public object Value { get; private set; }

        public CsvRecord Record { get; private set; }

        public CsvField(CsvRecord record, object value)
        {
            Record = record;
            Value = value;
            if (value != null)
            {
                Value = (value.ToString() == "NULL") ? null : value;
            }
        }
    }
}