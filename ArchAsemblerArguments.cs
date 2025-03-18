using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vsocasm.CommandLine;

namespace vsocasm
{
    internal class ArchAsemblerArguments : Argument
    {
        public ArchAsemblerArguments() : base("x86", false)
        {
            Processor = Func;
            AsmPrgBase.Instance.PlatformArch = "x86";
        }

        public void Func(object value)
        {
            if(value != null)
                AsmPrgBase.Instance.PlatformArch = (string)value;
        }

        public override string GetHelpText()
        {
            return "Specifies the platform's ISA to use. The sub folder under \"Platform\" name will be used. [default: '32bit']";
        }
    }
}
