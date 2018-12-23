using obj_lib;
using Oracle.DataAccess.Client;

namespace omp_sepo
{
    public static class Module
    {
        public static OracleConnection Connection { get; set; }

        public static OmpModel Context { get; set; }
    }
}