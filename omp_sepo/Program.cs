using obj_lib;
using ora_dialog;
using System;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace omp_sepo
{
    public static class Program
    {
        public static Settings Settings { get; private set; }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // настройки
            Settings = new Settings("settings.xml");

            System.Environment.SetEnvironmentVariable("NLS_LANG", "AMERICAN_AMERICA.CL8MSWIN1251");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            //ui_lib.MdiFormBase form = new ui_lib.MdiFormBase();
            //Application.Run(new ui_lib.MdiFormBase());

            // соединение с Oracle
            OracleConnectionStringBuilder oraBuilder = new OracleConnectionStringBuilder();
            oraBuilder.DataSource = "omega";
            oraBuilder.UserID = "omp_adm";
            oraBuilder.Password = "eastsoft";

            OracleConnection connection = new OracleConnection(oraBuilder.ToString());
            connection.Open();

            Module.Connection = connection;

            // контекст
            OmpModel ent = new OmpModel();
            ent.Database.Connection.ConnectionString = oraBuilder.ConnectionString;

            Module.Context = ent;

            // запуск приложения
            Application.Run(new MainForm());
#else

            try
            {
                MainDialog dialog = new MainDialog(Settings.Connections);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // соединение с Oracle
                    Module.Connection = dialog.Connection;

                    // контекст
                    OmpModel ent = new OmpModel();
                    ent.Database.Connection.ConnectionString = dialog.ConnectionString;

                    Module.Context = ent;

                    // запуск приложения
                    Application.Run(new MainForm());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
#endif
        }
    }
}