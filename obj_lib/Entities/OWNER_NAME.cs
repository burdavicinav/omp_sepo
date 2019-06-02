namespace obj_lib.Entities
{
    public class OWNER_NAME
    {
        public virtual int OWNER { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual int? PARENT { get; set; }

        public virtual string NOTE { get; set; }

        public virtual int PRINTSTATUS { get; set; }

        public virtual int? TP_OPER_COST_FORMULA { get; set; }

        public virtual int? WAGE_RATE { get; set; }

        public virtual int? TP_OPER_PRICE_TYPE { get; set; }

        public virtual int? ENTERPRISE { get; set; }

        public virtual int? USE_ROUTE { get; set; }

        public virtual int? WAGERATEFORMULA { get; set; }

        public virtual int? DIVISION_CODE { get; set; }

        public OWNER_NAME()
        {
            PRINTSTATUS = 1;
        }
    }
}