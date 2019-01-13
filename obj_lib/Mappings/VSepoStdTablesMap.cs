using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoStdTablesMap : ClassMap<V_SEPO_STD_TABLES>
    {
        public VSepoStdTablesMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.ID);
            Map(x => x.ID_RECORD);
            Map(x => x.F_LEVEL);
            Map(x => x.F_TABLE);
            Map(x => x.F_DESCR);
        }
    }
}