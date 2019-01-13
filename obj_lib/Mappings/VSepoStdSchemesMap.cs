using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoStdSchemesMap : ClassMap<V_SEPO_STD_SCHEMES>
    {
        public VSepoStdSchemesMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.ID_RECORD).GeneratedBy.Assigned();
            Map(x => x.F_TABLE);
            Map(x => x.H_LEVEL);
            Map(x => x.F_NAME);
            Map(x => x.TBL_DESCR);
            Map(x => x.SCHEME_NAME);
            Map(x => x.OMP_NAME);
            Map(x => x.ISTABLE);
            Map(x => x.ISEDIT);
        }
    }
}