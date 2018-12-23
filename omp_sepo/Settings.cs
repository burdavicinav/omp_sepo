using general;
using System.Collections.Generic;
using System.Xml.Linq;

namespace omp_sepo
{
    public class Settings
    {
        public XDocument SettingsDocument { get; private set; }

        public string ProgramName
        {
            get
            {
                XElement program = SettingsDocument.Root;
                XElement program_text = program.Element("ProgramText");

                return program_text.Value;
            }
        }

        public ConnectionObject[] Connections
        {
            get
            {
                List<ConnectionObject> con_list = new List<ConnectionObject>();

                XElement program = SettingsDocument.Root;
                XElement connections = program.Element("Connections");

                foreach (var xml_con in connections.Elements())
                {
                    ConnectionObject s_data = new ConnectionObject();
                    s_data.Name = xml_con.Attribute("name").Value;
                    s_data.Tns = xml_con.Attribute("tns").Value;

                    con_list.Add(s_data);
                }

                return con_list.ToArray();
            }
        }

        public Settings()
        {
        }

        public Settings(string settings_file)
            : this()
        {
            SettingsDocument = XDocument.Load(settings_file);
        }
    }
}