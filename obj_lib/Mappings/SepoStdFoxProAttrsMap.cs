using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoStdFoxProAttrsMap : ClassMap<SEPO_STD_FOXPRO_ATTRS>
    {
        public SepoStdFoxProAttrsMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID).GeneratedBy.Sequence("sq_sepo_std_foxpro_attrs");

            Map(x => x.NAME)
                .Length(255)
                .Not.Nullable();

            Map(x => x.SHORTNAME)
                .Length(15)
                .Not.Nullable();

            Map(x => x.TYPE_)
                .Not.Nullable();
        }
    }
}