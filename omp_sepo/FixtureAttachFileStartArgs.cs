using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omp_sepo
{
    public class FixtureAttachFileStartArgs : EventArgs
    {
        public string FileName { get; set; }

        public FixtureAttachFileStartArgs(string file)
        {
            FileName = file;
        }
    }
}