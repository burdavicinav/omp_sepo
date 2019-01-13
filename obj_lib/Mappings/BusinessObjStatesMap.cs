using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class BusinessObjStatesMap : ClassMap<BUSINESSOBJ_STATES>
    {
        public BusinessObjStatesMap()
        {
            Schema("OMP_ADM");

            Id(x => x.CODE).GeneratedBy.Sequence("sq_bobj_states_code");

            Map(x => x.BOTYPE)
                .Not.Nullable();

            Map(x => x.NAME)
                .Length(40)
                .Not.Nullable();

            Map(x => x.SHORTNAME)
                .Length(15)
                .Not.Nullable();

            Map(x => x.RIGHTS_SCHEME);
            Map(x => x.MINSTATE)
                .Not.Nullable();

            References(x => x.ADDITIONAL_TO);
            Map(x => x.DESCRIPTION)
                .Length(100);

            Map(x => x.PROMLEVEL)
                .Not.Nullable();

            Map(x => x.RDATE)
                .Not.Nullable();

            Map(x => x.NOTE)
                .Length(40);

            Map(x => x.REVCOUNT)
                .Not.Nullable();

            Map(x => x.REVCOUNT_OTHER)
                .Not.Nullable();

            Map(x => x.CATEGORY)
                .Not.Nullable();

            Map(x => x.EXCEPT_ACTION);
            References(x => x.TO_STATE);
            Map(x => x.OBJECTS_DEF);

            Map(x => x.DO_INHERIT)
                .Not.Nullable();

            Map(x => x.SUBLEVEL);
            Map(x => x.MARKING_COLOR);
            References(x => x.TO_STATE_BY_MAIL_CIRC);
        }
    }
}