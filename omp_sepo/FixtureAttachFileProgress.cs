using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omp_sepo
{
    public enum FixtureAttachFileProgressType { BeginAttachFile, EndAttachFile }

    public class FixtureAttachFileProgress
    {
        public FixtureAttachFileProgressType Type { get; set; }

        public string FileName { get; set; }

        public List<FixtureAttachFileObject> Objects { get; set; }
    }
}