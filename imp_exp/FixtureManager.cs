using csv_lib;
using Oracle.DataAccess.Client;
using System;

namespace imp_exp
{
    public class FixtureManager
    {
        private void LoadOsnAll(string file)
        {
            (new OracleCommand("delete from sepo_osn_all", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("art_id"),
                new CsvColumn("doc_id"),
                new CsvColumn("isp_code"),
                new CsvColumn("designation"),
                new CsvColumn("name"),
                new CsvColumn("okp_code"),
                new CsvColumn("imbase_key"),
                new CsvColumn("purchased"),
                new CsvColumn("massa"),
                new CsvColumn("mu_id"),
                new CsvColumn("section_id"),
                new CsvColumn("note"),
                new CsvColumn("expanding"),
                new CsvColumn("litera"),
                new CsvColumn("mmr"),
                new CsvColumn("art_ver_id"),
                new CsvColumn("author"),
                new CsvColumn("chkindate"),
                new CsvColumn("pr_id"),
                new CsvColumn("need_svo"),
                new CsvColumn("modifdate"),
                new CsvColumn("modifuser_id"),
                new CsvColumn("baseart_id"),
                new CsvColumn("art_class"),
                new CsvColumn("serial_no"),
                new CsvColumn("set_no")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_all", Module.Connection);

            csv.Close();
        }

        private void LoadOsnDet(string file)
        {
            (new OracleCommand("delete from sepo_osn_det", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("art_id"),
                new CsvColumn("s_material"),
                new CsvColumn("dce"),
                new CsvColumn("izd")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_det", Module.Connection);

            csv.Close();
        }

        private void LoadOsnDocs(string file)
        {
            (new OracleCommand("delete from sepo_osn_docs", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("doc_id"),
                new CsvColumn("archive_id"),
                new CsvColumn("filename"),
                new CsvColumn("arc_dir_id"),
                new CsvColumn("wrk_dir_id"),
                new CsvColumn("designatio"),
                new CsvColumn("name"),
                new CsvColumn("format"),
                new CsvColumn("designerid"),
                new CsvColumn("doc_type"),
                new CsvColumn("doc_status"),
                new CsvColumn("revision"),
                new CsvColumn("note"),
                new CsvColumn("version_id"),
                new CsvColumn("createdate"),
                new CsvColumn("modifydate"),
                new CsvColumn("modifyuser_id"),
                new CsvColumn("otd_status"),
                new CsvColumn("otd_reg"),
                new CsvColumn("otd_annul"),
                new CsvColumn("otd_reg_user"),
                new CsvColumn("otd_annul_user"),
                new CsvColumn("canc_status"),
                new CsvColumn("invisible")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_docs", Module.Connection);

            csv.Close();
        }

        private void LoadOsnSe(string file)
        {
            (new OracleCommand("delete from sepo_osn_se", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("art_id"),
                new CsvColumn("s_material"),
                new CsvColumn("naim_dse"),
                new CsvColumn("field1"),
                new CsvColumn("field3"),
                new CsvColumn("field2"),
                new CsvColumn("dce"),
                new CsvColumn("izd")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_se", Module.Connection);

            csv.Close();
        }

        private void LoadOsnSostav(string file)
        {
            (new OracleCommand("delete from sepo_osn_sostav", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("prjlink_id"),
                new CsvColumn("proj_aid"),
                new CsvColumn("part_aid"),
                new CsvColumn("count_pc"),
                new CsvColumn("mu_id"),
                new CsvColumn("razdel"),
                new CsvColumn("position"),
                new CsvColumn("note"),
                new CsvColumn("variants"),
                new CsvColumn("link_type"),
                new CsvColumn("format"),
                new CsvColumn("pr_id"),
                new CsvColumn("f_start_dt"),
                new CsvColumn("f_finish_dt"),
                new CsvColumn("order_id"),
                new CsvColumn("ctx_id"),
                new CsvColumn("ctx_fl"),
                new CsvColumn("opt_link"),
                new CsvColumn("author"),
                new CsvColumn("proj_ver_id")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_sostav", Module.Connection);

            csv.Close();
        }

        private void LoadOsnSp(string file)
        {
            (new OracleCommand("delete from sepo_osn_sp", Module.Connection)).ExecuteNonQuery();

            CsvColumn[] columns = {
                new CsvColumn("art_id"),
                new CsvColumn("s_material"),
                new CsvColumn("field1"),
                new CsvColumn("field2"),
                new CsvColumn("field3"),
                new CsvColumn("dce"),
                new CsvColumn("izd")
            };

            CsvFile csv = new CsvFile(file, '\t', columns);
            csv.Load("sepo_osn_sp", Module.Connection);

            csv.Close();
        }

        private void LoadOsnTypes()
        {
            OracleCommand del_command = new OracleCommand(
                "delete from sepo_osn_types",
                Module.Connection
                );
            del_command.ExecuteNonQuery();

            OracleCommand command = new OracleCommand(
                @"insert into sepo_osn_types (shortname)
                    SELECT
                      Upper(SubStr(osn_type, 1, 1))
                    FROM
                      v_sepo_osn_se_sp
                    WHERE
                        osn_type IS NOT NULL
                    GROUP BY
                      Upper(SubStr(osn_type, 1, 1))",
                Module.Connection
                );

            command.ExecuteNonQuery();
        }

        public void LoadFromFiles(
            string file_all,
            string file_det,
            string file_docs,
            string file_se,
            string file_sostav,
            string file_sp
            )
        {
            using (OracleTransaction transaction = Module.Connection.BeginTransaction())
            {
                try
                {
                    LoadOsnAll(file_all);
                    LoadOsnDet(file_det);
                    LoadOsnDocs(file_docs);
                    LoadOsnSe(file_se);
                    LoadOsnSostav(file_sostav);
                    LoadOsnSp(file_sp);
                    LoadOsnTypes();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void LoadFromCsv(string file_all)
        {
        }
    }
}