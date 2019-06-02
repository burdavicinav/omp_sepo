namespace obj_lib.Entities
{
    public class ATTACHMENTS
    {
        public virtual int CODE { get; protected set; }

        public virtual BUSINESS_OBJECTS BUSINESSOBJ { get; set; }

        public virtual int DOCUMENT { get; set; }

        public virtual ATTACHMENTS_GROUPS GROUPCODE { get; set; }

        public virtual int? ADDITIONAL_TO { get; set; }

        public virtual int? HINT { get; set; }

        public virtual int AS_LINK { get; set; }

        public ATTACHMENTS()
        {
            AS_LINK = 0;
        }
    }
}