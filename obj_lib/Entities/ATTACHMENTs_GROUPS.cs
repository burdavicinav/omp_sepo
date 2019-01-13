namespace obj_lib.Entities
{
    public class ATTACHMENTS_GROUPS
    {
        public virtual int CODE { get; protected set; }

        public virtual int BOTYPE { get; set; }

        public virtual string NAME { get; set; }

        public virtual string SHORTNAME { get; set; }

        public virtual string DEF_EXT { get; set; }

        public virtual int RIGHTS_SCHEME { get; set; }

        public virtual string NOTE { get; set; }

        public ATTACHMENTS_GROUPS()
        {
            RIGHTS_SCHEME = 0;
        }
    }
}