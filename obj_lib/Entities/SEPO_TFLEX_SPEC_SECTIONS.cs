using System.Collections.Generic;

namespace obj_lib.Entities
{
    public class SEPO_TFLEX_SPEC_SECTIONS
    {
        public virtual int ID { get; protected set; }

        public virtual string SECTION_ { get; set; }

        public virtual IList<SEPO_TFLEX_OBJ_SYNCH> OBJ_SYNCH_LIST { get; set; }

        public SEPO_TFLEX_SPEC_SECTIONS()
        {
            OBJ_SYNCH_LIST = new List<SEPO_TFLEX_OBJ_SYNCH>();
        }
    }
}