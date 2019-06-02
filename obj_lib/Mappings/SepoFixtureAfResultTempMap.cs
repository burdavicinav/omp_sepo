using FluentNHibernate.Mapping;
using obj_lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Mappings
{
    public class SepoFixtureAfResultTempMap : ClassMap<SEPO_FIXTURE_AF_RESULT_TEMP>
    {
        public SepoFixtureAfResultTempMap()
        {
            Id(m => m.ID).GeneratedBy.Assigned();

            Map(m => m.IDDOC);
            Map(m => m.FILENAME);
            Map(m => m.OBJSIGN);
            Map(m => m.STATE);
        }
    }
}