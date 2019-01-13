namespace obj_lib.Entities
{
    public class SPC_SECTIONS
    {
        public virtual int CODE { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual int? DEFTYPE { get; set; }

        public virtual int? NUM { get; set; }

        public virtual int ORDERNUM { get; set; }

        public virtual int UNDERLINE { get; set; }

        public virtual int ROWS_BEFORE { get; set; }

        public virtual int ROWS_AFTER { get; set; }

        public SPC_SECTIONS()
        {
            UNDERLINE = 1;

            ROWS_BEFORE = 1;

            ROWS_AFTER = 1;
        }
    }
}