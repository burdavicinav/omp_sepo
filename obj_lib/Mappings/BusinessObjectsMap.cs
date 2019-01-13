using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class BusinessObjectsMap : ClassMap<BUSINESS_OBJECTS>
    {
        public BusinessObjectsMap()
        {
            Schema("OMP_ADM");

            Id(x => x.CODE).GeneratedBy.Sequence("sq_business_objects_code");

            Map(x => x.TYPE)
                .Not.Nullable();

            Map(x => x.DOCCODE)
                .Not.Nullable();

            Map(x => x.NAME)
                .Length(200)
                .Not.Nullable();

            Map(x => x.OWNER)
                .Not.Nullable();

            Map(x => x.CHECKOUT);
            Map(x => x.REVISION)
                .Not.Nullable();

            Map(x => x.REVSIGN)
                .Length(30);

            Map(x => x.PRODCODE)
                .Not.Nullable();

            Map(x => x.ACCESS_LEVEL);
            References(x => x.TODAY_STATE);
            Map(x => x.TODAY_STATEDATE);
            Map(x => x.TODAY_PRODBOCODE);
            Map(x => x.TODAY_PRODDOCCODE);
            Map(x => x.TODAY_STATEUSER);
            Map(x => x.CREATE_DATE);
            Map(x => x.CREATE_USER);
            Map(x => x.UPDATE_DATE);
            Map(x => x.UPDATE_USER);
            Map(x => x.ATTACHMENTS_COUNT);
        }
    }
}