
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


            // MEM
            Add(new Instruction("XMEM", 3, m_lenghtFull)); // Set Memore OP(2) X=T(2)V(4) Y= T(2)V(4) Mem= T(2)V(4)
            Add(new Instruction("GMEM", 3, m_lenghtFull)); // Get Mem Pos X Y from Addrs on RF

        

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
