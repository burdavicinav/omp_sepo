namespace obj_lib.Entities
{
    public class V_SEPO_TFLEX_OBJ_SYNCH
    {
        public virtual int ID { get; }

        public virtual int ID_SECTION { get; }

        public virtual string TFLEX_SECTION { get; }

        public virtual int? ID_DOCSIGN { get; }

        public virtual string TFLEX_DOCSIGN { get; }

        public virtual int KOTYPE { get; }

        public virtual int BOTYPE { get; }

        public virtual string BOTYPENAME { get; }

        public virtual string BOTYPESHORTNAME { get; }

        public virtual int BOSTATECODE { get; }

        public virtual string BOSTATENAME { get; }

        public virtual string BOSTATESHORTNAME { get; }

        public virtual int? FILEGROUP { get; }

        public virtual string FILEGROUPNAME { get; }

        public virtual string FILEGROUPSHORTNAME { get; }

        public virtual int? OWNER { get; }

        public virtual string OWNERNAME { get; }

        public virtual int OMPSECTION { get; }

        public virtual string OMPSECTIONNAME { get; }

        public virtual int PARAM_DEPENDENCE { get; }

        public virtual int ID_PARAM { get; }

        public virtual string PARAM { get; }

        public virtual string PARAM_EXPRESSION { get; }

        public virtual int ID_SECTYPE { get; }

        public virtual short SECTYPE_SIGN { get; }

        public virtual string SECTYPE_NAME { get; }
    }
}