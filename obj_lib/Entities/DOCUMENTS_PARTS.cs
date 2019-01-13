using NHibernate;

namespace obj_lib.Entities
{
    public class DOCUMENTS_PARTS
    {
        public virtual int CODE { get; protected set; }

        public virtual int NUM { get; set; }

        public virtual int COMPRESSED { get; set; }

        //public virtual byte[] DATA { get; set; }

        public virtual byte[] DATA
        {
            get
            {
                ISQLQuery query = Module.OpenSession().CreateSQLQuery(
                    "select data from documents_parts where code = :code");

                query.SetParameter("code", CODE);

                byte[] data = (byte[])query.UniqueResult();

                return data;
            }
        }

        public DOCUMENTS_PARTS()
        {
            COMPRESSED = 0;
        }
    }
}