using FluentNHibernate.Mapping;
using obj_lib.Entities;

namespace obj_lib.Mappings
{
    public class OwnerNameMap : ClassMap<OWNER_NAME>
    {
        public OwnerNameMap()
        {
            Id(m => m.OWNER).GeneratedBy.Assigned();

            Map(m => m.NAME);
            Map(m => m.PARENT);
            Map(m => m.NOTE);
            Map(m => m.PRINTSTATUS);
            Map(m => m.TP_OPER_COST_FORMULA);
            Map(m => m.WAGE_RATE);
            Map(m => m.TP_OPER_PRICE_TYPE);
            Map(m => m.ENTERPRISE);
            Map(m => m.USE_ROUTE);
            Map(m => m.WAGERATEFORMULA);
            Map(m => m.DIVISION_CODE);
        }
    }
}