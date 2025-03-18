using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vsocasm.CommandLine;

namespace vsocasm
{
    internal class DumpArchArguments : FlagArgument
    {
        public DumpArchArguments() : base(false, false)
        {
            Processor = Func;
            AsmPrgBase.Instance.Dump = false;
        }

        public void Func(object value)
        {
            AsmPrgBase.Instance.Dump = (bool)value;
        }

        public override string GetHelpText()
        {
            return "Dumps the instructions to the output, without actually performing the compilation.";
        }
    }
}
