using System;

namespace obj_lib.Entities
{
    public class BUSINESS_OBJECTS
    {
        public virtual int CODE { get; protected set; }

        public virtual int TYPE { get; set; }

        public virtual int DOCCODE { get; set; }

        public virtual string NAME { get; set; }

        public virtual int OWNER { get; set; }

        public virtual int? CHECKOUT { get; set; }

        public virtual int REVISION { get; set; }

        public virtual string REVSIGN { get; set; }

        public virtual int PRODCODE { get; set; }

        public virtual int? ACCESS_LEVEL { get; set; }

        public virtual BUSINESSOBJ_STATES TODAY_STATE { get; set; }

        public virtual DateTime? TODAY_STATEDATE { get; set; }

        public virtual int? TODAY_PRODBOCODE { get; set; }

        public virtual int? TODAY_PRODDOCCODE { get; set; }

        public virtual int? TODAY_STATEUSER { get; set; }

        public virtual DateTime? CREATE_DATE { get; set; }

        public virtual int? CREATE_USER { get; set; }

        public virtual DateTime? UPDATE_DATE { get; set; }

        public virtual int? UPDATE_USER { get; set; }

        public virtual int? ATTACHMENTS_COUNT { get; set; }
    }
}