using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoFixtureAfLoadMap : ClassMap<V_SEPO_FIXTURE_AF_LOAD>
    {
        public VSepoFixtureAfLoadMap()
        {
            Id(m => m.ID).GeneratedBy.Assigned();

            Map(m => m.DOC_ID);
            Map(m => m.DESIGNATIO);
            Map(m => m.NAME);
            Map(m => m.FILENAME);
            Map(m => m.BOCODE);
            Map(m => m.BOTYPE);
            Map(m => m.DOCCODE);
            Map(m => m.REVISION);
            Map(m => m.ID_FILE_GROUP);
            Map(m => m.ID_OWNER);
        }
    }
}