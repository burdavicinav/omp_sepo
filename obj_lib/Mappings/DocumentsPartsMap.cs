using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class DocumentsPartsMap : ClassMap<DOCUMENTS_PARTS>
    {
        public DocumentsPartsMap()
        {
            Id(m => m.CODE).GeneratedBy.Assigned();

            Map(m => m.NUM).Not.Nullable();
            Map(m => m.COMPRESSED);
            //Map(m => m.DATA).Not.LazyLoad();
        }
    }
}