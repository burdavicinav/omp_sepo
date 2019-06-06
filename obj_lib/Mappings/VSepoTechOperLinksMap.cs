using FluentNHibernate.Mapping;
using obj_lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Mappings
{
    public class VSepoTechOperLinksMap : ClassMap<V_SEPO_TECH_OPER_LINKS>
    {
        public VSepoTechOperLinksMap()
        {
            Id(m => m.ID).GeneratedBy.Assigned();

            Map(m => m.OPERCODE);
            Map(m => m.OPERNAME);
            Map(m => m.OMP_ID);
            Map(m => m.OMP_OPERNAME);
            Map(m => m.OMP_OPERCODE);
            Map(m => m.OMP_VARIANTCODE);
        }
    }
}