using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoFixtureDocsMap : ClassMap<V_SEPO_FIXTURE_DOCS>
    {
        public VSepoFixtureDocsMap()
        {
            Id(x => x.BOCODE)
                .GeneratedBy
                .Assigned();

            Map(x => x.ART_ID);
            Map(x => x.DOC_ID);
            Map(x => x.FILENAME);
            Map(x => x.FILE_ISLOAD);
            Map(x => x.NAME);
            Map(x => x.REVISION);
            Map(x => x.STATE);
            Map(x => x.TODAY_STATE);
        }
    }
}