using System;
using System.Collections.Generic;

namespace csv_lib
{
    public class CsvRecord
    {
        public CsvFile File { get; private set; }

        public string String { get; set; }

        public CsvField[] Fields
        {
            get
            {
                string[] str_list = String.Split(File.Separator);
                List<CsvField> fields = new List<CsvField>();

                foreach (var str in str_list)
                {
                    CsvField field = new CsvField(this, str);
                    fields.Add(field);
                }

                return fields.ToArray();
            }
        }

        public CsvRecord(CsvFile file)
        {
            File = file;
        }
    }
}