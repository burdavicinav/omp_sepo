using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTFlexSectionTypesMap : ClassMap<SEPO_TFLEX_SECTION_TYPES>
    {
        public SepoTFlexSectionTypesMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            References(m => m.ID_SECTION, "ID_SECTION").Not.Nullable();
            Map(m => m.SIGN).Not.Nullable();
            Map(m => m.NAME).Not.Nullable();
        }
    }
}