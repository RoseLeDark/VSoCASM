using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vsocasm.CommandLine;

namespace vsocasm
{
    internal class OutputFileArguments : Argument<string>
    {
        public OutputFileArguments() : base("O.bin", false)
        {
            Processor = Func;
            AsmPrgBase.Instance.OutputFile = "O.bin";
        }
        public void Func(object value)
        {
            AsmPrgBase.Instance.OutputFile = (string)value;
        }

        public override string GetHelpText()
        {
            return "Specifies the output file name for the compiled program. [default: '0.bin']";
        }
    }
}
