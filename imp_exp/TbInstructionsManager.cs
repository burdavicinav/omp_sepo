using Oracle.DataAccess.Client;
using System;
using System.Xml.Linq;

namespace imp_exp
{
    public class TbInstructionsManager : IImportManager
    {
        private string GetXmlNamespace(XDocument doc)
        {
            string xml_namespace = String.Empty;

            XAttribute xmlns_attr = doc.Root.Attribute("xmlns");
            if (xmlns_attr != null)
            {
                xml_namespace = xmlns_attr.Value;
            }

            return xml_namespace;
        }

        public bool VerifyXml(string file)
        {
            return true;
        }

        public void LoadFromXml(string file)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand del_command = new OracleCommand(
                        "delete from sepo_instructions_tb",
                        Module.Connection
                        );

                    del_command.ExecuteNonQuery();

                    OracleCommand command = new OracleCommand();
                    command.CommandText =
                        @"insert into sepo_instructions_tb (f_key, f_name, f_owner, f_level)
                            values (:f_key, :f_name, :f_owner, :f_level)";
                    command.Connection = Module.Connection;

                    OracleParameter p_key = new OracleParameter("f_key", OracleDbType.Decimal);
                    OracleParameter p_name = new OracleParameter("f_name", OracleDbType.Varchar2);
                    OracleParameter p_owner = new OracleParameter("f_owner", OracleDbType.Decimal);
                    OracleParameter p_level = new OracleParameter("f_level", OracleDbType.Decimal);

                    command.Parameters.AddRange(new OracleParameter[] { p_key, p_name, p_owner, p_level });

                    if (!VerifyXml(file)) throw new XmlVerifyException();

                    XDocument doc = XDocument.Load(file);
                    XElement dictionaries = doc.Root;
                    string xml_namespace = GetXmlNamespace(doc);
                    XElement instructions = null;

                    foreach (var dict in dictionaries.Elements())
                    {
                        string dict_name = dict.Attribute("F_TABLE").Value;
                        if (dict_name == "CTL000115") instructions = dict;
                    }

                    XName folders = XName.Get("Folders", xml_namespace);

                    foreach (var folder in instructions.Element(folders).Elements())
                    {
                        p_key.Value = Convert.ToDecimal(folder.Attribute("F_KEY").Value);
                        p_name.Value = folder.Attribute("F_NAME").Value;
                        p_owner.Value = Convert.ToDecimal(folder.Attribute("F_OWNER").Value);
                        p_level.Value = Convert.ToDecimal(folder.Attribute("F_LEVEL").Value);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void LoadFromCsv(string file)
        {
        }

        public void Load()
        {
        }

        public void Load(decimal p_state, decimal p_owner)
        {
            OracleCommand ld_command = new OracleCommand();
            ld_command.Connection = Module.Connection;
            ld_command.CommandType = System.Data.CommandType.StoredProcedure;
            ld_command.CommandText = "pkg_sepo_import_global.loadinstructionstb";

            OracleParameter p_st = new OracleParameter("p_state", p_state);
            OracleParameter p_ow = new OracleParameter("p_owner", p_owner);

            ld_command.Parameters.Add(p_ow);
            ld_command.Parameters.Add(p_st);

            ld_command.ExecuteNonQuery();
        }

        public TbInstructionsManager()
        {
        }
    }
}