using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SpcSectionsMap : ClassMap<SPC_SECTIONS>
    {
        public SpcSectionsMap()
        {
            Schema("OMP_ADM");

            Id(x => x.CODE).GeneratedBy.Sequence("sq_spcsections_code");

            Map(x => x.NAME)
                .Not.Nullable()
                .Length(100);

            Map(x => x.DEFTYPE);
            Map(x => x.NUM);
            Map(x => x.ORDERNUM)
                .Not.Nullable();

            Map(x => x.UNDERLINE)
                .Not.Nullable();

            Map(x => x.ROWS_BEFORE)
                .Not.Nullable();

            Map(x => x.ROWS_AFTER)
                .Not.Nullable();
        }
    }
}