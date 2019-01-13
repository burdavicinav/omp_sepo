using System;

namespace obj_lib.Entities
{
    public class BUSINESSOBJ_STATES
    {
        public virtual int CODE { get; protected set; }

        public virtual int BOTYPE { get; set; }

        public virtual string NAME { get; set; }

        public virtual string SHORTNAME { get; set; }

        public virtual int RIGHTS_SCHEME { get; set; }

        public virtual int MINSTATE { get; set; }

        public virtual BUSINESSOBJ_STATES ADDITIONAL_TO { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual int PROMLEVEL { get; set; }

        public virtual DateTime RDATE { get; set; }

        public virtual string NOTE { get; set; }

        public virtual int REVCOUNT { get; set; }

        public virtual int REVCOUNT_OTHER { get; set; }

        public virtual int CATEGORY { get; set; }

        public virtual int? EXCEPT_ACTION { get; set; }

        public virtual BUSINESSOBJ_STATES TO_STATE { get; set; }

        public virtual int? OBJECTS_DEF { get; set; }

        public virtual int DO_INHERIT { get; set; }

        public virtual int? SUBLEVEL { get; set; }

        public virtual int? MARKING_COLOR { get; set; }

        public virtual BUSINESSOBJ_STATES TO_STATE_BY_MAIL_CIRC { get; set; }

        public BUSINESSOBJ_STATES()
        {
            RIGHTS_SCHEME = 1;
            REVCOUNT = -1;
            REVCOUNT_OTHER = -1;
            CATEGORY = 0;
            DO_INHERIT = 0;
        }
    }
}