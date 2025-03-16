
//
//  Instruction.cs
//
//  Author:
//       anna-sophia <${AuthorEmail}>
//
//  Copyright (c) 2015 anna-sophia
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace vsocasm
{
    /// <summary>
    /// Typ arten eines Parameter
    /// </summary>
	public enum InstructionParam : int
    {
        /// <summary>
        /// Nachfolgene Bits ist ein Adresse zu einen Register
        /// </summary>
		Register = 0,
        /// <summary>
        /// Nachfolgene Bits ist eine Value (#)
        /// </summary>
		Value = 1,
        /// <summary>
        /// Nachfolgene Bits ist ein zeiger (@)
        /// </summary>
		Pointer = 2,
        /// <summary>
        /// Nachfolgene Bits ist ein Label (.)
        /// </summary>
		Lable = 3,
        /// <summary>
        /// Nachfolgene Bits ist ein Variable verweis  (%)
        /// </summary>
        Variable = 4,
        No = 9
    }
    /// <summary>
    /// Basis Klasse jeder Instruction
    /// </summary>
	public class Instruction
    {
        /// <summary>
        /// Name / String des Codes
        /// </summary>
		public string OpCode;
        /// <summary>
        /// param1 = instruction Lenght in bytes; Param2 Parameter Anzahl
        /// </summary>
		public int OpParam1, OpParam2; //Param1 = IP Lenght int byte; Param2 = Parameter Count

        public int size;
        /// <summary>
        /// Konstruter zum erstellen eines OpCode
        /// </summary>
        /// <param name="OpCode">Name des OpCodes</param>
        /// <param name="LenghtInBytes">Lenght in bytes </param>
        /// <param name="ParaneterCount">Parameter Count</param>
        public Instruction(string OpCode, int LenghtInBytes, int ParaneterCount, int size)
        {
            this.OpCode = OpCode;
            this.OpParam1 = LenghtInBytes;
            this.OpParam2 = ParaneterCount;
            this.size = size;
        }

        public Instruction(string OpCode, int ParaneterCount, int size)
        {
            this.OpCode = OpCode;
            this.OpParam2 = ParaneterCount;
            this.size = size;
            this.OpParam1 = 4 + (((size / 8) + 1) * ParaneterCount); // OPCODE(4) + (OpCode(32/8+1) * Count)
        }
        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns>instruction als string</returns>
		public override string ToString()
        {
            return string.Format("{0}\tByte:{1}\tCount:{2}\tBit:{3}", this.OpCode, this.OpParam1, this.OpParam2, this.size);
        }
    }
    /// <summary>
    /// Liste aller vorhandenen Instructionen
    /// </summary>
    public class Instructions : System.Collections.Generic.List<Instruction>
    {
        public const int VariableCode = -4;
        public static readonly Instruction VariableIns = new Instruction("DIM", 8, 9, 32); //OP(4) V(4) {data(N)}

        private int m_lenghtFull;
        private int m_langhtHalf;

        public Instructions(int lenght)
        {
            m_lenghtFull = lenght;
            m_langhtHalf = lenght / 2;

            Add(new Instruction("NOP",      4, 0, m_lenghtFull)); // OP(4)
            Add(new Instruction("STI",      4, 0, m_lenghtFull)); // OP(4)
            Add(new Instruction("CLI",      4, 0, m_lenghtFull)); // OP(4)
            Add(new Instruction("PUSH",     1, m_lenghtFull)); // OP (4) R@#(2) 3(4)
            Add(new Instruction("POP",      1, m_lenghtFull)); // OP (4) R@#(2) 3(4)
            Add(new Instruction("PUSHF",    0, m_lenghtFull)); // OP (4)  Push Flags
            Add(new Instruction("POPF",     1, m_lenghtFull)); // OP (4) R@#(2) 3(4) Pop Flags
            Add(new Instruction("SHL",      1, m_lenghtFull)); // OP (4) R@#(2) 3(4) Shift Left
            Add(new Instruction("SHR",      1, m_lenghtFull)); // OP (4) R@#(2) 3(4) Shift Right
            Add(new Instruction("PEEK",     1, m_lenghtFull)); // OP (4) R@#(2) 3(4)
            Add(new Instruction("HLT",      0, m_lenghtFull));
            Add(new Instruction("JMP",      0, 1, m_lenghtFull));
            Add(new Instruction("LCK",      9, 1, m_lenghtFull)); // Lock  OP(4) T(1)V(4)  LCK Variable
            Add(new Instruction("UCK",      9, 1, m_lenghtFull)); // ULock  OP(4) T(1)V(4)  UCK Variable
            Add(new Instruction("ADD",      3, m_lenghtFull)); // ADD return to V1 not to AX OP(2) T(2)V(4)  T(2)V(4) T(2)V(4)
            Add(new Instruction("SUB",      3, m_lenghtFull)); // SUB return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add(new Instruction("MUL",      3, m_lenghtFull)); // MUL return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add(new Instruction("DIV",      3, m_lenghtFull)); // DIV return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add(new Instruction("EXP",      3, m_lenghtFull));
            Add(new Instruction("SQR", 3, m_lenghtFull));

            // MEM
            Add(new Instruction("XMEM", 3, m_lenghtFull)); // Set Memore OP(2) X=T(2)V(4) Y= T(2)V(4) Mem= T(2)V(4)
            Add(new Instruction("GMEM", 3, m_lenghtFull)); // Get Mem Pos X Y from Addrs on RF

            Add(new Instruction("MOV",      3, m_lenghtFull)); // MOV(4) T(1)V(4) T(1)V(4) T(1)V(4)
            Add(new Instruction("CLR",      1, m_lenghtFull)); // register, pointer, flag löschen
            Add(new Instruction("OR",       3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("XOR",      3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("AND",      3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("NAND",     3, m_lenghtFull)); //  OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("NOR",      3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("NXOR",     3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("NOT",      3, m_lenghtFull)); //+ OP(4) T(1)V(4)  T(1)V(4) 
            Add(new Instruction("RET",      0, 0, m_lenghtFull));
            Add(new Instruction("CALL",     0, 1, m_lenghtFull));
            
            Add(new Instruction("JC",       0, 0, m_lenghtFull)); // Jump Carry OP(4) 
            Add(new Instruction("JNC",      0, 0, m_lenghtFull)); // Jump not carry
            Add(new Instruction("JO",       0, 0, m_lenghtFull)); // Jump overflow
            Add(new Instruction("JNO",      0, 0, m_lenghtFull)); // Jump not overflow 
            Add(new Instruction("INC",      1, m_lenghtFull));
            Add(new Instruction("DEC",      1, m_lenghtFull));
            Add(new Instruction("INV",      1, m_lenghtFull)); // register, flags, pointer invetieren
            Add(new Instruction("STO",      1, m_lenghtFull)); // register, flags, pointer = 1
            //
            Add(new Instruction("JG",       19, 0, m_lenghtFull)); // Jump greater:  OP(4) T(1)V(4) > T(1)V(4) T(1)V(4)
            Add(new Instruction("JGE",      19, 0, m_lenghtFull)); // Jump greater equel  OP(4) T(1)V(4) >= T(1)V(4) T(1)V(4) 
            Add(new Instruction("JL",       19, 0, m_lenghtFull)); // Jump less OP(4) T(1)V(4) < T(1)V(4) T(1)V(4) 
            Add(new Instruction("JLE",      19, 0, m_lenghtFull)); // Jump less equel OP(4) T(1)V(4) <= T(1)V(4) T(1)V(4) 


            Add(new Instruction("HADD",     3, m_langhtHalf)); // HADD return to V1 not to AX OP(4) T(1)V(2)  T(1)V(2) T(1)V(2)
            Add(new Instruction("HSUB",     3, m_langhtHalf)); // HSUB return to V1 not to AX OP(4) T(1)V(2)  T(1)V(2) T(1)V(2) 
            Add(new Instruction("HMUL",     3, m_langhtHalf)); // HMUL return to V1 not to AX OP(4) T(1)V(2)  T(1)V(2) T(1)V(2) 
            Add(new Instruction("HDIV",     3, m_langhtHalf)); // HDIV return to V1 not to AX OP(4) T(1)V(2)  T(1)V(2) T(1)V(2) 
            Add(new Instruction("HINC",     1, m_langhtHalf));
            Add(new Instruction("HDEC",     1, m_langhtHalf));
            Add(new Instruction("HINV",     1, m_langhtHalf)); // register, flags, pointer invetieren
            Add(new Instruction("HEXP",     3, m_langhtHalf)); // HEXP return to V1 not to AX OP(4) T(1)V(2)  T(1)V(2) T(1)V(2) 
            Add(new Instruction("HSQR", 3, m_lenghtFull));

            // Float
            Add(new Instruction("FADD", 16, 3, m_lenghtFull * 2)); // ADD return to V1 not to AX OP(2) T(2)V(4)  T(2)V(4) T(2)V(4)
            Add(new Instruction("FSUB", 16, 3, m_lenghtFull * 2)); // SUB return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add(new Instruction("FMUL", 16, 3, m_lenghtFull * 2)); // MUL return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4) 
            Add(new Instruction("FDIV", 16, 3, m_lenghtFull * 2)); // DIV return to V1 not to AX OP(2) T(1)V(4)  T(1)V(4) T(1)V(4)
            Add(new Instruction("FINC", 8, 1, m_lenghtFull * 2));
            Add(new Instruction("FDEC", 8, 1, m_lenghtFull * 2));
            Add(new Instruction("FINV", 8, 1, m_lenghtFull * 2)); // register, flags, pointer invetieren
            Add(new Instruction("FMOV", 40, 5, m_lenghtFull * 2)); // MOV(4) F(8) F(8) F(8) F(8) R(4)  - FMOV 21.0f, 6.0f, 12.0f, 18,0f, F0
            Add(new Instruction("FEXP", 16, 3, m_lenghtFull * 2)); // Exponent FEXP(4) A(4), B(4), ERG(4) - Erg = A^B
            Add(new Instruction("FSQR", 16, 3, m_lenghtFull * 2)); // Würzel X von Arg ERG = Würzel B von A



            // Extended Math

        }
        public override string ToString()
        {
            string _ret = "";
            for (int i = 0; i < this.Count; i++)
            {
                _ret += i.ToString() + " : " + this.ElementAt<Instruction>(i).ToString() + "\n";
            }
            return _ret;
        }
    }
    public enum RegisterType : int
    {
        Regsiter = 0,
        System = 1,
        Float = 2,
        Vector = 4,

    }
    /// <summary>
    /// Item der Registers Liste
    /// </summary>
	public class Register
    {
        /// <summary>
        /// Name des Registers
        /// </summary>
		public string Name;
        /// <summary>
        /// Typ des Registers
        /// </summary>
		public RegisterType Type;

        /// <summary>
        /// Konstructor für ein Register
        /// </summary>
        /// <param name="n">Name des Registers</param>
        /// <param name="t">Typ des Registers</param>
		public Register(string n, int t) { Name = n; Type = (RegisterType)t; }
    }
    /// <summary>
    /// Liste aller vorhandenen Register
    /// </summary>
	public class Registers : System.Collections.Generic.List<Register>
    {
        public Registers()
        {
            Add(new Register("R0", 0)); //0 Stack Pointer 
            Add(new Register("R1", 0)); //1 Intrsuction Pointer
            Add(new Register("R2", 0)); //2 Akku A
            Add(new Register("R3", 0)); //3 Akku B
            Add(new Register("R4", 0)); //
            Add(new Register("R5", 0)); //
            Add(new Register("R6", 0)); //
            Add(new Register("R7", 0)); //
            Add(new Register("R8", 0)); //
            Add(new Register("R9", 0)); //
            Add(new Register("RA", 0)); //
            Add(new Register("RB", 0)); //
            Add(new Register("RC", 0)); //
            Add(new Register("RD", 0)); //
            Add(new Register("RE", 0)); //
            Add(new Register("RF", 0)); //

            Add(new Register("CF", 1)); //5 Carry Flag
            Add(new Register("ZF", 1)); //6 Zero Flag
            Add(new Register("UF", 1)); //7 Underflow Flag
            Add(new Register("OF", 1)); //8 Overflow Flag
            Add(new Register("SF", 1)); //9 Sign Flag
            Add(new Register("IP", 1)); //10 Intrsuction Pointer
            Add(new Register("SP", 1)); //10 Intrsuction Pointer

            Add(new Register("F0", 2));  // Float 0
            Add(new Register("F1", 2));  // Float 1 
            Add(new Register("F2", 2));  // Float 2
            Add(new Register("F3", 2));  // Float 3
            Add(new Register("F4", 2));  // Float 4

            // Extended Math 1.0
            Add(new Register("ETH0", 4));  // Vector 0
            Add(new Register("ETH1", 4));  // Vector 1
            Add(new Register("ETH2", 4));  // Vector 2
            Add(new Register("ETH3", 4));  // Vector 3

        }
        /// <summary>
        /// Ist der Register Name in der Liste vorhanden
        /// </summary>
        /// <param name="str">Register name der gesucht wird</param>
        /// <returns>true wenn der Registername in der Liste ist</returns>
		public bool Contains(string str)
        {
            foreach (var item in this)
            {
                if (item.Name == str)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Index of des gesuchten Registers
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Index des Registers in der liste</returns>
		public int IndexOf(string name)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == name.ToUpper())
                    return i;
            }
            return -1;
        }
    }

    public class MemMap
    {
        /// <summary>
        /// Name des Registers
        /// </summary>
		public string Name;
        /// <summary>
        /// Adress
        /// </summary>
		public ulong Adress;

        public ulong AdressEnd;

        /// <summary>
        /// Konstructor für ein Register
        /// </summary>
        /// <param name="n">Name des Registers</param>
        /// <param name="t">Typ des Registers</param>
		public MemMap(string n, ulong t, ulong size) { Name = n; Adress = t; AdressEnd = size;  }
    }

    public class IOMap : System.Collections.Generic.List<MemMap>
    {
        public IOMap() {
            Add(new MemMap("Cache", 0xC100000, 0xC10FFFF)); // Client to Host 
        }

        public bool Contains(string str)
        {
            foreach (var item in this)
            {
                if (item.Name == str)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Index of des gesuchten Registers
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Index des Registers in der liste</returns>
		public int IndexOf(string name)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == name.ToUpper())
                    return i;
            }
            return -1;
        }
    }
}
