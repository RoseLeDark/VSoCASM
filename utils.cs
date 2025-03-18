using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsocasm
{
    public static class Utils
    {
        public static uint ToBoundary(this uint number, uint boundary)
        {
            uint newNumber = (uint)(boundary * ((number / boundary) + ((number % boundary > 0) ? 1 : 0)));
            return newNumber;
        }
        public static byte[] ToBytes(this string text)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            return ascii.GetBytes(text);
        }
        public unsafe static byte[] ToBytes(this uint UIntIn)
        {
            //turn a uint into 4 bytes
            byte[] fourBytes = new byte[4];
            uint* pt = &UIntIn;
            byte* bt = (byte*)&pt[0];
            fourBytes[0] = *bt++;
            fourBytes[1] = *bt++;
            fourBytes[2] = *bt++;
            fourBytes[3] = *bt++;
            return fourBytes;
        }
        public unsafe static byte[] ToBytes(this int IntIn)
        {
            //turn a uint into 4 bytes
            byte[] fourBytes = new byte[4];
            int* pt = &IntIn;
            byte* bt = (byte*)&pt[0];
            fourBytes[0] = *bt++;
            fourBytes[1] = *bt++;
            fourBytes[2] = *bt++;
            fourBytes[3] = *bt++;
            return fourBytes;
        }
        public unsafe static uint ToUint(this byte[] BytesIn)
        {
            fixed (byte* otherbytes = BytesIn)
            {
                uint newUint = 0;
                uint* ut = (uint*)&otherbytes[0];
                newUint = *ut;
                return newUint;
            }
        }
        public static bool Get(this uint A, int pos)
        {
            return (A >> pos & 1) == 1;
        }
        public static uint Set(this uint A, bool bit, int pos)
        {
            uint res = (!bit) ? (uint)(A & ~(1 << (pos))) : (uint)(A | (uint)(1 << pos));
            return res;

        }
        public static uint Chg(this uint A, int pos)
        {
            return A.Set(!A.Get(pos), pos);
        }
        public unsafe static byte[] ToBytes(this ushort UIntIn)
        {
            //turn a uint into 4 bytes
            byte[] fourBytes = new byte[2];
            ushort* pt = &UIntIn;
            byte* bt = (byte*)&pt[0];
            fourBytes[0] = *bt++;
            fourBytes[1] = *bt++;
            return fourBytes;
        }
        public unsafe static UInt16 ToShort(this byte[] BytesIn)
        {
            fixed (byte* otherbytes = BytesIn)
            {
                ushort newUint = 0;
                ushort* ut = (ushort*)&otherbytes[0];
                newUint = *ut;
                return newUint;
            }
        }
    }
}
