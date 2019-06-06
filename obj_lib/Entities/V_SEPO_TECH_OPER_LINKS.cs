using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Entities
{
    public class V_SEPO_TECH_OPER_LINKS
    {
        public virtual int ID { get; protected set; }

        public virtual string OPERCODE { get; set; }

        public virtual string OPERNAME { get; set; }

        public virtual int? OMP_ID { get; set; }

        public virtual string OMP_OPERNAME { get; set; }

        public virtual string OMP_OPERCODE { get; set; }

        public virtual string OMP_VARIANTCODE { get; set; }
    }
}