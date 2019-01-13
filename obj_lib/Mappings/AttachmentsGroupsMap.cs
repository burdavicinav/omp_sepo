using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class AttachmentsGroupsMap : ClassMap<ATTACHMENTS_GROUPS>
    {
        public AttachmentsGroupsMap()
        {
            Schema("OMP_ADM");

            Id(x => x.CODE).GeneratedBy.Sequence("sq_attachments_groups_code");

            Map(x => x.BOTYPE)
                .Not.Nullable();

            Map(x => x.NAME)
                .Not.Nullable()
                .Length(40);

            Map(x => x.SHORTNAME)
                .Not.Nullable()
                .Length(20);

            Map(x => x.DEF_EXT)
                .Length(255);

            Map(x => x.RIGHTS_SCHEME)
                .Not.Nullable();

            Map(x => x.NOTE)
                .Length(40);
        }
    }
}