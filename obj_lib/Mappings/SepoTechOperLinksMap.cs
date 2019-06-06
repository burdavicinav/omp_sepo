using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTechOperLinksMap : ClassMap<SEPO_TECH_OPER_LINKS>
    {
        public SepoTechOperLinksMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.OPERCODE);
            Map(m => m.OPERNAME);
            Map(m => m.OMPID);
        }
    }
}