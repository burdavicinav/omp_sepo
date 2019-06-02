using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace obj_lib
{
    public class Module
    {
        private static ISession _session;

        public static string ConnectionString { get; set; }

        public static ISession OpenSession()
        {
            if (_session == null)
            {
                ISessionFactory sessionFatory =
                    Fluently.Configure()
                        .Database(OracleDataClientConfiguration.Oracle10.ConnectionString(ConnectionString))
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Module>())
                        .Mappings(m => m.HbmMappings.AddFromAssemblyOf<Module>())
                        .BuildSessionFactory();

                _session = sessionFatory.OpenSession();
            }

            _session.Clear();

            return _session;
        }

        public static Oracle.DataAccess.Client.OracleConnection Connection { get; set; }
    }
}