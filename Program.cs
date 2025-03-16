using System;

namespace vsocasm
{
    static class Programm
    {
        private static Registers registers = new ();
        private static Instructions instructions = new (32);
        private static IOMap memMap = new ();

        static void Main()
        {
            Console.WriteLine(instructions.ToString());
            
        }
    }
}