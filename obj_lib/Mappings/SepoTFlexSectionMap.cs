using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTFlexSectionMap : ClassMap<SEPO_TFLEX_SPEC_SECTIONS>
    {
        public SepoTFlexSectionMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID).GeneratedBy.TriggerIdentity();

            Map(x => x.SECTION_).Length(100).Unique();

            HasMany(x => x.OBJ_SYNCH_LIST).Inverse();
        }
    }
}