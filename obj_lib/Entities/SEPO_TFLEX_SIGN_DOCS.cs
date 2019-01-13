using System.Collections.Generic;

namespace obj_lib.Entities
{
    public class SEPO_TFLEX_SIGN_DOCS
    {
        public virtual int ID { get; protected set; }

        public virtual string SIGN { get; set; }

        public virtual IList<SEPO_TFLEX_OBJ_SYNCH> OBJ_SYNCH_LIST { get; set; }

        public SEPO_TFLEX_SIGN_DOCS()
        {
            OBJ_SYNCH_LIST = new List<SEPO_TFLEX_OBJ_SYNCH>();
        }
    }
}