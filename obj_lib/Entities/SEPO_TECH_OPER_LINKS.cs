namespace obj_lib.Entities
{
    public class SEPO_TECH_OPER_LINKS
    {
        public virtual int ID { get; protected set; }

        public virtual string OPERCODE { get; set; }

        public virtual string OPERNAME { get; set; }

        public virtual int? OMPID { get; set; }
    }
}