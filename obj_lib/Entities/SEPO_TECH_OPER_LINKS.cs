namespace obj_lib.Entities
{
    public class SEPO_TECH_OPER_LINKS
    {
        public virtual int ID { get; protected set; }

        public virtual string OPERCODE { get; set; }

        public virtual string VARIANTCODE { get; set; }

        public virtual string NAME { get; set; }

        public virtual int? RECKEY { get; set; }

        public virtual int? TPOPERCODE { get; set; }

        public virtual string TPOPERNAME { get; set; }
    }
}