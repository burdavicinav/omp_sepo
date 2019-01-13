using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTaskFolderListMap : ClassMap<SEPO_TASK_FOLDER_LIST>
    {
        public SepoTaskFolderListMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.NAME).Unique().Not.Nullable();
            References(r => r.ID_PARENT, "ID_PARENT");
        }
    }
}