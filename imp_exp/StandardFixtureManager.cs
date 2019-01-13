using general;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace imp_exp
{
    public class StandardFixtureManager : ImportManager
    {
        private XDocumentExt fix_doc, enum_doc;

        private void SetFiles(string fix_file, string enum_file)
        {
            fix_doc = new XDocumentExt(fix_file);
            enum_doc = new XDocumentExt(enum_file);
        }

        private void ClearLoadData()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "pkg_sepo_import_global.clearstdfixturedata";
            command.ExecuteNonQuery();
        }

        private void LoadFieldsInfo()
        {
            XDocument xdoc = fix_doc.Document;
            XElement catalog = xdoc.Root;

            XName fields = fix_doc.GetXName("FieldsInfo");

            OracleCommand command = new OracleCommand();
            command.CommandText =
                @"insert into sepo_std_fields (field, f_longname, f_datatype, f_entermode, f_data)
                            values (:field, :f_longname, :f_datatype, :f_entermode, :f_data)";
            command.Connection = obj_lib.Module.Connection;

            OracleParameter p_field = new OracleParameter("field", OracleDbType.Varchar2);
            OracleParameter p_longname = new OracleParameter("f_longname", OracleDbType.Varchar2);
            OracleParameter p_datatype = new OracleParameter("f_datatype", OracleDbType.Varchar2);
            OracleParameter p_entermode = new OracleParameter("f_entermode", OracleDbType.Varchar2);
            OracleParameter p_data = new OracleParameter("f_data", OracleDbType.Varchar2);

            command.Parameters.AddRange(
                new OracleParameter[] {
                    p_field,
                    p_longname,
                    p_datatype,
                    p_entermode,
                    p_data
                }
            );

            foreach (var field in catalog.Element(fields).Elements())
            {
                p_field.Value = field.Name.LocalName;
                p_longname.Value = field.Attribute("F_LONGNAME").Value;
                p_datatype.Value = field.Attribute("F_DATATYPE").Value;
                p_entermode.Value = field.Attribute("F_ENTERMODE").Value;
                p_data.Value = (field.Attribute("F_DATA") != null) ? field.Attribute("F_DATA").Value : null;

                command.ExecuteNonQuery();
            }
        }

        private void LoadFolders()
        {
            XDocument xdoc = fix_doc.Document;
            XElement catalog = xdoc.Root;

            XName folders = fix_doc.GetXName("Folders");
            XName f_key = fix_doc.GetXName("F_KEY");
            XName f_name = fix_doc.GetXName("F_NAME");
            XName f_owner = fix_doc.GetXName("F_OWNER");
            XName f_level = fix_doc.GetXName("F_LEVEL");

            OracleCommand command = new OracleCommand();
            command.CommandText =
                @"insert into sepo_std_folders (f_key, f_name, f_owner, f_level)
                            values (:f_key, :f_name, :f_owner, :f_level)";
            command.Connection = obj_lib.Module.Connection;

            OracleParameter p_key = new OracleParameter("f_key", OracleDbType.Decimal);
            OracleParameter p_name = new OracleParameter("f_name", OracleDbType.Varchar2);
            OracleParameter p_owner = new OracleParameter("f_owner", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("f_level", OracleDbType.Decimal);

            command.Parameters.AddRange(new OracleParameter[] { p_key, p_name, p_owner, p_level });

            foreach (var folder in catalog.Element(folders).Elements())
            {
                p_key.Value = Convert.ToDecimal(folder.Element(f_key).Value);
                p_name.Value = folder.Element(f_name).Value;
                p_owner.Value = Convert.ToDecimal(folder.Element(f_owner).Value);
                p_level.Value = Convert.ToDecimal(folder.Element(f_level).Value);

                command.ExecuteNonQuery();
            }
        }

        private void LoadRecords()
        {
            XDocument xdoc = fix_doc.Document;
            XElement catalog = xdoc.Root;

            XName records = fix_doc.GetXName("Records");
            XName f_key = fix_doc.GetXName("F_KEY");
            XName f_level = fix_doc.GetXName("F_LEVEL");

            OracleCommand sq_command = new OracleCommand();
            sq_command.Connection = obj_lib.Module.Connection;
            sq_command.CommandText = "select sq_sepo_std_records.nextval from dual";

            OracleCommand command = new OracleCommand();
            command.CommandText =
                @"insert into sepo_std_records (id, f_key, f_level) values (:id, :f_key, :f_level)";
            command.Connection = obj_lib.Module.Connection;

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("f_key", OracleDbType.Decimal);
            OracleParameter p_level = new OracleParameter("f_level", OracleDbType.Decimal);

            command.Parameters.AddRange(new OracleParameter[] { p_id, p_key, p_level });

            OracleCommand fld_command = new OracleCommand();
            fld_command.Connection = obj_lib.Module.Connection;
            fld_command.CommandText = "select id, field from sepo_std_fields";

            Dictionary<decimal, XName> fields = new Dictionary<decimal, XName>();

            using (OracleDataReader rd = fld_command.ExecuteReader())
            {
                while (rd.Read())
                {
                    XName fname = fix_doc.GetXName(rd.GetString(1));
                    fields.Add(rd.GetDecimal(0), fname);
                }
            }

            OracleCommand rec_command = new OracleCommand();
            rec_command.Connection = obj_lib.Module.Connection;
            rec_command.CommandText =
                @"insert into sepo_std_record_contents (id_record, id_field, field_value)
                    values (:id_record, :id_field, :field_value)";

            OracleParameter p_record = new OracleParameter("id_record", OracleDbType.Decimal);
            OracleParameter p_field = new OracleParameter("id_field", OracleDbType.Decimal);
            OracleParameter p_value = new OracleParameter("field_value", OracleDbType.Varchar2);

            rec_command.Parameters.AddRange(new OracleParameter[] { p_record, p_field, p_value });

            foreach (var record in catalog.Element(records).Elements())
            {
                p_id.Value = sq_command.ExecuteScalar();
                p_key.Value = Convert.ToDecimal(record.Element(f_key).Value);
                p_level.Value = Convert.ToDecimal(record.Element(f_level).Value);

                command.ExecuteNonQuery();

                foreach (var i in fields)
                {
                    p_record.Value = p_id.Value;
                    p_field.Value = i.Key;
                    p_value.Value = record.Element(i.Value).Value;

                    rec_command.ExecuteNonQuery();
                }
            }
        }

        private void LoadTables()
        {
            XDocument xdoc = fix_doc.Document;
            XElement catalog = xdoc.Root;

            XName tables = fix_doc.GetXName("Tables");
            XName fields = fix_doc.GetXName("FieldsInfo");
            XName records = fix_doc.GetXName("Records");
            XName f_key = fix_doc.GetXName("F_KEY");
            XName f_table = fix_doc.GetXName("F_TABLE");
            XName f_descr = fix_doc.GetXName("F_DESCR");

            Dictionary<decimal, XName> dict_fields = new Dictionary<decimal, XName>();

            #region tables command

            OracleCommand sq_table = new OracleCommand();
            sq_table.Connection = obj_lib.Module.Connection;
            sq_table.CommandText = "select sq_sepo_std_tables.nextval from dual";

            OracleCommand command = new OracleCommand();
            command.CommandText =
                @"insert into sepo_std_tables (id, f_key, f_table, f_descr) values (:id, :f_key, :f_level, :f_descr)";
            command.Connection = obj_lib.Module.Connection;

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("f_key", OracleDbType.Decimal);
            OracleParameter p_table = new OracleParameter("f_table", OracleDbType.Varchar2);
            OracleParameter p_descr = new OracleParameter("f_descr", OracleDbType.Varchar2);

            command.Parameters.AddRange(new OracleParameter[] { p_id, p_key, p_table, p_descr });

            #endregion tables command

            #region fields command

            OracleCommand sq_table_fields = new OracleCommand();
            sq_table_fields.Connection = obj_lib.Module.Connection;
            sq_table_fields.CommandText = "select sq_sepo_std_table_fields.nextval from dual";

            OracleCommand field_command = new OracleCommand();
            field_command.CommandText =
                @"insert into sepo_std_table_fields
                    (id, id_table, field, f_longname, f_shortname, f_datatype, f_entermode, f_data,
                    enm_owner, enm_type)
                    values (:id, :id_table, :field, :f_longname, :f_shortname, :f_datatype, :f_entermode, :f_data,
                        :enm_owner, :enm_type)";

            field_command.Connection = obj_lib.Module.Connection;

            OracleParameter p_field_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_field = new OracleParameter("field", OracleDbType.Varchar2);
            OracleParameter p_longname = new OracleParameter("f_longname", OracleDbType.Varchar2);
            OracleParameter p_shortname = new OracleParameter("f_shortname", OracleDbType.Varchar2);
            OracleParameter p_datatype = new OracleParameter("f_datatype", OracleDbType.Varchar2);
            OracleParameter p_entermode = new OracleParameter("f_entermode", OracleDbType.Varchar2);
            OracleParameter p_data = new OracleParameter("f_data", OracleDbType.Varchar2);
            OracleParameter p_enm_owner = new OracleParameter("enm_owner", OracleDbType.Decimal);
            OracleParameter p_enm_type = new OracleParameter("enm_type", OracleDbType.Varchar2);

            field_command.Parameters.AddRange(
                new OracleParameter[] {
                    p_field_id, p_id, p_field, p_longname, p_shortname, p_datatype, p_entermode, p_data,
                    p_enm_owner, p_enm_type
                }
            );

            #endregion fields command

            #region records command

            OracleCommand sq_table_records = new OracleCommand();
            sq_table_records.Connection = obj_lib.Module.Connection;
            sq_table_records.CommandText = "select sq_sepo_std_table_records.nextval from dual";

            OracleCommand record_command = new OracleCommand();
            record_command.CommandText =
                @"insert into sepo_std_table_records (id, f_key, id_table) values (:id, :f_key, :id_table)";
            record_command.Connection = obj_lib.Module.Connection;

            OracleParameter p_record_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_record_key = new OracleParameter("f_key", OracleDbType.Decimal);

            record_command.Parameters.AddRange(new OracleParameter[] { p_record_id, p_record_key, p_id });

            #endregion records command

            #region record contents command

            OracleCommand content_command = new OracleCommand();
            content_command.CommandText =
                @"insert into sepo_std_table_rec_contents (id_record, id_field, field_value)
                    values (:id_record, :id_field, :field_value)";
            content_command.Connection = obj_lib.Module.Connection;

            OracleParameter p_field_value = new OracleParameter("field_value", OracleDbType.Varchar2);
            content_command.Parameters.AddRange(new OracleParameter[] { p_record_id, p_field_id, p_field_value });

            #endregion record contents command

            string data_value = String.Empty;
            string[] enm_params = null;
            int pos = -1;

            foreach (var table in catalog.Element(tables).Elements())
            {
                dict_fields.Clear();

                p_id.Value = sq_table.ExecuteScalar();
                p_key.Value = table.Element(f_key).Value;
                p_table.Value = table.Element(f_table).Value;
                p_descr.Value = table.Element(f_descr).Value;

                command.ExecuteNonQuery();

                foreach (var field in table.Element(fields).Elements())
                {
                    data_value = (field.Attribute("F_DATA") != null) ? field.Attribute("F_DATA").Value : null;

                    p_field_id.Value = sq_table_fields.ExecuteScalar();
                    p_field.Value = field.Name.LocalName;
                    p_longname.Value = field.Attribute("F_LONGNAME").Value;
                    p_shortname.Value = (field.Attribute("F_SHORTNAME") != null) ?
                        field.Attribute("F_SHORTNAME").Value : null;
                    p_datatype.Value = field.Attribute("F_DATATYPE").Value;
                    p_entermode.Value = field.Attribute("F_ENTERMODE").Value;
                    p_data.Value = data_value;
                    p_enm_owner.Value = null;
                    p_enm_type.Value = null;

                    if (p_entermode.Value.ToString() == "IEM_LIST")
                    {
                        enm_params = data_value.Split(',');
                        pos = enm_params[1].Trim().IndexOf('=');

                        p_enm_owner.Value = decimal.Parse(enm_params[1].Trim().Substring(pos + 1));
                        p_enm_type.Value = enm_params[2].Trim();
                    }

                    field_command.ExecuteNonQuery();

                    dict_fields.Add((decimal)p_field_id.Value, field.Name);
                }

                foreach (var record in table.Element(records).Elements())
                {
                    p_record_id.Value = sq_table_records.ExecuteScalar();
                    p_record_key.Value = record.Element(f_key).Value;

                    record_command.ExecuteNonQuery();

                    foreach (var content in dict_fields)
                    {
                        if (record.Element(content.Value) != null)
                        {
                            p_field_id.Value = content.Key;
                            p_field_value.Value = record.Element(content.Value).Value;
                            content_command.ExecuteNonQuery();
                        }
                        //else
                        //{
                        //p_field_value.Value = null;
                        //}
                    }
                }
            }

            // обновление связи записи с таблицей
            OracleCommand up_command = new OracleCommand();
            up_command.Connection = obj_lib.Module.Connection;
            up_command.CommandType = System.Data.CommandType.StoredProcedure;
            up_command.CommandText = "pkg_sepo_import_global.setidtableonrecord";
            up_command.ExecuteNonQuery();

            // построение схем на основе таблиц
            up_command.CommandType = System.Data.CommandType.StoredProcedure;
            up_command.CommandText = "pkg_sepo_import_global.buildstandardschemes";
            up_command.ExecuteNonQuery();
        }

        private void LoadEnums()
        {
            #region log

            //OracleConnection connection_log = new OracleConnection();
            //connection_log.ConnectionString = "Data Source=omega;User Id=omp_adm;Password=eastsoft";
            //connection_log.Open();

            //OracleCommand command_log = new OracleCommand();
            //command_log.Connection = connection_log;
            //command_log.CommandText = "insert into sepo_log(f_key) values (:f_key)";

            //OracleParameter p_key_ = new OracleParameter("f_key", OracleDbType.Decimal);

            //command_log.Parameters.Add(p_key_);

            #endregion log

            XDocument xdoc = enum_doc.Document;
            XElement catalog = xdoc.Root;

            XName folders = enum_doc.GetXName("Folders");
            XName lists = enum_doc.GetXName("Lists");
            XName records = enum_doc.GetXName("Records");
            XName f_key = enum_doc.GetXName("F_KEY");
            XName f_str = enum_doc.GetXName("F_STR");
            XName f_int = enum_doc.GetXName("F_INT");
            XName f_dbl = enum_doc.GetXName("F_DBL");
            XName f_name = enum_doc.GetXName("F_NAME");

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"insert into sepo_std_enum_folders (f_key, f_name, f_owner)
                                        values (:f_key, :f_name, :f_owner)";

            OracleParameter p_key = new OracleParameter("f_key", OracleDbType.Decimal);
            OracleParameter p_name = new OracleParameter("f_name", OracleDbType.Varchar2);
            OracleParameter p_owner = new OracleParameter("f_owner", OracleDbType.Decimal);
            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_int = new OracleParameter("f_int", OracleDbType.Varchar2);
            OracleParameter p_tcentity = new OracleParameter("tcentity", OracleDbType.Varchar2);
            OracleParameter p_id_enum = new OracleParameter("id_enum", OracleDbType.Decimal);
            OracleParameter p_str = new OracleParameter("f_str", OracleDbType.Varchar2);
            OracleParameter p_dbl = new OracleParameter("f_dbl", OracleDbType.Decimal);

            command.Parameters.AddRange(new OracleParameter[] { p_key, p_name, p_owner });

            foreach (var folder in catalog.Element(folders).Elements())
            {
                p_key.Value = folder.Attribute("F_KEY").Value;
                p_name.Value = folder.Attribute("F_NAME").Value;
                p_owner.Value = folder.Attribute("F_OWNER").Value;

                command.ExecuteNonQuery();
            }

            OracleCommand sq_command = new OracleCommand();
            sq_command.Connection = obj_lib.Module.Connection;
            sq_command.CommandText = "select sq_sepo_std_enum_list.nextval from dual";

            command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText =
                @"insert into sepo_std_enum_list (id, f_key, f_name, f_owner, f_int, tcentity)
                    values (:id, :f_key, :f_name, :f_owner, :f_int, :tcentity)";

            command.Parameters.AddRange(new OracleParameter[] {
                p_id, p_key, p_name, p_owner, p_int, p_tcentity
            });

            OracleCommand rec_command = new OracleCommand();
            rec_command.Connection = obj_lib.Module.Connection;
            rec_command.CommandText =
                @"insert into sepo_std_enum_contents (id_enum, f_key, f_str, f_int, f_dbl, f_name)
                    values (:id_enum, :f_key, :f_str, :f_int, :f_dbl, :f_name)";

            rec_command.Parameters.AddRange(new OracleParameter[] {
                p_id_enum, p_key, p_str, p_int, p_dbl, p_name
            });

            string value = String.Empty;

            foreach (var lookup in catalog.Element(lists).Elements())
            {
                #region log

                //p_key_.Value = lookup.Attribute("F_KEY").Value;
                //command_log.ExecuteNonQuery();

                #endregion log

                p_id.Value = sq_command.ExecuteScalar();
                p_key.Value = lookup.Attribute("F_KEY").Value;
                p_name.Value = lookup.Attribute("F_NAME").Value;
                p_owner.Value = lookup.Attribute("F_OWNER").Value;
                p_int.Value = lookup.Attribute("F_INT").Value;
                p_tcentity.Value = (lookup.Attribute("TcEntity") == null) ? null : lookup.Attribute("TcEntity").Value;

                command.ExecuteNonQuery();

                string[] types = p_int.Value.ToString().Split(',');

                if (lookup.Element(records) != null)
                {
                    foreach (var record in lookup.Element(records).Elements())
                    {
                        p_id_enum.Value = p_id.Value;
                        p_key.Value = record.Element(f_key).Value;

                        p_str.Value = null;
                        p_int.Value = null;
                        p_dbl.Value = null;
                        p_name.Value = null;

                        foreach (var type in types)
                        {
                            value = String.Empty;

                            switch (type.Trim())
                            {
                                case "LL_STRING":
                                    value = record.Element(f_str).Value;
                                    p_str.Value = (value == String.Empty) ? null : value;
                                    break;

                                case "LL_INTEGER":
                                    value = record.Element(f_int).Value;
                                    p_int.Value = (value == String.Empty) ? null : value;
                                    break;

                                case "LL_DOUBLE":
                                    value = record.Element(f_dbl).Value;
                                    p_dbl.Value = (value == String.Empty) ? null : value;
                                    break;

                                case "LL_NAMED":
                                    value = record.Element(f_name).Value;
                                    p_name.Value = (value == String.Empty) ? null : value;
                                    break;

                                default:
                                    break;
                            }
                        }

                        rec_command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateDB()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = obj_lib.Module.Connection;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pkg_sepo_import_global.setstdfixobjparams";

            cmd.ExecuteNonQuery();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pkg_sepo_import_global.setoldfixobjparams";

            cmd.ExecuteNonQuery();
        }

        public StandardFixtureManager()
        {
        }

        public bool VerifyXml(string file)
        {
            return true;
        }

        public void LoadFromXml(string file, string enumerations)
        {
            if (!VerifyXml(file)) throw new XmlVerifyException();

            SetFiles(file, enumerations);
#if DEBUG
            OpenLogSession();
            Log("Загрузка данных стандартной оснастки");
            Log("Удаление данных");
#endif
            ClearLoadData();
#if DEBUG
            Log("Загрузка атрибутов");
#endif
            LoadFieldsInfo();
#if DEBUG
            Log("Загрузка каталогов");
#endif
            LoadFolders();
#if DEBUG
            Log("Загрузка записей");
#endif
            LoadRecords();
#if DEBUG
            Log("Загрузка таблиц");
#endif
            LoadTables();
#if DEBUG
            Log("Загрузка списков");
#endif
            LoadEnums();
#if DEBUG
            Log("Загрузка данных стандартной оснастки завершена");
#endif
        }

        public void Import()
        {
        }

        public void LoadFromCsv(string file)
        {
        }

        public void Load()
        {
        }
    }
}