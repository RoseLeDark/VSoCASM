using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm.Instructions
{
    public class ISAConfig
    {
        public int CoreSize { get; set; } // Größe des Cores, z.B. 32 oder 64
        public int ExtendVectorSize { get; set; } // Berechneter Vektorbereich (z.B. 128)
        public int CompressedSize { get; set; } // Komprimierte Größe (z.B. 16)

        public List<InstructionConfig> Instructions { get; set; }
        public List<RegisterConfig> Registers { get; set; }
    }
}
