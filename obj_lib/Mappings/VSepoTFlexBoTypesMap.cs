using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoTFlexBoTypesMap : ClassMap<V_SEPO_TFLEX_BO_TYPES>
    {
        public VSepoTFlexBoTypesMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.BOTYPE);

            Map(m => m.BOTYPENAME);
            Map(m => m.BOTYPESHORTNAME);
        }
    }
}