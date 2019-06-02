using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoFixtureAfObjectsMap : ClassMap<V_SEPO_FIXTURE_AF_OBJECTS>
    {
        public VSepoFixtureAfObjectsMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.ID_TYPE);
            Map(m => m.TYPENAME);
            Map(m => m.FILEGROUP);
            Map(m => m.OWNER);
        }
    }
}