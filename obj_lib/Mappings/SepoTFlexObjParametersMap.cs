using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTFlexObjParametersMap : ClassMap<SEPO_TFLEX_OBJ_PARAMETERS>
    {
        public SepoTFlexObjParametersMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID)
                .GeneratedBy.Assigned();

            Map(x => x.NAME);
        }
    }
}