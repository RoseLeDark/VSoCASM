using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm.CommandLine
{
    internal class HelpArgument : FlagArgument
    {
        public HelpArgument()  : base(false,false) {
            //Processor.ad
            base.NeedsValue = false;
        }

        protected internal override bool Validate(object value)  {
            return true;
        }
        public override string GetHelpText()
        {
            return "Show this help text";
        }
    }
}
