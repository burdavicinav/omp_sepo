using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class DocumentsParamsMap : ClassMap<DOCUMENTS_PARAMS>
    {
        public DocumentsParamsMap()
        {
            Id(m => m.CODE).GeneratedBy.Assigned();

            Map(m => m.NAME).Length(150).Not.Nullable();
            Map(m => m.FILENAME).Length(255);
            Map(m => m.FORMAT);
            Map(m => m.DESCRIPTION).Length(100);
            Map(m => m.CHECKOUT);
            Map(m => m.CHECKOUT_FOLDER).Length(255);
            Map(m => m.MODDATE).Not.Nullable();
            Map(m => m.RDATE).Not.Nullable();
            Map(m => m.F_CREDATE).Not.Nullable();
            Map(m => m.F_MODDATE).Not.Nullable();
            Map(m => m.HASH).Length(40);
            Map(m => m.HASH_ALG);
            Map(m => m.MAINCODE);
            Map(m => m.VERNUM).Not.Nullable();
            Map(m => m.VERDATE).Not.Nullable();
            Map(m => m.USERCODE).Not.Nullable();
            Map(m => m.TEXT_SEARCH);
            Map(m => m.HISTORY_VERSIONS_COUNT);
        }
    }
}