using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTFlexObjSynchMap : ClassMap<SEPO_TFLEX_OBJ_SYNCH>
    {
        public SepoTFlexObjSynchMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID).GeneratedBy.TriggerIdentity();

            References(x => x.TFLEX_SECTION, "TFLEX_SECTION")
                .Not.Nullable();

            References(x => x.TFLEX_DOCSIGN, "TFLEX_DOCSIGN");

            Map(x => x.OMP_BOTYPE)
                .Not.Nullable();

            Map(x => x.OMP_BOSTATE)
                .Not.Nullable();

            Map(x => x.OMP_FILEGROUP);

            Map(x => x.OMP_OWNER);

            Map(x => x.OMP_SECTION)
                .Not.Nullable();

            Map(x => x.PARAM_DEPENDENCE);
            References(x => x.ID_PARAM, "ID_PARAM");
            Map(x => x.EXPRESSION);
        }
    }
}