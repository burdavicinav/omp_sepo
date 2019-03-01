namespace obj_lib.Entities
{
    public class SEPO_OSN_ALL
    {
        public virtual long ID { get; protected set; }

        public virtual decimal? ART_ID { get; set; }

        public virtual decimal? DOC_ID { get; set; }

        public virtual decimal? ISP_CODE { get; set; }

        public virtual string DESIGNATION { get; set; }

        public virtual string NAME { get; set; }

        public virtual decimal? OKP_CODE { get; set; }

        public virtual string IMBASE_KEY { get; set; }

        public virtual string PURCHASED { get; set; }

        public virtual decimal? MASSA { get; set; }

        public virtual decimal? MU_ID { get; set; }

        public virtual decimal? SECTION_ID { get; set; }

        public virtual string NOTE { get; set; }

        public virtual string EXPANDING { get; set; }

        public virtual string LITERA { get; set; }

        public virtual string MMR { get; set; }

        public virtual decimal? ART_VER_ID { get; set; }

        public virtual string AUTHOR { get; set; }

        public virtual string CHKINDATE { get; set; }

        public virtual decimal? PR_ID { get; set; }

        public virtual string NEED_SVO { get; set; }

        public virtual string MODIFDATE { get; set; }

        public virtual decimal? MODIFUSER_ID { get; set; }

        public virtual decimal? BASEART_ID { get; set; }

        public virtual decimal? ART_CLASS { get; set; }

        public virtual string SERIAL_NO { get; set; }

        public virtual string SET_NO { get; set; }

        public virtual decimal FILE_ISLOAD { get; set; }

        public SEPO_OSN_ALL()
        {
            FILE_ISLOAD = 1;
        }
    }
}