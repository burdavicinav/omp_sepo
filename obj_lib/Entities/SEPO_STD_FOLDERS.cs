namespace obj_lib.Entities
{
    public class SEPO_STD_FOLDERS
    {
        public virtual int ID { get; protected set; }

        public virtual int? F_KEY { get; set; }

        public virtual int? F_OWNER { get; set; }

        public virtual int? F_LEVEL { get; set; }

        public virtual string F_NAME { get; set; }
    }
}