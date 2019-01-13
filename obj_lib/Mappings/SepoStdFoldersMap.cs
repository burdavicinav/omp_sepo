using FluentNHibernate.Mapping;

using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoStdFoldersMap : ClassMap<SEPO_STD_FOLDERS>
    {
        public SepoStdFoldersMap()
        {
            Schema("OMP_ADM");

            Id(x => x.ID).GeneratedBy.TriggerIdentity();

            Map(x => x.F_KEY);
            Map(x => x.F_LEVEL);
            Map(x => x.F_OWNER);
            Map(x => x.F_NAME).Length(100);
        }
    }
}