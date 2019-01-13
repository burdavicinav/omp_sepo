using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class TechoperationTypesMap : ClassMap<TECHOPERATION_TYPES>
    {
        public TechoperationTypesMap()
        {
            Id(x => x.CODE).GeneratedBy.Sequence("sq_techoperation_types");

            Map(m => m.NAME).Not.Nullable();
            Map(m => m.NOTE);
            Map(m => m.ISACTIVE);
        }
    }
}