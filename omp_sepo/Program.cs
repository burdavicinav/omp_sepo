#if DEBUG

using Oracle.DataAccess.Client;
using System;
using System.Windows.Forms;

#else

using ora_dialog;
using System;
using System.Reflection;
using System.Windows.Forms;

#endif

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
            OracleConnectionStringBuilder oraBuilder = new OracleConnectionStringBuilder
            {
                DataSource = "omega",
                UserID = "omp_adm",
                Password = "eastsoft"
            };

            OracleConnection connection = new OracleConnection(oraBuilder.ToString());
            connection.Open();

            // контекст
            obj_lib.Module.ConnectionString = oraBuilder.ConnectionString;

            // дополнительное подключение
            obj_lib.Module.Connection = connection;

            // запуск приложения
            Application.Run(new MainForm());
#else

            try
            {
                MainDialog dialog = new MainDialog(
                    Assembly.GetExecutingAssembly().GetName().Version,
                    Settings.Connections);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // соединение с Oracle
                    obj_lib.Module.Connection = dialog.Connection;

                    // контекст
                    obj_lib.Module.ConnectionString = dialog.ConnectionString;

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