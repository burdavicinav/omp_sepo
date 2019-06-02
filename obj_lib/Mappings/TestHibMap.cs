using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class TestHibMap : ClassMap<TEST_HIB>
    {
        public TestHibMap()
        {
            Id(m => m.CODE).GeneratedBy.Assigned();

            Map(m => m.STR);
        }
    }
}