using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Xml.Linq;
using vsocasm.CommandLine;
using vsocasm.Instructions;

namespace vsocasm
{

    public class Program
    {
        static CommandLineArgs cmdLine;
        static InstructionLoader instructions;
        private static int Main(string[] args)
        {
            cmdLine = new CommandLineArgs();
            cmdLine.ApplicationInfo = "vsocasm is a tool used to compile code for the VSoC Instruction Set Architecture (ISA).\n" +
                "It allows you to specify various arguments to control the compilation process and customize its behavior. ";

            cmdLine.RegisterArgument(["-a", "--arch"], new ArchAsemblerArguments());
            cmdLine.RegisterArgument(["-d", "--dump"], new DumpArchArguments());

            cmdLine.RegisterArgument(["-o", "--output"], new OutputFileArguments());
            cmdLine.RegisterArgument(["-i", "--input"], new InputFileArguments());

            if (!cmdLine.Parse(args)) 
                return 1;

            instructions = new InstructionLoader(AsmPrgBase.Instance.PlatformArch);

            if (AsmPrgBase.Instance.Dump) {
                dumpArch(instructions.Config);
            }



            return 0;
        }

        private static void dumpArch(ISAConfig config)
        {
            // Ausgabe der Core-Größe und anderer Konfigurationen
            Console.WriteLine($"Core Size: {config.CoreSize}");
            Console.WriteLine($"Extend Vector Size: {config.ExtendVectorSize}");
            Console.WriteLine($"Compressed Size: {config.CompressedSize}");

            // Ausgabe der geladenen Instruktionen mit berechneten Größen
            foreach (var instruction in config.Instructions)
            {
                Console.WriteLine(instruction.ToString());
            }

            // Ausgabe der geladenen Register
            foreach (var register in config.Registers)
            {
                Console.WriteLine(register.ToString());
            }
        }
    }
}
