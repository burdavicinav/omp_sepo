using Oracle.DataAccess.Client;
using System;
using System.Xml.Linq;

namespace imp_exp
{
    public class TechnologyStepsManager : IImportManager
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

        private void LoadXmlSteps(string file)
        {
            OracleCommand del_command = new OracleCommand(
                        "delete from sepo_tech_step_texts",
                        obj_lib.Module.Connection
                        );
            del_command.ExecuteNonQuery();

            del_command.CommandText = "delete from sepo_tech_steps";
            del_command.ExecuteNonQuery();

            OracleCommand command = new OracleCommand(
                @"insert into sepo_tech_steps (f_key, f_owner, f_level, f_name)
                            values (:key, :owner, :level_, :name)",
                obj_lib.Module.Connection
                );

            OracleParameter p_key = new OracleParameter("key", OracleDbType.Decimal);
            OracleParameter p_owner = new OracleParameter("owner", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("level_", OracleDbType.Decimal);
            OracleParameter p_name = new OracleParameter("name", OracleDbType.Varchar2);

            command.Parameters.AddRange(new OracleParameter[] { p_key, p_owner, p_level, p_name });

            XDocument doc = XDocument.Load(file);
            XElement catalog = doc.Root;
            string xml_namespace = GetXmlNamespace(doc);

            XName folders = XName.Get("Folders", xml_namespace);
            XName f_key = XName.Get("F_KEY", xml_namespace);
            XName f_owner = XName.Get("F_OWNER", xml_namespace);
            XName f_level = XName.Get("F_LEVEL", xml_namespace);
            XName f_name = XName.Get("F_NAME", xml_namespace);

            foreach (var folder in catalog.Element(folders).Elements())
            {
                p_key.Value = Convert.ToDecimal(folder.Element(f_key).Value);
                p_owner.Value = Convert.ToDecimal(folder.Element(f_owner).Value);
                p_level.Value = Convert.ToDecimal(folder.Element(f_level).Value);
                p_name.Value = folder.Element(f_name).Value;

                command.ExecuteNonQuery();
            }
        }

        private void LoadXmlStepTexts(string file)
        {
            OracleCommand command_al = new OracleCommand();
            command_al.Connection = obj_lib.Module.Connection;

            command_al.CommandText =
                @"insert into sepo_tech_step_texts (f_key, f_level, f_type, f_numbered, f_blob)
                            values (:key, :level_, :type_, :numbered, :blob)";

            OracleParameter p_key = new OracleParameter("key", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("level_", OracleDbType.Decimal);
            OracleParameter p_numbered = new OracleParameter("numbered", OracleDbType.Varchar2);
            OracleParameter p_type = new OracleParameter("type_", OracleDbType.Varchar2);
            OracleParameter p_blob = new OracleParameter("blob", OracleDbType.Clob);

            command_al.Parameters.AddRange(
                new OracleParameter[] { p_key, p_level, p_type, p_numbered, p_blob });

            XDocument doc_al = XDocument.Load(file);
            string xml_namespace = GetXmlNamespace(doc_al);

            XName texts = XName.Get("Texts", xml_namespace);

            foreach (var oper in doc_al.Root.Elements())
            {
                p_level.Value = Convert.ToDecimal(oper.Attribute("F_LEVEL").Value);
                p_type.Value = oper.Attribute("F_TYPE").Value;
                p_numbered.Value = oper.Attribute("F_NUMBERED").Value;

                if (oper.HasElements)
                {
                    foreach (var text in oper.Element(texts).Elements())
                    {
                        p_key.Value = Convert.ToDecimal(text.Attribute("F_KEY").Value);
                        p_blob.Value = text.Attribute("F_BLOB").Value;
                    }
                }
                else
                {
                    p_key.Value = null;
                    p_blob.Value = null;
                }

                command_al.ExecuteNonQuery();
            }
        }

        public bool VerifyXml(string file)
        {
            return true;
        }

        public void LoadFromXml(string file)
        {
        }

        public void LoadFromXml(string file, string file_dop)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    LoadXmlSteps(file);
                    LoadXmlStepTexts(file_dop);

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

        public void Load(int is_load_classify_catalogs = 1)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = obj_lib.Module.Connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.clearsteps";
                    command.ExecuteNonQuery();

                    OracleParameter p_load_classify =
                        new OracleParameter("p_is_load_classify_group", OracleDbType.Int16);
                    p_load_classify.Value = is_load_classify_catalogs;

                    command.Parameters.Add(p_load_classify);
                    command.CommandText = "pkg_sepo_import_global.loadsteps";
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}