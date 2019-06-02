using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTechOperLinksMap : ClassMap<SEPO_TECH_OPER_LINKS>
    {
        public SepoTechOperLinksMap()
        {
            Id(m => m.ID).GeneratedBy.Assigned();

            Map(m => m.OPERCODE);
            Map(m => m.VARIANTCODE);
            Map(m => m.NAME);
            Map(m => m.RECKEY);
            Map(m => m.TPOPERCODE);
            Map(m => m.TPOPERNAME);
        }
    }
}