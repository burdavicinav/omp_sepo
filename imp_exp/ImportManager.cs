using Oracle.DataAccess.Client;

namespace imp_exp
{
    public class ImportManager
    {
        protected OracleConnection ora_log;

        protected void OpenLogSession()
        {
            ora_log = obj_lib.Module.Connection.Clone() as OracleConnection;
            ora_log.Open();
        }

        protected void CloseLogSession()
        {
            if (ora_log != null) ora_log.Close();
        }

        protected virtual void Log(string msg)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = ora_log;
            cmd.CommandText = "insert into sepo_import_log (msg) values (:msg)";

            cmd.Parameters.Add("msg", msg);
            cmd.ExecuteNonQuery();
        }
    }
}