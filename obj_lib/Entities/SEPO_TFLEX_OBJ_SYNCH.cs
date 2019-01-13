﻿namespace obj_lib.Entities
{
    public class SEPO_TFLEX_OBJ_SYNCH
    {
        public virtual int ID { get; protected set; }

        public virtual SEPO_TFLEX_SPEC_SECTIONS TFLEX_SECTION { get; set; }

        public virtual SEPO_TFLEX_SIGN_DOCS TFLEX_DOCSIGN { get; set; }

        public virtual int OMP_BOTYPE { get; set; }

        public virtual int OMP_BOSTATE { get; set; }

        public virtual int? OMP_FILEGROUP { get; set; }

        public virtual int? OMP_OWNER { get; set; }

        public virtual int OMP_SECTION { get; set; }
    }
}