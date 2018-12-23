using Oracle.DataAccess.Client;

namespace csv_lib
{
    public class CsvColumn
    {
        public string Column { get; set; }

        public OracleDbType Type { get; set; }

        public CsvColumn()
        {
        }

        public CsvColumn(string column) : this()
        {
            Column = column;
        }

        public CsvColumn(string column, OracleDbType type) : this(column)
        {
            Type = type;
        }
    }
}