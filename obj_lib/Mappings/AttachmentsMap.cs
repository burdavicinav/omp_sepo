using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class AttachmentsMap : ClassMap<ATTACHMENTS>
    {
        public AttachmentsMap()
        {
            Id(m => m.CODE).GeneratedBy.Assigned();

            References(m => m.BUSINESSOBJ)
                .Column("businessobj");

            Map(m => m.DOCUMENT);
            References(m => m.GROUPCODE)
                .Column("groupcode");

            Map(m => m.ADDITIONAL_TO);
            Map(m => m.HINT);
            Map(m => m.AS_LINK);
        }
    }
}