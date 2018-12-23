using System;

namespace imp_exp
{
    public class XmlVerifyException : Exception
    {
        public XmlVerifyException() : base()
        {
        }

        public override string Message
        {
            get
            {
                return "Xml файл имеет неверный формат";
            }
        }
    }
}