using general;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

using ICSharpCode.SharpZipLib.BZip2;
using obj_lib;

namespace imp_exp
{
    public enum TPImportGroup { TP, DCE }

    public class TPManager : ImportManager
    {
        public delegate void TPManagerEventHandler(object s, EventArgs e);

        //public event TPManagerEventHandler StartImporting, ImportingTP;

        private Dictionary<decimal, decimal> tp_objects;

        //private Dictionary<decimal, string> tp_entities;

        public TPManager()
        {
        }

        private void DisableConstraints()
        {
        }

        private void ClearLoadData()
        {
#if DEBUG
            Log("Удаление данных");
#endif
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "pkg_sepo_import_global.cleartpdata";

            command.ExecuteNonQuery();
        }

        private void LoadTPList(string tp_file, TPImportGroup group)
        {
#if DEBUG
            Log("Загрузка списка ТП");
#endif
            tp_objects = new Dictionary<decimal, decimal>();

            XDocumentExt tp_doc = new XDocumentExt(tp_file);

            XDocument xdoc = tp_doc.Document;
            XElement tp_element = xdoc.Root;
            XName techpr = tp_doc.GetXName("TechProcess");
            XName articles = tp_doc.GetXName("Articles");

            OracleCommand sq_cmd = new OracleCommand();
            sq_cmd.Connection = obj_lib.Module.Connection;
            sq_cmd.CommandText = "select sq_sepo_tech_processes.nextval from dual";

            OracleCommand tp_cmd = new OracleCommand();
            tp_cmd.Connection = obj_lib.Module.Connection;
            tp_cmd.CommandText =
                @"insert into sepo_tech_processes
                    (id, key_, designation, name, doc_id, kind, production_id, version_key)
                    values (:id, :key_, :designation, :name, :doc_id, :kind, :production_id, :version_key)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_designation = new OracleParameter("designation", OracleDbType.Varchar2);
            OracleParameter p_name = new OracleParameter("name", OracleDbType.Varchar2);
            OracleParameter p_doc_id = new OracleParameter("doc_id", OracleDbType.Decimal);
            OracleParameter p_kind = new OracleParameter("kind", OracleDbType.Int16);
            OracleParameter p_production_id = new OracleParameter("production_id", OracleDbType.Decimal);
            OracleParameter p_version_key = new OracleParameter("version_key", OracleDbType.Decimal);

            tp_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_key,
                p_designation,
                p_name,
                p_doc_id,
                p_kind,
                p_production_id,
                p_version_key
            });

            OracleCommand art_cmd = new OracleCommand();
            art_cmd.Connection = obj_lib.Module.Connection;
            art_cmd.CommandText =
                @"insert into sepo_tp_to_dce (id_tp, key_, designation, name, art_id)
                    values (:id_tp, :key_, :designation, :name, :art_id)";

            OracleParameter p_art_id = new OracleParameter("art_id", OracleDbType.Decimal);

            art_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_key,
                p_designation,
                p_name,
                p_art_id
                });

            XElement xarticles = null;

            foreach (var tp in tp_element.Elements())
            {
                p_id.Value = sq_cmd.ExecuteScalar();
                p_key.Value = tp.Attribute("Key").Value;
                p_designation.Value = tp.Attribute("Designation").Value;
                p_name.Value = tp.Attribute("Name").Value;
                p_doc_id.Value = tp.Attribute("DocId").Value;
                p_kind.Value = tp.Attribute("Kind").Value;
                p_production_id.Value = tp.Attribute("ProductionId").Value;
                p_version_key.Value = tp.Attribute("VersionKey").Value;

                tp_cmd.ExecuteNonQuery();

                tp_objects.Add((decimal)p_id.Value, decimal.Parse(p_key.Value.ToString()));

                xarticles = tp.Element(articles);
                if (xarticles != null)
                {
                    foreach (var art in xarticles.Elements())
                    {
                        p_key.Value = art.Attribute("Key").Value;
                        p_designation.Value = art.Attribute("Designation").Value;
                        p_name.Value = art.Attribute("Name").Value;
                        p_art_id.Value = art.Attribute("ArtId").Value;

                        art_cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadEntities(string ent_file)
        {
#if DEBUG
            Log("Загрузка сущностей");
#endif
            //tp_entities = new Dictionary<decimal, string>();

            XDocumentExt tp_doc = new XDocumentExt(ent_file);

            XDocument xdoc = tp_doc.Document;
            XElement entities = xdoc.Root;
            XName legend = tp_doc.GetXName("Legend");
            XName entity = tp_doc.GetXName("Entity");

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"insert into sepo_tp_entities_legend (f_recordid, f_type, f_name, f_tblkey)
                                    values (:f_recordid, :f_type, :f_name, :f_tblkey)";

            OracleParameter p_recordid = new OracleParameter("f_recordid", OracleDbType.Decimal);
            OracleParameter p_type = new OracleParameter("f_type", OracleDbType.Varchar2);
            OracleParameter p_name = new OracleParameter("f_name", OracleDbType.Varchar2);
            OracleParameter p_tblkey = new OracleParameter("f_tblkey", OracleDbType.Decimal);

            command.Parameters.AddRange(new OracleParameter[] { p_recordid, p_type, p_name, p_tblkey });

            foreach (var rec in entities.Element(legend).Elements())
            {
                p_recordid.Value = (rec.Attribute("F_RECORDID").Value != String.Empty) ?
                    rec.Attribute("F_RECORDID").Value : null;
                p_type.Value = rec.Attribute("F_TYPE").Value;
                p_name.Value = rec.Attribute("F_NAME").Value;
                p_tblkey.Value = (rec.Attribute("F_TBLKEY") != null) ? rec.Attribute("F_TBLKEY").Value : null;

                command.ExecuteNonQuery();
            }

            OracleCommand sq_cmd = new OracleCommand();
            sq_cmd.Connection = obj_lib.Module.Connection;
            sq_cmd.CommandText = "select sq_sepo_tp_entities.nextval from dual";

            OracleCommand ent_command = new OracleCommand();
            ent_command.Connection = obj_lib.Module.Connection;
            ent_command.CommandText = @"insert into sepo_tp_entities (id, f_code, f_name, f_recordid, f_record,
                                    f_type, f_reference, f_linkcode, f_field) values (:id, :p_code, :p_name,
                                    :p_recordid, :p_record, :p_type, :p_reference, :p_linkcode, :p_field)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_code = new OracleParameter("p_code", OracleDbType.Varchar2);
            OracleParameter p_record = new OracleParameter("p_record", OracleDbType.Varchar2);
            OracleParameter p_reference = new OracleParameter("p_reference", OracleDbType.Decimal);
            OracleParameter p_linkcode = new OracleParameter("p_linkcode", OracleDbType.Varchar2);
            OracleParameter p_field = new OracleParameter("p_field", OracleDbType.Decimal);

            ent_command.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_code,
                p_name,
                p_recordid,
                p_record,
                p_type,
                p_reference,
                p_linkcode,
                p_field
            });

            foreach (var ent in entities.Elements(entity))
            {
                p_id.Value = sq_cmd.ExecuteScalar();
                p_code.Value = ent.Attribute("F_CODE").Value;
                p_name.Value = ent.Attribute("F_NAME").Value;
                p_recordid.Value = (ent.Attribute("F_RECORDID") != null) ?
                    ent.Attribute("F_RECORDID").Value : null;
                p_record.Value = (ent.Attribute("F_RECORD") != null) ? ent.Attribute("F_RECORD").Value : null;
                p_type.Value = (ent.Attribute("F_TYPE") != null) ? ent.Attribute("F_TYPE").Value : null;
                p_reference.Value = (ent.Attribute("F_REFERENCE") != null) ?
                    ent.Attribute("F_REFERENCE").Value : null;
                p_linkcode.Value = (ent.Attribute("F_LINKCODE") != null) ?
                    ent.Attribute("F_LINKCODE").Value : null;
                p_field.Value = (ent.Attribute("F_FIELD") != null) ? ent.Attribute("F_FIELD").Value : null;

                ent_command.ExecuteNonQuery();

                //tp_entities.Add((decimal)p_id.Value, p_code.Value.ToString());
            }
        }

        private void LoadTpEntities(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка сущностей");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");

            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = @"insert into sepo_tp_fields (id_tp, field_name, f_value)
                                        values (:id_tp, :field_name, :f_value)";

            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            //OracleParameter p_id_field = new OracleParameter("id_field", OracleDbType.Decimal);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            command.Parameters.AddRange(new OracleParameter[] { p_id_tp, p_field_name, p_value });
            p_id_tp.Value = id_tp;

            OracleCommand comment_cmd = new OracleCommand();
            comment_cmd.Connection = obj_lib.Module.Connection;
            comment_cmd.CommandText = @"insert into sepo_tp_comments (id_tp, field_name, comment_)
                                        values (:id_tp, :field_name, :comment_)";

            OracleParameter p_comment = new OracleParameter("comment_", OracleDbType.Clob);

            comment_cmd.Parameters.AddRange(new OracleParameter[] { p_id_tp, p_field_name, p_comment });

            string code = null;

            if (tp.Element(entities) != null)
            {
                foreach (var ent in tp.Element(entities).Elements())
                {
                    code = ent.Attribute("Code").Value.ToString();

                    //p_id_field.Value = tp_entities.Where(x => x.Value == code).
                    //Select(x => x.Key).FirstOrDefault();

                    p_field_name.Value = code;

                    if (code.Contains("Ком"))
                    {
                        p_comment.Value = ent.Attribute("Value").Value;
                        comment_cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        p_value.Value = ent.Attribute("Value").Value;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadOpers(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка операций");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");
            XName opers = doc.GetXName("Opers");
            XName oper = doc.GetXName("Oper");

            if (tp.Element(opers) == null) return;

            OracleCommand sq_op_cmd = new OracleCommand();
            sq_op_cmd.Connection = obj_lib.Module.Connection;
            sq_op_cmd.CommandText = "select sq_sepo_tp_opers.nextval from dual";

            OracleCommand op_cmd = new OracleCommand();
            op_cmd.Connection = obj_lib.Module.Connection;
            op_cmd.CommandText = @"insert into sepo_tp_opers (id, id_tp, key_, reckey, order_, date_,
                                    num, place, tpkey) values (:id, :id_tp, :key_, :reckey, :order_, :date_,
                                    :num, :place, :tpkey)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_reckey = new OracleParameter("reckey", OracleDbType.Decimal);
            OracleParameter p_order = new OracleParameter("order_", OracleDbType.Decimal);
            OracleParameter p_date = new OracleParameter("date_", OracleDbType.Varchar2);
            OracleParameter p_num = new OracleParameter("num", OracleDbType.Varchar2);
            OracleParameter p_place = new OracleParameter("place", OracleDbType.Decimal);
            OracleParameter p_tpkey = new OracleParameter("tpkey", OracleDbType.Decimal);

            op_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_id_tp,
                p_key,
                p_reckey,
                p_order,
                p_date,
                p_num,
                p_place,
                p_tpkey
                });

            p_id_tp.Value = id_tp;

            OracleCommand op_ent_cmd = new OracleCommand();
            op_ent_cmd.Connection = obj_lib.Module.Connection;
            op_ent_cmd.CommandText = @"insert into sepo_tp_oper_fields (id_oper, field_name, f_value)
                                            values (:id_oper, :field_name, :f_value)";

            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            //OracleParameter p_id_field = new OracleParameter("id_field", OracleDbType.Decimal);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            op_ent_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_value });

            OracleCommand op_comment_cmd = new OracleCommand();
            op_comment_cmd.Connection = obj_lib.Module.Connection;
            op_comment_cmd.CommandText = @"insert into sepo_tp_oper_comments (id_oper, field_name, comment_)
                                            values (:id_oper, :field_name, :comment_)";

            OracleParameter p_comment = new OracleParameter("comment_", OracleDbType.Clob);

            op_comment_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_comment });

            string code = null;

            foreach (var op in tp.Element(opers).Elements())
            {
                p_id.Value = sq_op_cmd.ExecuteScalar();
                p_key.Value = op.Attribute("Key").Value;
                p_reckey.Value = op.Attribute("RecKey").Value;
                p_order.Value = op.Attribute("Order").Value;
                p_date.Value = op.Attribute("Date").Value;
                p_num.Value = op.Attribute("Number").Value;
                p_place.Value = op.Attribute("Place").Value;
                p_tpkey.Value = op.Attribute("TpKey").Value;

                op_cmd.ExecuteNonQuery();

                if (op.Element(entities) != null)
                {
                    foreach (var ent in op.Element(entities).Elements())
                    {
                        code = ent.Attribute("Code").Value.ToString();

                        p_field_name.Value = code;

                        //p_id_field.Value =
                        //    tp_entities.Where(x => x.Value == code).
                        //    Select(x => x.Key).FirstOrDefault();

                        if (code.Contains("Ком"))
                        {
                            p_comment.Value = ent.Attribute("Value").Value;
                            op_comment_cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            p_value.Value = ent.Attribute("Value").Value;
                            op_ent_cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadSteps(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка переходов");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");
            XName steps = doc.GetXName("Perehs");
            XName step = doc.GetXName("Pereh");

            if (tp.Element(steps) == null) return;

            OracleCommand sq_step_cmd = new OracleCommand();
            sq_step_cmd.Connection = obj_lib.Module.Connection;
            sq_step_cmd.CommandText = "select sq_sepo_tp_steps.nextval from dual";

            OracleCommand step_cmd = new OracleCommand();
            step_cmd.Connection = obj_lib.Module.Connection;
            step_cmd.CommandText = @"insert into sepo_tp_steps (id, id_tp, key_, reckey, order_, date_,
                                    num, operkey) values (:id, :id_tp, :key_, :reckey, :order_, :date_,
                                    :num, :operkey)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_reckey = new OracleParameter("reckey", OracleDbType.Decimal);
            OracleParameter p_order = new OracleParameter("order_", OracleDbType.Decimal);
            OracleParameter p_date = new OracleParameter("date_", OracleDbType.Varchar2);
            OracleParameter p_num = new OracleParameter("num", OracleDbType.Varchar2);
            OracleParameter p_operkey = new OracleParameter("operkey", OracleDbType.Decimal);

            step_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_id_tp,
                p_key,
                p_reckey,
                p_order,
                p_date,
                p_num,
                p_operkey
                });

            p_id_tp.Value = id_tp;

            OracleCommand step_ent_cmd = new OracleCommand();
            step_ent_cmd.Connection = obj_lib.Module.Connection;
            step_ent_cmd.CommandText = @"insert into sepo_tp_step_fields (id_step, field_name, f_value)
                                            values (:id_step, :field_name, :f_value)";

            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            step_ent_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_value });

            OracleCommand step_comment_cmd = new OracleCommand();
            step_comment_cmd.Connection = obj_lib.Module.Connection;
            step_comment_cmd.CommandText = @"insert into sepo_tp_step_comments (id_step, field_name, comment_)
                                            values (:id_step, :field_name, :comment_)";

            OracleParameter p_comment = new OracleParameter("comment_", OracleDbType.Clob);

            step_comment_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_comment });

            string code = null;

            foreach (var op in tp.Element(steps).Elements())
            {
                p_id.Value = sq_step_cmd.ExecuteScalar();
                p_key.Value = op.Attribute("Key").Value;
                p_reckey.Value = op.Attribute("RecKey").Value;
                p_order.Value = op.Attribute("Order").Value;
                p_date.Value = op.Attribute("Date").Value;
                p_num.Value = op.Attribute("Number").Value;
                p_operkey.Value = op.Attribute("OperKey").Value;

                step_cmd.ExecuteNonQuery();

                if (op.Element(entities) != null)
                {
                    foreach (var ent in op.Element(entities).Elements())
                    {
                        code = ent.Attribute("Code").Value.ToString();

                        p_field_name.Value = code;

                        if (code.Contains("Ком"))
                        {
                            p_comment.Value = ent.Attribute("Value").Value;
                            step_comment_cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            p_value.Value = ent.Attribute("Value").Value;
                            step_ent_cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void LoadWorkers(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка исполнителей");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");
            XName workers = doc.GetXName("Workers");
            XName worker = doc.GetXName("Worker");

            if (tp.Element(workers) == null) return;

            OracleCommand sq_wk_cmd = new OracleCommand();
            sq_wk_cmd.Connection = obj_lib.Module.Connection;
            sq_wk_cmd.CommandText = "select sq_sepo_tp_workers.nextval from dual";

            OracleCommand wk_cmd = new OracleCommand();
            wk_cmd.Connection = obj_lib.Module.Connection;
            wk_cmd.CommandText = @"insert into sepo_tp_workers (id, id_tp, key_, reckey, tblkey, order_, date_,
                                    kind, count_, operkey, perehkey) values (:id, :id_tp, :key_, :reckey, :tblkey,
                                    :order_, :date_, :kind, :count_, :operkey, :perehkey)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_reckey = new OracleParameter("reckey", OracleDbType.Decimal);
            OracleParameter p_tblkey = new OracleParameter("tblkey", OracleDbType.Decimal);
            OracleParameter p_order = new OracleParameter("order_", OracleDbType.Decimal);
            OracleParameter p_date = new OracleParameter("date_", OracleDbType.Varchar2);
            OracleParameter p_kind = new OracleParameter("kind", OracleDbType.Decimal);
            OracleParameter p_count = new OracleParameter("count_", OracleDbType.Decimal);
            OracleParameter p_operkey = new OracleParameter("operkey", OracleDbType.Decimal);
            OracleParameter p_perehkey = new OracleParameter("perehkey", OracleDbType.Decimal);

            wk_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_id_tp,
                p_key,
                p_reckey,
                p_tblkey,
                p_order,
                p_date,
                p_kind,
                p_count,
                p_operkey,
                p_perehkey
                });

            p_id_tp.Value = id_tp;

            OracleCommand wk_ent_cmd = new OracleCommand();
            wk_ent_cmd.Connection = obj_lib.Module.Connection;
            wk_ent_cmd.CommandText = @"insert into sepo_tp_worker_fields (id_worker, field_name, f_value)
                                            values (:id_worker, :field_name, :f_value)";

            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            wk_ent_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_value });

            string code = null;

            foreach (var op in tp.Element(workers).Elements())
            {
                p_id.Value = sq_wk_cmd.ExecuteScalar();
                p_key.Value = op.Attribute("Key").Value;
                p_reckey.Value = op.Attribute("RecKey").Value;
                p_tblkey.Value = op.Attribute("TblKey").Value;
                p_order.Value = op.Attribute("Order").Value;
                p_date.Value = op.Attribute("Date").Value;
                p_kind.Value = op.Attribute("Kind").Value;
                p_count.Value = op.Attribute("Count").Value;
                p_operkey.Value = op.Attribute("OperKey").Value;
                p_perehkey.Value = op.Attribute("PerehKey").Value;

                wk_cmd.ExecuteNonQuery();

                if (op.Element(entities) != null)
                {
                    foreach (var ent in op.Element(entities).Elements())
                    {
                        code = ent.Attribute("Code").Value.ToString();

                        p_field_name.Value = code;
                        p_value.Value = ent.Attribute("Value").Value;
                        wk_ent_cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadEquipments(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка оборудования");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");
            XName equipments = doc.GetXName("Equipments");
            XName equipment = doc.GetXName("Equipment");

            if (tp.Element(equipments) == null) return;

            OracleCommand sq_eq_cmd = new OracleCommand();
            sq_eq_cmd.Connection = obj_lib.Module.Connection;
            sq_eq_cmd.CommandText = "select sq_sepo_tp_equipments.nextval from dual";

            OracleCommand eq_cmd = new OracleCommand();
            eq_cmd.Connection = obj_lib.Module.Connection;
            eq_cmd.CommandText = @"insert into sepo_tp_equipments (id, id_tp, key_, reckey, order_, invnom, date_,
                                    operkey, perehkey) values (:id, :id_tp, :key_, :reckey, :order_, :invnom,
                                    :date_, :operkey, :perehkey)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_reckey = new OracleParameter("reckey", OracleDbType.Decimal);
            OracleParameter p_order = new OracleParameter("order_", OracleDbType.Decimal);
            OracleParameter p_invnom = new OracleParameter("invnom", OracleDbType.Decimal);
            OracleParameter p_date = new OracleParameter("date_", OracleDbType.Varchar2);
            OracleParameter p_operkey = new OracleParameter("operkey", OracleDbType.Decimal);
            OracleParameter p_perehkey = new OracleParameter("perehkey", OracleDbType.Decimal);

            eq_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_id_tp,
                p_key,
                p_reckey,
                p_order,
                p_invnom,
                p_date,
                p_operkey,
                p_perehkey
                });

            p_id_tp.Value = id_tp;

            OracleCommand eq_ent_cmd = new OracleCommand();
            eq_ent_cmd.Connection = obj_lib.Module.Connection;
            eq_ent_cmd.CommandText = @"insert into sepo_tp_equipment_fields (id_equipment, field_name, f_value)
                                            values (:id_equipment, :field_name, :f_value)";

            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            eq_ent_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_value });

            string code = null;

            foreach (var i in tp.Element(equipments).Elements())
            {
                p_id.Value = sq_eq_cmd.ExecuteScalar();
                p_key.Value = i.Attribute("Key").Value;
                p_reckey.Value = i.Attribute("RecKey").Value;
                p_order.Value = i.Attribute("Order").Value;
                p_invnom.Value = i.Attribute("InvNom").Value;
                p_date.Value = i.Attribute("Date").Value;
                p_operkey.Value = i.Attribute("OperKey").Value;
                p_perehkey.Value = i.Attribute("PerehKey").Value;

                eq_cmd.ExecuteNonQuery();

                if (i.Element(entities) != null)
                {
                    foreach (var ent in i.Element(entities).Elements())
                    {
                        code = ent.Attribute("Code").Value.ToString();

                        p_field_name.Value = code;
                        p_value.Value = ent.Attribute("Value").Value;
                        eq_ent_cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadTools(XDocumentExt doc, decimal id_tp)
        {
#if DEBUG
            Log("Загрузка оснастки");
#endif
            XDocument xdoc = doc.Document;
            XElement tp = xdoc.Root;

            XName entities = doc.GetXName("Entities");
            XName entity = doc.GetXName("Entity");
            XName tools = doc.GetXName("Tools");
            XName tool = doc.GetXName("Tool");

            if (tp.Element(tools) == null) return;

            OracleCommand sq_tool_cmd = new OracleCommand();
            sq_tool_cmd.Connection = obj_lib.Module.Connection;
            sq_tool_cmd.CommandText = "select sq_sepo_tp_tools.nextval from dual";

            OracleCommand tool_cmd = new OracleCommand();
            tool_cmd.Connection = obj_lib.Module.Connection;
            tool_cmd.CommandText = @"insert into sepo_tp_tools (id, id_tp, key_, reckey, tblkey, order_, date_,
                                    kind, count_, operkey, perehkey) values (:id, :id_tp, :key_, :reckey, :tblkey,
                                    :order_, :date_, :kind, :count_, :operkey, :perehkey)";

            OracleParameter p_id = new OracleParameter("id", OracleDbType.Decimal);
            OracleParameter p_id_tp = new OracleParameter("id_tp", OracleDbType.Decimal);
            OracleParameter p_key = new OracleParameter("key_", OracleDbType.Decimal);
            OracleParameter p_reckey = new OracleParameter("reckey", OracleDbType.Decimal);
            OracleParameter p_tblkey = new OracleParameter("tblkey", OracleDbType.Decimal);
            OracleParameter p_order = new OracleParameter("order_", OracleDbType.Decimal);
            OracleParameter p_date = new OracleParameter("date_", OracleDbType.Varchar2);
            OracleParameter p_kind = new OracleParameter("kind", OracleDbType.Decimal);
            OracleParameter p_count = new OracleParameter("count_", OracleDbType.Decimal);
            OracleParameter p_operkey = new OracleParameter("operkey", OracleDbType.Decimal);
            OracleParameter p_perehkey = new OracleParameter("perehkey", OracleDbType.Decimal);

            tool_cmd.Parameters.AddRange(new OracleParameter[] {
                p_id,
                p_id_tp,
                p_key,
                p_reckey,
                p_tblkey,
                p_order,
                p_date,
                p_kind,
                p_count,
                p_operkey,
                p_perehkey
                });

            p_id_tp.Value = id_tp;

            OracleCommand tool_ent_cmd = new OracleCommand();
            tool_ent_cmd.Connection = obj_lib.Module.Connection;
            tool_ent_cmd.CommandText = @"insert into sepo_tp_tool_fields (id_tool, field_name, f_value)
                                            values (:id_step, :field_name, :f_value)";

            OracleParameter p_field_name = new OracleParameter("field_name", OracleDbType.Varchar2);
            OracleParameter p_value = new OracleParameter("f_value", OracleDbType.Varchar2);

            tool_ent_cmd.Parameters.AddRange(new OracleParameter[] { p_id, p_field_name, p_value });

            string code = null;

            foreach (var op in tp.Element(tools).Elements())
            {
                p_id.Value = sq_tool_cmd.ExecuteScalar();
                p_key.Value = op.Attribute("Key").Value;
                p_reckey.Value = op.Attribute("RecKey").Value;
                p_tblkey.Value = op.Attribute("TblKey").Value;
                p_order.Value = op.Attribute("Order").Value;
                p_date.Value = op.Attribute("Date").Value;
                p_kind.Value = op.Attribute("Kind").Value;
                p_count.Value = op.Attribute("Count").Value;
                p_operkey.Value = op.Attribute("OperKey").Value;
                p_perehkey.Value = op.Attribute("PerehKey").Value;

                tool_cmd.ExecuteNonQuery();

                if (op.Element(entities) != null)
                {
                    foreach (var ent in op.Element(entities).Elements())
                    {
                        code = ent.Attribute("Code").Value.ToString();

                        p_field_name.Value = code;
                        p_value.Value = ent.Attribute("Value").Value;
                        tool_ent_cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void LoadTP(string tp_file)
        {
#if DEBUG
            StringBuilder sb = new StringBuilder("Загрузка файла ТП: ");
            sb.Append(tp_file);
            Log(sb.ToString());
#endif
            XDocumentExt tp_doc = new XDocumentExt(tp_file);
            XDocument xdoc = tp_doc.Document;
            XElement tp = xdoc.Root;

            if (tp.Attribute("Key").Value == null) return;

            decimal key = decimal.Parse(tp.Attribute("Key").Value.ToString());
            decimal id_tp = tp_objects.Where(x => x.Value == key).Select(x => x.Key).FirstOrDefault();

            LoadTpEntities(tp_doc, id_tp);

            LoadOpers(tp_doc, id_tp);
            LoadSteps(tp_doc, id_tp);
            LoadWorkers(tp_doc, id_tp);
            LoadEquipments(tp_doc, id_tp);
            LoadTools(tp_doc, id_tp);
        }

        private void LoadTpDir(string tp_dir, string tp_prefix)
        {
            string[] tp_files = Directory.GetFiles(tp_dir, tp_prefix + "*");

            foreach (var tp in tp_files)
            {
                LoadTP(tp);
            }
        }

        private void UpdateTpLinks()
        {
#if DEBUG
            Log("Обновление ссылок");
#endif
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "pkg_sepo_import_global.updatetplinks";

            command.ExecuteNonQuery();
        }

        public void ParsingStdFixFormuls()
        {
#if DEBUG
            Log("Анализ формул стандартной оснастки");
#endif
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "pkg_sepo_import_global.parsingstdfixformuls";

            command.ExecuteNonQuery();
        }

        public void LoadFromXml(
            string tp_file,
            TPImportGroup tp_group,
            string ent_file,
            string tp_dir,
            string tp_prefix = "TC")
        {
#if DEBUG
            OpenLogSession();
            Log("Начало загрузки");
#endif

            //OnImporting();

            ClearLoadData();

            //OnTpImporting();

            LoadTPList(tp_file, tp_group);

            LoadEntities(ent_file);

            LoadTpDir(tp_dir, tp_prefix);

            //UpdateTpLinks();
#if DEBUG
            Log("Окончание загрузки");
            CloseLogSession();
#endif
        }

        public void Load(decimal groupcode, decimal letter, decimal state, decimal owner)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = obj_lib.Module.Connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.importtp";

                    command.Parameters.Add("p_groupcode", groupcode);
                    command.Parameters.Add("p_letter", letter);
                    command.Parameters.Add("p_state", state);
                    command.Parameters.Add("p_owner", owner);
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

        public void StepTextToRTF()
        {
            string steptext = String.Empty;
            string rtf = String.Empty;
            decimal code;
            int k = 0;

            RichTextBox box = new RichTextBox
            {
                Font = new System.Drawing.Font("Times New Roman", 9)
            };

            OracleCommand command = new OracleCommand();
            command.Connection = Module.Connection;
            command.CommandText = @"select stepcode, new_stepname, new_steptext
                                                    from omp_adm.v_sepo_tp_diff_steptext";

            OracleCommand txtCommand = new OracleCommand();
            txtCommand.Connection = Module.Connection;
            txtCommand.CommandText = @"update omp_adm.steps_for_oper_steptext
                                                    set steptext_rtf = :txt where stepcode = :code";

            OracleParameter p_txt = new OracleParameter("txt", OracleDbType.Blob);
            OracleParameter p_code = new OracleParameter("code", OracleDbType.Decimal);

            txtCommand.Parameters.AddRange(new OracleParameter[] { p_txt, p_code });

            OracleCommand stepCommand = new OracleCommand();
            stepCommand.Connection = Module.Connection;
            stepCommand.CommandText = @"update omp_adm.steps_for_operation
                                                    set name = :name, texttype = 1
                                                        where code = :code";

            OracleParameter p_name = new OracleParameter("name", OracleDbType.Varchar2);
            OracleParameter p_scode = new OracleParameter("code", OracleDbType.Decimal);

            stepCommand.Parameters.AddRange(new OracleParameter[] { p_name, p_scode });

            using (var transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        k++;

                        if (!reader.IsDBNull(2))
                        {
                            code = reader.GetDecimal(0);
                            steptext = reader.GetString(2);

                            p_scode.Value = code;
                            p_name.Value = reader.GetString(1);

                            // обновление наименования
                            stepCommand.ExecuteNonQuery();

                            box.Text = steptext;

                            rtf = box.Rtf;
                            int length = rtf.Length;

                            Stream inStream = new MemoryStream(Encoding.Default.GetBytes(rtf));
                            Stream outStream = new MemoryStream();

                            BZip2.Compress(inStream, outStream, false, 9);

                            outStream.Seek(0, SeekOrigin.Begin);

                            byte[] archBytes = new byte[outStream.Length];
                            outStream.Read(archBytes, 0, archBytes.Length);

                            byte[] lenBytes = BitConverter.GetBytes(length);
                            byte[] bytes = { 0x78, 0x56, 0x34, 0x12 };

                            p_txt.Value = (lenBytes.Concat(bytes)).Concat(archBytes).ToArray();
                            p_code.Value = code;

                            // обновление текста перехода в rtf формате
                            txtCommand.ExecuteNonQuery();

                            // освобождение потоков
                            inStream.Dispose();
                            outStream.Dispose();
                        }
                    }

                    reader.Dispose();

                    // завершение транзакции
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