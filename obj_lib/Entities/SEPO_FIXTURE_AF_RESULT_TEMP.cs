using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obj_lib.Entities
{
    public class SEPO_FIXTURE_AF_RESULT_TEMP
    {
        public virtual int ID { get; protected set; }

        public virtual int IDDOC { get; set; }

        public virtual string OBJSIGN { get; set; }

        public virtual string FILENAME { get; set; }

        public virtual short STATE { get; set; }
    }
}