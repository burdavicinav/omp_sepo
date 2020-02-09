using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Entities
{
    public class SEPO_TFLEX_SECTION_TYPES
    {
        public virtual int ID { get; set; }

        public virtual SEPO_TFLEX_SPEC_SECTIONS ID_SECTION { get; set; }

        public virtual int SIGN { get; set; }

        public virtual string NAME { get; set; }
    }
}