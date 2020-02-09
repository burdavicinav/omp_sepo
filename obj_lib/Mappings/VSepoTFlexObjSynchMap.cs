using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class VSepoTFlexObjSynchMap : ClassMap<V_SEPO_TFLEX_OBJ_SYNCH>
    {
        public VSepoTFlexObjSynchMap()
        {
            Schema("OMP_ADM");

            ReadOnly();

            Id(x => x.ID);

            Map(m => m.ID_SECTION);
            Map(m => m.TFLEX_SECTION);
            Map(m => m.ID_DOCSIGN);
            Map(m => m.TFLEX_DOCSIGN);
            Map(m => m.KOTYPE);
            Map(m => m.BOTYPE);
            Map(m => m.BOTYPENAME);
            Map(m => m.BOTYPESHORTNAME);
            Map(m => m.BOSTATECODE);
            Map(m => m.BOSTATENAME);
            Map(m => m.BOSTATESHORTNAME);
            Map(m => m.FILEGROUP);
            Map(m => m.FILEGROUPNAME);
            Map(m => m.FILEGROUPSHORTNAME);
            Map(m => m.OWNER);
            Map(m => m.OWNERNAME);
            Map(m => m.OMPSECTION);
            Map(m => m.OMPSECTIONNAME);
            Map(m => m.PARAM_DEPENDENCE);
            Map(m => m.ID_PARAM);
            Map(m => m.PARAM);
            Map(m => m.PARAM_EXPRESSION);
            Map(m => m.ID_SECTYPE);
            Map(m => m.SECTYPE_SIGN);
            Map(m => m.SECTYPE_NAME);
        }
    }
}