using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Entities
{
    public class SEPO_TP_WORKSHOP_OWNER
    {
        public virtual int ID { get; protected set; }

        public virtual int WSCODE { get; set; }

        public virtual int? OWNER { get; set; }
    }
}