using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Entities
{
    public class TECHNOLOGY_OPERATIONS
    {
        public virtual int CODE { get; protected set; }

        public virtual string OPERCODE { get; set; }

        public virtual string VARIANTCODE { get; set; }

        public virtual string NAME { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual int? OPERTYPE { get; set; }

        public virtual int? PROCTYPE { get; set; }

        public virtual int SUMUPSTEPSTIME { get; set; }

        public virtual int USEMAINMATNORMS { get; set; }

        public virtual int CHECKWORKTIME { get; set; }

        public virtual int CHECKUNITTIME { get; set; }

        public virtual int CHECKBATCHTIME { get; set; }

        public virtual int OPERCODECODE { get; set; }

        public virtual int CHECK_WS_VER { get; set; }

        public virtual int CHECK_EQ_MODEL_VER { get; set; }

        public TECHNOLOGY_OPERATIONS()
        {
            SUMUPSTEPSTIME = 0;
            USEMAINMATNORMS = 0;
            CHECKWORKTIME = 1;
            CHECKUNITTIME = 1;
            CHECKBATCHTIME = 1;
            CHECK_WS_VER = 0;
            CHECK_EQ_MODEL_VER = 0;
        }
    }
}