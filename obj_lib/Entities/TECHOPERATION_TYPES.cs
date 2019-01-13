namespace obj_lib.Entities
{
    public class TECHOPERATION_TYPES
    {
        public virtual int CODE { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual string NOTE { get; set; }

        public virtual int ISACTIVE { get; set; }

        public TECHOPERATION_TYPES()
        {
            ISACTIVE = 1;
        }
    }
}