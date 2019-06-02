using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omp_sepo
{
    public class FixtureAttachFileEndArgs : EventArgs
    {
        public string FileName { get; set; }

        public List<FixtureAttachFileObject> Objects { get; set; }
    }
}