using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class SepoTaskListMap : ClassMap<SEPO_TASK_LIST>
    {
        public SepoTaskListMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.NAME).Not.Nullable();
            References(m => m.ID_FOLDER, "ID_FOLDER").Not.Nullable();
        }
    }
}