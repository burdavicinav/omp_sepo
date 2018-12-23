using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csv_lib
{
    public class CsvFile
    {
        private StreamReader stream;

        public char Separator { get; set; }

        public CsvColumn[] Columns { get; set; }

        public CsvRecord[] Records
        {
            get
            {
                List<CsvRecord> records = new List<CsvRecord>();

                while (!stream.EndOfStream)
                {
                    CsvRecord record = new CsvRecord(this);
                    record.String = stream.ReadLine();

                    records.Add(record);
                }

                return records.ToArray();
            }
        }

        public CsvFile(string file, Encoding encoding, char separator)
        {
            Separator = separator;
            stream = new StreamReader(file, encoding);
        }

        public CsvFile(string file, char separator)
            : this(file, Encoding.Default, separator)
        {
        }

        public CsvFile(string file, char separator, CsvColumn[] columns) : this(file, separator)
        {
            Columns = columns;
        }

        public void Close()
        {
            stream.Close();
        }

        public void Load(string table, OracleConnection connection)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = connection;

            StringBuilder command_text = new StringBuilder();
            command_text.Append("insert into ");
            command_text.Append(table);
            command_text.Append(" (");

            for (int i = 0; i < Columns.Length; i++)
            {
                command_text.Append(Columns[i].Column);
                command_text.Append((i < Columns.Length - 1) ? ',' : ')');
            }

            command_text.Append(" values (");

            for (int i = 0; i < Columns.Length; i++)
            {
                command_text.Append(":");
                command_text.Append(Columns[i].Column);
                command_text.Append((i < Columns.Length - 1) ? ',' : ')');

                OracleParameter param = new OracleParameter(
                    Columns[i].Column,
                    null
                    );
                command.Parameters.Add(param);
            }

            command.CommandText = command_text.ToString();

            CsvRecord[] records = Records;
            foreach (var record in records)
            {
                CsvField[] fields = record.Fields;
                for (int i = 0; i < Columns.Length; i++)
                {
                    command.Parameters[i].Value = fields[i].Value;
                }

                command.ExecuteNonQuery();
            }
        }
    }
}