using Oracle.DataAccess.Client;
using System;
using System.Xml.Linq;

namespace imp_exp
{
    public class ProfessionsManager :
        IExportManager,
        IImportManager
    {
        public bool VerifyXml(string file)
        {
            //XDocument doc = XDocument.Load(file);
            //XElement professions = doc.Root;

            //if (!professions.HasElements) return false;

            //if (professions.Element("Profession") == null) return false;
            return true;
        }

        public ProfessionsManager()
        {
        }

        public void ExportToXml(string file)
        {
            XDocument doc = new XDocument();
            XElement professions = new XElement("Professions");

            OracleCommand command = new OracleCommand(
                "select profcode, name from professions order by profcode",
                obj_lib.Module.Connection
                );

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                XElement profession = new XElement("Profession");

                XElement code = new XElement("Code");
                code.Value = reader.GetInt64(0).ToString();

                XElement name = new XElement("Name");
                name.Value = reader.GetString(1);

                profession.Add(code);
                profession.Add(name);

                professions.Add(profession);
            }

            doc.Add(professions);
            doc.Save(file);
        }

        public void LoadFromXml(string file)
        {
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand del_command = new OracleCommand(
                        "delete from sepo_professions",
                        obj_lib.Module.Connection
                        );

                    del_command.ExecuteNonQuery();

                    OracleCommand command = new OracleCommand();
                    command.CommandText =
                        "insert into sepo_professions (prof_code, prof_name) values (:code, :name)";
                    command.Connection = obj_lib.Module.Connection;

                    OracleParameter p_code = new OracleParameter("code", OracleDbType.Decimal);
                    OracleParameter p_name = new OracleParameter("name", OracleDbType.Varchar2);

                    command.Parameters.AddRange(new OracleParameter[] { p_code, p_name });

                    if (!VerifyXml(file)) throw new XmlVerifyException();

                    XDocument doc = XDocument.Load(file);
                    XElement professions = doc.Root;

                    foreach (var prof in professions.Elements())
                    {
                        p_code.Value = Convert.ToDecimal(prof.Element("Code").Value);
                        p_name.Value = prof.Element("Name").Value;

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
            using (OracleTransaction transaction = obj_lib.Module.Connection.BeginTransaction())
            {
                try
                {
                    OracleCommand command = new OracleCommand();
                    command.Connection = obj_lib.Module.Connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "pkg_sepo_import_global.clearprofessions";
                    command.ExecuteNonQuery();

                    command.CommandText = "pkg_sepo_import_global.loadprofessions";
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