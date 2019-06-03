using FluentNHibernate.Mapping;
using obj_lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Mappings
{
    public class VSepoTpWorkshopOwnerMap : ClassMap<V_SEPO_TP_WORKSHOP_OWNER>
    {
        public VSepoTpWorkshopOwnerMap()
        {
            Id(m => m.ID).GeneratedBy.Assigned();

            Map(m => m.WSCODE);
            Map(m => m.WSSIGN);
            Map(m => m.OWNER);
            Map(m => m.OWNERNAME);
        }
    }
}