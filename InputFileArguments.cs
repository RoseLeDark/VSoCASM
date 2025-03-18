using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vsocasm.CommandLine;

namespace vsocasm
{
    internal class InputFileArguments : Argument<string>
    {
        public InputFileArguments() : base("", true)
        {
            Processor = Func;
        }
        public void Func(object value)
        {
            AsmPrgBase.Instance.InputFile = (string)value;
        }

        public override string GetHelpText()
        {
            return "Specifies the input file to compile. This is a required argument.";
        }
    }
}
