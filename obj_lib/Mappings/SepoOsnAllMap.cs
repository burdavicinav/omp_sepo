using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoOsnAllMap : ClassMap<SEPO_OSN_ALL>
    {
        public SepoOsnAllMap()
        {
            Id(x => x.ID)
                .GeneratedBy
                .Sequence("sq_sepo_osn_all");

            Map(x => x.ART_CLASS);
            Map(x => x.ART_ID);
            Map(x => x.ART_VER_ID);
            Map(x => x.AUTHOR);
            Map(x => x.BASEART_ID);
            Map(x => x.CHKINDATE);
            Map(x => x.DESIGNATION);
            Map(x => x.DOC_ID);
            Map(x => x.EXPANDING);
            Map(x => x.FILE_ISLOAD);
            Map(x => x.IMBASE_KEY);
            Map(x => x.ISP_CODE);
            Map(x => x.LITERA);
            Map(x => x.MASSA);
            Map(x => x.MMR);
            Map(x => x.MODIFDATE);
            Map(x => x.MODIFUSER_ID);
            Map(x => x.MU_ID);
            Map(x => x.NAME);
            Map(x => x.NEED_SVO);
            Map(x => x.NOTE);
            Map(x => x.OKP_CODE);
            Map(x => x.PR_ID);
            Map(x => x.PURCHASED);
            Map(x => x.SECTION_ID);
            Map(x => x.SERIAL_NO);
            Map(x => x.SET_NO);
        }
    }
}