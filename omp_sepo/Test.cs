using System;
using System.Windows.Forms;

namespace omp_sepo
{
    public class Test
    {
        public void Zip()
        {
            try
            {
                //OracleConnection con = new OracleConnection("Data Source=omega;User Id=omp_adm;Password=eastsoft");
                //con.Open();

                //OracleCommand cmd = new OracleCommand("select steptext_rtf from technological_steps where code = 98081");
                //cmd.Connection = con;

                //using (OracleDataReader rd = cmd.ExecuteReader())
                //{
                //    rd.Read();
                //    OracleBlob blb = rd.GetOracleBlob(0);

                //    FileStream fstr = File.Create("pereh");
                //    fstr.Write(blb.Value, 0, (int)blb.Length);
                //    fstr.Close();
                //}

                //FileStream file_ = File.Open("D:\\pereh", FileMode.Open);

                //OracleCommand cmd = new OracleCommand("update technological_steps set steptext_rtf=:param where code = 98081");
                //cmd.Connection = con;
                //OracleParameter param = new OracleParameter("param", OracleDbType.Blob);

                //byte[] bytes = new byte[file_.Length];
                //file_.Read(bytes, 0, bytes.Length);

                //param.Value = bytes;
                //cmd.Parameters.Add(param);

                //cmd.ExecuteNonQuery();

                //FileStream file = File.Create("D:\\pereh_unzip");
                //BZip2InputStream fout = new BZip2InputStream(file_);

                //fout.IsStreamOwner = true;

                //BZip2.Decompress(file_, file, false);
                //StreamUtils.Copy(fout, file, new byte[4096]);
                //;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}