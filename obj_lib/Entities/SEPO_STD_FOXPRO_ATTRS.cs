namespace obj_lib.Entities
{
    public class SEPO_STD_FOXPRO_ATTRS
    {
        public virtual int ID { get; protected set; }

        public virtual string NAME { get; set; }

        public virtual string SHORTNAME { get; set; }

        public virtual short TYPE_ { get; set; }

        public virtual string TYPENAME
        {
            get
            {
                switch (TYPE_)
                {
                    case 1:
                        return "Строка";

                    case 2:
                        return "Число";

                    case 3:
                        return "Целое число";

                    case 4:
                        return "Дата";

                    default:
                        return null;
                }
            }
        }
    }
}