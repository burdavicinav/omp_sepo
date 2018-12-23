namespace obj_lib
{
    public class V_SEPO_STD_FOXPRO_ATTRS
    {
        public decimal Id { get; set; }

        public string Name { get; set; }

        public string Shortname { get; set; }

        public short Type_ { get; set; }

        public string TypeName
        {
            get
            {
                switch (Type_)
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