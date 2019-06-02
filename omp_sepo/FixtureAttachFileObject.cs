using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace omp_sepo
{
    public enum FixtureAttachFileState { Success, Error, None }

    public class FixtureAttachFileObject
    {
        public int IdDoc { get; set; }

        public string Sign { get; set; }

        public string ObjectType { get; set; }

        public int ObjectRevision { get; set; }

        public FixtureAttachFileState State { get; set; }
    }
}