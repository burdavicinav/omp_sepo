namespace obj_lib.Entities
{
    public class V_SEPO_FIXTURE_AF_LOAD
    {
        public virtual int ID { get; protected set; }

        public virtual int DOC_ID { get; set; }

        public virtual string FILENAME { get; set; }

        public virtual string DESIGNATIO { get; set; }

        public virtual string NAME { get; set; }

        public virtual int BOCODE { get; set; }

        public virtual int BOTYPE { get; set; }

        public virtual int DOCCODE { get; set; }

        public virtual int REVISION { get; set; }

        public virtual int? ID_FILE_GROUP { get; set; }

        public virtual int? ID_OWNER { get; set; }
    }
}