using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoFixtureAfObjectsMap : ClassMap<SEPO_FIXTURE_AF_OBJECTS>
    {
        public SepoFixtureAfObjectsMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.ID_TYPE);
            Map(m => m.ID_FILE_GROUP);
            Map(m => m.ID_OWNER);
        }
    }
}