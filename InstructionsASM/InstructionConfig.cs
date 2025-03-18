using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace vsocasm.Instructions
{
    public class InstructionConfig
    {
        public string OpCode { get; set; }
        public int BitSize { get; }
        public int ParamCount { get; set; }
        public int SizeInBytes { get; set; }

        public string Value { get; set; }
        public int ValueSize { get; }
        public int OpCodeSize { get; }

        public InstructionConfig(XElement instructionElement, int bitSize)
        {

            this.ParamCount = int.Parse(instructionElement.Element("ParamCount").Value);
            this.OpCode = instructionElement.Element("Mnemonic").Value;
            this.BitSize = bitSize;
            this.Value = instructionElement.Element("hex").Value;
            this.ValueSize = int.Parse(instructionElement.Element("ValueSize").Value);
            this.OpCodeSize = int.Parse(instructionElement.Element("OpCodeSize").Value);

            var autoCalcSize = bool.Parse(instructionElement.Element("AutoCalcSize").Value);

            if (!autoCalcSize)
            {
                this.SizeInBytes = int.Parse(instructionElement.Element("SizeInBytes")?.Value ?? "0");
            }
            else
            {
                this.SizeInBytes = OpCodeSize + ((ValueSize + 1) * this.ParamCount);
            }
        }
        public override string ToString()
        {
            return $"{OpCode}:{Value}\tPC:{ParamCount}\tVS:{ValueSize}\tOS:{OpCodeSize}:{SizeInBytes}";
        }
    }
}
