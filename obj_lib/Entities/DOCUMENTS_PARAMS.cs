using System;

namespace obj_lib.Entities
{
    public class DOCUMENTS_PARAMS
    {
        public virtual int CODE { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual string FILENAME { get; set; }

        public virtual int? FORMAT { get; set; }

        public virtual string DESCRIPTION { get; set; }

        public virtual int? CHECKOUT { get; set; }

        public virtual string CHECKOUT_FOLDER { get; set; }

        public virtual DateTime MODDATE { get; set; }

        public virtual DateTime RDATE { get; set; }

        public virtual DateTime F_CREDATE { get; set; }

        public virtual DateTime F_MODDATE { get; set; }

        public virtual string HASH { get; set; }

        public virtual int? HASH_ALG { get; set; }

        public virtual int? MAINCODE { get; set; }

        public virtual int VERNUM { get; set; }

        public virtual DateTime VERDATE { get; set; }

        public virtual int USERCODE { get; set; }

        public virtual int? TEXT_SEARCH { get; set; }

        public virtual int? HISTORY_VERSIONS_COUNT { get; set; }
    }
}