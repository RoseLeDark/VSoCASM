using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm
{
    class AsmPrgBase
    {
        public string PlatformArch { get; set; }

        private static AsmPrgBase _instance;
        private AsmPrgBase()  { }

        public static AsmPrgBase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AsmPrgBase();
                return _instance;
            }
        }

        public bool Dump { get; internal set; }
        public string InputFile { get; internal set; }
        public string OutputFile { get; internal set; }
    }
}
