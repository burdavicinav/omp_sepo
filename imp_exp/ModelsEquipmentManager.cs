using Oracle.DataAccess.Client;
using System;
using System.Xml.Linq;

namespace imp_exp
{
    public class ModelsEquipmentManager : IImportManager
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
            OracleCommand del_command = new OracleCommand(
                        "delete from sepo_eqp_model_folders",
                        Module.Connection
                        );
            del_command.ExecuteNonQuery();

            del_command.CommandText = "delete from sepo_eqp_model_records";
            del_command.ExecuteNonQuery();

            OracleCommand command = new OracleCommand(
                @"insert into sepo_eqp_model_folders (f_key, f_owner, f_level, f_name)
                            values (:key, :owner, :level_, :name)",
                Module.Connection
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

            //

            command.Parameters.Clear();
            command.CommandText = @"insert into sepo_eqp_model_records
                                    (f_key, f_level, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11)
                                    values (:key, :level_, :f1, :f2, :f3, :f4, :f5, :f6, :f7, :f8, :f9, :f10, :f11)";

            OracleParameter p_f1 = new OracleParameter("f1", OracleDbType.Varchar2);
            OracleParameter p_f2 = new OracleParameter("f2", OracleDbType.Varchar2);
            OracleParameter p_f3 = new OracleParameter("f3", OracleDbType.Varchar2);
            OracleParameter p_f4 = new OracleParameter("f4", OracleDbType.Varchar2);
            OracleParameter p_f5 = new OracleParameter("f5", OracleDbType.Varchar2);
            OracleParameter p_f6 = new OracleParameter("f6", OracleDbType.Varchar2);
            OracleParameter p_f7 = new OracleParameter("f7", OracleDbType.Varchar2);
            OracleParameter p_f8 = new OracleParameter("f8", OracleDbType.Varchar2);
            OracleParameter p_f9 = new OracleParameter("f9", OracleDbType.Varchar2);
            OracleParameter p_f10 = new OracleParameter("f10", OracleDbType.Varchar2);
            OracleParameter p_f11 = new OracleParameter("f11", OracleDbType.Varchar2);

            command.Parameters.AddRange(
                new OracleParameter[] { p_key, p_level, p_f1, p_f2, p_f3, p_f4, p_f5, p_f6, p_f7,
                                        p_f8, p_f9, p_f10, p_f11 });

            XName records = XName.Get("Records", xml_namespace);
            XName f1 = XName.Get("F1", xml_namespace);
            XName f2 = XName.Get("F2", xml_namespace);
            XName f3 = XName.Get("F3", xml_namespace);
            XName f4 = XName.Get("F4", xml_namespace);
            XName f5 = XName.Get("F5", xml_namespace);
            XName f6 = XName.Get("F6", xml_namespace);
            XName f7 = XName.Get("F7", xml_namespace);
            XName f8 = XName.Get("F8", xml_namespace);
            XName f9 = XName.Get("F9", xml_namespace);
            XName f10 = XName.Get("F10", xml_namespace);
            XName f11 = XName.Get("F11", xml_namespace);

            foreach (var record in catalog.Element(records).Elements())
            {
                p_key.Value = record.Element(f_key).Value;
                p_level.Value = record.Element(f_level).Value;
                p_f1.Value = record.Element(f1).Value;
                p_f2.Value = record.Element(f2).Value;
                p_f3.Value = record.Element(f3).Value;
                p_f4.Value = record.Element(f4).Value;
                p_f5.Value = record.Element(f5).Value;
                p_f6.Value = record.Element(f6).Value;
                p_f7.Value = record.Element(f7).Value;
                p_f8.Value = record.Element(f8).Value;
                p_f9.Value = record.Element(f9).Value;
                p_f10.Value = record.Element(f10).Value;
                p_f11.Value = record.Element(f11).Value;

                command.ExecuteNonQuery();
            }
        }

        public void LoadFromCsv(string file)
        {
        }

        public void Load()
        {
        }

        public void Load(decimal classify, decimal owner)
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleParameter p_classify = new OracleParameter("p_classify", classify);
                    OracleParameter p_owner = new OracleParameter("p_owner", owner);

                    OracleCommand command = new OracleCommand();
                    command.Connection = Module.Connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "pkg_sepo_import_global.cleareqpmodels";
                    command.Parameters.Add(p_classify);
                    command.ExecuteNonQuery();

                    command.CommandText = "pkg_sepo_import_global.loadeqpmodelcatalogs";
                    command.Parameters.Add(p_owner);
                    command.ExecuteNonQuery();

                    command.Parameters.Clear();
                    command.CommandText = "pkg_sepo_import_global.loadeqpmodels";
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