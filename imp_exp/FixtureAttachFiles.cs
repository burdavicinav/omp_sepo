using Oracle.DataAccess.Client;
using System.IO;
using System.Security.Cryptography;

namespace imp_exp
{
    public class FixtureAttachFiles
    {
        public int CountFiles
        {
            get
            {
                OracleCommand command = new OracleCommand();
                command.Connection = obj_lib.Module.Connection;
                command.CommandText =
                    "select count(*) from (select doc_id, filename from v_sepo_fixture_docs group by doc_id, filename)";

                return (int)command.ExecuteScalar();
            }
        }

        public void CreateDocuments(string dir_path)
        {
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandText = "delete from sepo_osn_docs_link_omp";
            command.ExecuteNonQuery();

            DirectoryInfo dir = new DirectoryInfo(dir_path);

            OracleCommand doc_command = new OracleCommand();
            doc_command.Connection = obj_lib.Module.Connection;
            doc_command.CommandType = System.Data.CommandType.StoredProcedure;
            doc_command.CommandText = "pkg_sepo_import_global.createdocument";

            OracleParameter p_data = new OracleParameter("p_data", OracleDbType.Blob);
            OracleParameter p_doc = new OracleParameter("p_doc", OracleDbType.Int32);
            OracleParameter p_name = new OracleParameter("p_name", OracleDbType.Varchar2);
            OracleParameter p_hash = new OracleParameter("p_hash", OracleDbType.Varchar2);

            doc_command.Parameters.AddRange(new OracleParameter[] { p_data, p_doc, p_name, p_hash });

            OracleCommand file_command = new OracleCommand();
            file_command.Connection = obj_lib.Module.Connection;
            file_command.CommandText = "select doc_id, filename from v_sepo_fixture_docs group by doc_id, filename";

            OracleDataReader reader = file_command.ExecuteReader();
            while (reader.Read())
            {
                long doc = reader.GetInt32(0);
                string filename = reader.GetString(1);

                FileInfo[] files = dir.GetFiles(filename, SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    FileStream stream = new FileStream(files[0].FullName, FileMode.Open);
                    byte[] bytes = new byte[stream.Length];

                    stream.Read(bytes, 0, (int)stream.Length);

                    SHA1 s = new SHA1CryptoServiceProvider();
                    byte[] hash = s.ComputeHash(bytes);

                    p_data.Value = bytes;
                    p_doc.Value = doc;
                    p_name.Value = filename;
                    p_hash.Value = System.BitConverter.ToString(hash).Replace("-", "");

                    doc_command.ExecuteNonQuery();

                    stream.Close();
                }
            }
        }

        public void AttachDocuments()
        {
            OracleCommand command = new OracleCommand();
            command.Connection = obj_lib.Module.Connection;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "pkg_sepo_import_global.attachfixturedocuments";
            command.ExecuteNonQuery();
        }
    }
}