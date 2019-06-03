using FluentNHibernate.Mapping;
using obj_lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Mappings
{
    public class SepoTpWorkshopOwnerMap : ClassMap<SEPO_TP_WORKSHOP_OWNER>
    {
        public SepoTpWorkshopOwnerMap()
        {
            Id(m => m.ID).GeneratedBy.TriggerIdentity();

            Map(m => m.WSCODE);
            Map(m => m.OWNER);
        }
    }
}