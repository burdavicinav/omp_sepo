using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTFlexSignDocsMap : ClassMap<SEPO_TFLEX_SIGN_DOCS>
    {
        public SepoTFlexSignDocsMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID).GeneratedBy.TriggerIdentity();

            Map(x => x.SIGN).Length(100).Unique();

            HasMany(x => x.OBJ_SYNCH_LIST).Inverse();
        }
    }
}