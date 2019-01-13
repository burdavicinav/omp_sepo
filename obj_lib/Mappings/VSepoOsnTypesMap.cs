using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoOsnTypesMap : ClassMap<V_SEPO_OSN_TYPES>
    {
        public VSepoOsnTypesMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.ID);

            Map(x => x.SHORTNAME)
                .Length(100)
                .Not.Nullable();

            Map(x => x.OMP_CODE);
            Map(x => x.OMP_NAME);
        }
    }
}