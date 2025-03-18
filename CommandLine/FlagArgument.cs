using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm.CommandLine
{
    public class FlagArgument : Argument
    {
        public FlagArgument(bool defaultValue = false, bool required = false)
            : base(defaultValue, required)  {
            NeedsValue = false;
        }

        protected internal override bool Validate(object value) {
            // Nur true/false-Werte sind erlaubt
            return true;
        }
        public override string GetHelpText() { 
            return "This argument enables or disables a value."; 
        }
    }
}
