using FluentNHibernate.Mapping;
using obj_lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Mappings
{
    public class TechnologyOperationsMap : ClassMap<TECHNOLOGY_OPERATIONS>
    {
        public TechnologyOperationsMap()
        {
            Id(m => m.CODE).GeneratedBy.Assigned();

            Map(m => m.OPERCODE);
            Map(m => m.VARIANTCODE);
            Map(m => m.NAME);
            Map(m => m.DESCRIPTION);
            Map(m => m.OPERTYPE);
            Map(m => m.PROCTYPE);
            Map(m => m.SUMUPSTEPSTIME);
            Map(m => m.USEMAINMATNORMS);
            Map(m => m.CHECKWORKTIME);
            Map(m => m.CHECKUNITTIME);
            Map(m => m.CHECKBATCHTIME);
            Map(m => m.OPERCODECODE);
            Map(m => m.CHECK_WS_VER);
            Map(m => m.CHECK_EQ_MODEL_VER);
        }
    }
}