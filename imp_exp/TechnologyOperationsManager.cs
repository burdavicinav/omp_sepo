using Oracle.DataAccess.Client;
using System;
using System.Xml.Linq;

namespace imp_exp
{
    public class TechnologyOperationsManager : IImportManager
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

        private void LoadFolders(string file)
        {
            OracleCommand command = new OracleCommand(
                "delete from sepo_oper_folders",
                Module.Connection
                );
            command.ExecuteNonQuery();

            XDocument doc = XDocument.Load(file);
            string xml_namespace = GetXmlNamespace(doc);

            XElement folders = doc.Root.Element(XName.Get("Folders", xml_namespace));

            OracleCommand folderCommand = new OracleCommand(
                @"insert into sepo_oper_folders (f_key, f_owner, f_level, f_name)
                    values (:key, :owner, :level_, :name)",
                Module.Connection
                );

            OracleParameter p_key = new OracleParameter("key", OracleDbType.Decimal);
            OracleParameter p_owner = new OracleParameter("owner", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("level_", OracleDbType.Decimal);
            OracleParameter p_name = new OracleParameter("name", OracleDbType.Varchar2);

            folderCommand.Parameters.AddRange(new OracleParameter[] { p_key, p_owner, p_level, p_name });

            XElement f_key, f_owner, f_level, f_name;
            foreach (XElement item in folders.Elements())
            {
                f_key = item.Element(XName.Get("F_KEY", xml_namespace));
                f_owner = item.Element(XName.Get("F_OWNER", xml_namespace));
                f_level = item.Element(XName.Get("F_LEVEL", xml_namespace));
                f_name = item.Element(XName.Get("F_NAME", xml_namespace));

                p_key.Value = f_key.Value;
                p_owner.Value = f_owner.Value;
                p_level.Value = f_level.Value;
                p_name.Value = f_name.Value;

                folderCommand.ExecuteNonQuery();
            }
        }

        private void LoadRecords(string file)
        {
            OracleCommand command = new OracleCommand(
                "delete from sepo_professions_on_opers",
                Module.Connection
                );
            command.ExecuteNonQuery();

            command.CommandText = "delete from sepo_oper_recs";
            command.ExecuteNonQuery();

            XDocument doc = XDocument.Load(file);
            string xml_namespace = GetXmlNamespace(doc);

            XElement records = doc.Root.Element(XName.Get("Records", xml_namespace));
            XElement f_key, f_level, f1, f2, f3, f4;

            OracleCommand recCommand = new OracleCommand(
                @"insert into sepo_oper_recs (f_key, f_level, f1, f2, f3, f4)
                    values (:key, :level_, :f1, :f2, :f3, :f4)",
                Module.Connection
                );

            OracleParameter p_key = new OracleParameter("key", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("level_", OracleDbType.Decimal);
            OracleParameter p_f1 = new OracleParameter("f1", OracleDbType.Varchar2);
            OracleParameter p_f2 = new OracleParameter("f2", OracleDbType.Varchar2);
            OracleParameter p_f3 = new OracleParameter("f3", OracleDbType.Varchar2);
            OracleParameter p_f4 = new OracleParameter("f4", OracleDbType.Varchar2);

            recCommand.Parameters.AddRange(new OracleParameter[] { p_key, p_level, p_f1, p_f2, p_f3, p_f4 });

            foreach (XElement item in records.Elements())
            {
                f_key = item.Element(XName.Get("F_KEY", xml_namespace));
                f_level = item.Element(XName.Get("F_LEVEL", xml_namespace));
                f1 = item.Element(XName.Get("F1", xml_namespace));
                f2 = item.Element(XName.Get("F2", xml_namespace));
                f3 = item.Element(XName.Get("F3", xml_namespace));
                f4 = item.Element(XName.Get("F4", xml_namespace));

                if (f3.Value != String.Empty)
                {
                    string[] professions = f3.Value.Split(',');

                    OracleCommand profCommand = new OracleCommand();
                    profCommand.Connection = Module.Connection;
                    profCommand.CommandText =
                        @"insert into sepo_professions_on_opers (id_oper, id_prof)
                            select sq_sepo_oper_recs.currval, id FROM sepo_professions
                            where prof_code IN (";

                    for (int i = 0; i < professions.Length; i++)
                    {
                        profCommand.CommandText += ":" + (i + 1);
                        if (i < professions.Length - 1)
                        {
                            profCommand.CommandText += ",";
                        }
                        else
                        {
                            profCommand.CommandText += ")";
                        }

                        profCommand.Parameters.Add(
                            ":" + (i + 1),
                            Convert.ToDecimal(professions[i].Trim())
                            );
                    }
                    profCommand.ExecuteNonQuery();
                }

                p_key.Value = f_key.Value;
                p_level.Value = f_level.Value;
                p_f1.Value = f1.Value;
                p_f2.Value = f2.Value;
                p_f3.Value = f3.Value;
                p_f4.Value = f4.Value;

                recCommand.ExecuteNonQuery();
            }
        }

        private void LoadCodes(string file)
        {
            OracleCommand command = new OracleCommand(
                "delete from sepo_oper_folder_codes",
                Module.Connection
                );
            command.ExecuteNonQuery();

            XDocument doc = XDocument.Load(file);
            string xml_namespace = GetXmlNamespace(doc);

            XElement operations = doc.Root.Element(XName.Get("Operations", xml_namespace));

            OracleCommand folderCommand = new OracleCommand(
                @"insert into sepo_oper_folder_codes (f_level, f_code)
                    values (:level_, :code)",
                Module.Connection
                );

            OracleParameter p_level = new OracleParameter("level_", OracleDbType.Decimal);
            OracleParameter p_code = new OracleParameter("code", OracleDbType.Varchar2);

            folderCommand.Parameters.AddRange(new OracleParameter[] { p_level, p_code });

            foreach (XElement item in operations.Elements())
            {
                p_level.Value = item.Attribute(XName.Get("f_level", xml_namespace)).Value;
                p_code.Value = item.Attribute(XName.Get("f_code", xml_namespace)).Value;

                folderCommand.ExecuteNonQuery();
            }
        }

        public bool VerifyXml(string file)
        {
            return true;
        }

        public void LoadFromXml(string file)
        {
        }

        public void LoadFromXml(string file_opers, string file_codes)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    if (!VerifyXml(file_opers)) throw new XmlVerifyException();

                    LoadFolders(file_opers);
                    LoadRecords(file_opers);
                    LoadCodes(file_codes);

                    transaction.Commit();
                }
                catch (OracleException)
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

        public void Load(decimal? opertype)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = Module.Connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.clearoperations";
                    command.ExecuteNonQuery();

                    command.CommandText = "pkg_sepo_import_global.clearopercatalogs";
                    command.ExecuteNonQuery();

                    command.CommandText = "pkg_sepo_import_global.loadopercatalogs";
                    command.ExecuteNonQuery();

                    command.CommandText = "pkg_sepo_import_global.loadoperations";
                    OracleParameter p_opertype = new OracleParameter("p_opertype", OracleDbType.Decimal);
                    p_opertype.Value = opertype;

                    command.Parameters.Add(p_opertype);
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (OracleException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}