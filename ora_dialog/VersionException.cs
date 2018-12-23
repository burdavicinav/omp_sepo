using System;

namespace ora_dialog
{
    public class VersionException : Exception
    {
        public VersionException() : base()
        {
        }

        public override string Message
        {
            get
            {
                return "Некорректная версия приложения. Обратитесь к администратору!";
            }
        }
    }
}