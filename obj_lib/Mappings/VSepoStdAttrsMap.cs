using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoStdAttrsMap : ClassMap<V_SEPO_STD_ATTRS>
    {
        public VSepoStdAttrsMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.ID_ATTR).GeneratedBy.Assigned();
            Map(x => x.ID_TABLE);
            Map(x => x.TNAME);
            Map(x => x.FIELD);
            Map(x => x.F_DATATYPE);
            Map(x => x.F_ENTERMODE);
            Map(x => x.F_DATA);
            Map(x => x.ATTR_NAME);
            Map(x => x.OMP_NAME);
            Map(x => x.OMP_TYPE);
            Map(x => x.ID_ENUM);
            Map(x => x.ENUM_NAME);
            Map(x => x.ID_OMP_ENUM);
            Map(x => x.OMP_ENUM_NAME);
            Map(x => x.ID_RECORD);
        }
    }
}