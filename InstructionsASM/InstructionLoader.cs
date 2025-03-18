using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace vsocasm.Instructions
{
    public class InstructionLoader
    {
        static readonly string PlatformPath = "Platform\\";

        public string InstructionsPath { get; private set; }

        public ISAConfig Config { get; private set; }
        public InstructionLoader(string archName)
        {
            InstructionsPath = PlatformPath + "\\" + archName + "\\instructions.xml";
            LoadConfig(InstructionsPath);
        }
        private void LoadConfig(string filePath)
        {
            XElement doc = XElement.Load(filePath);

            Config = new ISAConfig
            {
                CoreSize = int.Parse(doc.Element("CoreSize").Value), // Laden der Core-Größe
                ExtendVectorSize = int.Parse(doc.Element("ExtendVectorSize").Value), // Laden der ExtendVectorSize
                CompressedSize = int.Parse(doc.Element("CompressedSize").Value), // Laden der CompressedSize
                Instructions = new List<InstructionConfig>(),
                Registers = new List<RegisterConfig>()
            };

            // Berechnung der SizeInBytes für jede Instruktion
            foreach (var instructionElement in doc.Element("Instructions").Elements("Opcode"))
            {

                //var instruction = new InstructionConfig(config.CoreSize, pCount, strOpcode);
                var instruction = new InstructionConfig(instructionElement, Config.CoreSize);


                Config.Instructions.Add(instruction);
            }

            // Laden der Register
            foreach (var registerElement in doc.Element("Registers").Elements("Register"))
            {
                var register = new RegisterConfig
                {
                    Name = registerElement.Element("Name").Value,
                    Type = registerElement.Element("Type").Value
                };
                Config.Registers.Add(register);
            }
        }
    }
}
