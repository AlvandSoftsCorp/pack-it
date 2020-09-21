using System;
using System.Text;

namespace PackIt
{
    public class MyCypher
    {
        private bool NoCipher = false;
        private LFSR64[] R = null;
        private byte[] Key = null;
        private byte[] Rand = null;
        private int DigestByteIndex = 0;
        private uint[] DigestByteMask = { 0x000000FF, 0x0000FF00, 0x00FF0000, 0xFF000000 };
        private uint Digest = 0;
        private long TotalBytesCiphered = 0;

        public string GetStat()
        {
            if (this.NoCipher) return "No Cipher";

            string stat = "";
            stat += string.Format("Key Length: {0} bytes\n", this.Key.Length);
            stat += string.Format("Rand Seed: '{0}' \n", ASCIIEncoding.ASCII.GetString(this.Rand));
            stat += string.Format("Total Bytes: {0:N2} (MB) \n", this.TotalBytesCiphered/(1024d*1024d));

            stat += string.Format("Clocking:\n");
            double total_clock_cnt = 0;

            for (int i = 0; i < R.Length; i++)
            {
                total_clock_cnt += R[0].ClockCounter;
            }

            stat += string.Format("R0: {0} times. Ratio: {1:N2} \n", R[0].ClockCounter, R[0].ClockCounter / total_clock_cnt);
            stat += string.Format("R1: {0} times. Ratio: {1:N2} \n", R[1].ClockCounter, R[1].ClockCounter / total_clock_cnt);
            stat += string.Format("R2: {0} times. Ratio: {1:N2} \n", R[2].ClockCounter, R[2].ClockCounter / total_clock_cnt);
            stat += string.Format("R3: {0} times. Ratio: {1:N2} \n", R[3].ClockCounter, R[3].ClockCounter / total_clock_cnt);
            stat += string.Format("R4: {0} times. Ratio: {1:N2} \n", R[4].ClockCounter, R[4].ClockCounter / total_clock_cnt);

            return stat;
        }

        public MyCypher(string Key, string Rand)
        {
            this.Init(Key, Rand);
        }
        
        public MyCypher(string Key, ulong Rand)
        {
            this.Init(Key, Rand);
        }

        public void Cipher(ref byte[] Data, int Count)
        {
            cip(ref Data, Count);
        }

        public void Cipher(ref byte[] Data)
        {
            cip(ref Data, Data.Length);
        }

        private void cip(ref byte[] Data, int Count)
        {
            if (this.NoCipher) return;
            for (int i = 0; i < Count; i++)
            {
                Data[i] ^= this.GetNextByte();
                this.TotalBytesCiphered++;
            }
        }


        public void Decipher(ref byte[] CipheredData, int Count)
        {
            cip(ref CipheredData, Count);
        }
        public void Decipher(ref byte[] CipheredData)
        {
            cip(ref CipheredData, CipheredData.Length);
        }

        private void Init(string Key, string Rand)
        {
            byte[] key = ASCIIEncoding.ASCII.GetBytes(Key);
            byte[] rand = ASCIIEncoding.ASCII.GetBytes(Rand);
            this.Init(key, rand);
        }

        private void Init(string Key, ulong Rand)
        {
            byte[] key = ASCIIEncoding.ASCII.GetBytes(Key);
            byte[] rand = BitConverter.GetBytes(Rand);
            this.Init(key, rand);
        }
        
        private void Init(byte[] Key, byte[] Rand)
        {
            this.TotalBytesCiphered = 0;
            if ((Key == null) || Key.Length == 0)
            {
                this.NoCipher = true;
                return;
            }
            else
            {
                this.Key = Key;
                this.Rand = Rand;

                LFSR64 R0 = new LFSR64(64, 16, new int[] { 24, 34, 45, 53, 63 }, new int[] { 19, 26, 37, 46, 60 });
                LFSR64 R1 = new LFSR64(64, 24, new int[] { 22, 31, 44, 52, 63 }, new int[] { 18, 28, 38, 49, 59 });
                LFSR64 R2 = new LFSR64(64, 37, new int[] { 25, 33, 41, 51, 63 }, new int[] { 17, 27, 36, 47, 58 });
                LFSR64 R3 = new LFSR64(64, 45, new int[] { 21, 32, 43, 55, 63 }, new int[] { 20, 30, 40, 50, 56 });
                LFSR64 R4 = new LFSR64(64, 56, new int[] { 23, 35, 42, 45, 63 }, new int[] { 16, 26, 39, 48, 57 });
                R = new LFSR64[] { R0, R1, R2, R3, R4 };

                this.Feed(this.Rand);
                this.Feed(this.Key);
                this.Mixup();
            }
        }

        private void Mixup()
        {
            for (int i = 0; i < 1024; i++)
            {
                Clock();
            }
            for (int i = 0; i < 1024; i++)
            {
                Clock();
            }
        }

        private void Feed(byte[] Food)
        {
            bool input_bit = false;
            for (int i = 0; i < Food.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    input_bit = GetBit(Food[i], j);
                    for (int k = 0; k < R.Length; k++)
                    {
                        R[k].Clock(input_bit);
                    }
                }
            }
        }

        private bool GetBit(byte InputByte, int BitIndex)
        {
            byte b = 1;
            b = (byte)(b << (byte)BitIndex);
            return ((InputByte & b) != 0);
        }


        private void Clock()
        {
            bool x = R[0].GetSelBit() ^ R[1].GetSelBit();
            bool y = R[2].GetSelBit() ^ R[3].GetSelBit();
            bool z = R[4].GetSelBit() ^ R[0].GetSelBit();

            bool xp = !x;
            bool yp = !y;
            bool zp = !z;

            //f[0] = xp || yp && zp;                              // f0 = x' + y'z'
            //f[1] = xp && yp || x && z || x && y;                // f1 = x'y' + xz + xy
            //f[2] = x && yp && z || y && zp || xp && y;          // f2 = xy'z + yz' + x'y
            //f[3] = xp && zp || x && yp || x && z;               // f3 = x'z' + xy' + xz
            //f[4] = xp && z || x && y || x && zp;                // f4 = x'z + xy + xz'


            if (xp || yp && zp) R[0].Clock();
            if (xp && yp || x && z || x && y) R[1].Clock();
            if (x && yp && z || y && zp || xp && y) R[2].Clock();
            if (xp && zp || x && yp || x && z) R[3].Clock();
            if (xp && z || x && y || x && zp) R[4].Clock();
        }

        private byte GetNextByte()
        {
            byte b = 0;
            if (DigestByteIndex == 0)
            {
                this.Clock();
                DigestByteIndex = 0;
                Digest = GetDigest();
            }
            b = (byte)((Digest & DigestByteMask[DigestByteIndex]) >> (DigestByteIndex * 8));
            DigestByteIndex = (DigestByteIndex + 1) % 4;
            return b;
        }
        
        
        public uint GetDigest()
        {
            UInt64 D = 0;
            D = R[0].R ^ ~R[1].R ^ R[2].R ^ ~R[3].R ^ R[4].R;
            D ^= (D >> 32);
            D &= 0x00000000FFFFFFFF;
            return (uint)D;
        }

    }

    class LFSR64
    {

        public UInt64 R = 0;
        private int BitsCount = 0;
        private UInt64 MaskFeedPos = 0;
        private UInt64 MaskFeedNeg = 0;
        private UInt64 MaskSel = 0;

        public long ClockCounter = 0;

        public bool GetSelBit()
        {
            return (MaskSel & R) != 0;
        }

        public LFSR64(int BitsCount, int SelBitIndex, int[] PosFeed, int[] NegFeed)
        {
            this.BitsCount = BitsCount;
            this.R = 0;

            for (int i = 0; i < PosFeed.Length; i++)
            {
                MaskFeedPos |= (UInt64)((UInt64)0x1 << PosFeed[i]);
            }
            
            for (int i = 0; i < NegFeed.Length; i++)
            {
                MaskFeedNeg |= (UInt64)((UInt64)0x1 << NegFeed[i]);
            }

            MaskSel = (UInt64)(((UInt64)0x1) << SelBitIndex);
        }


        private bool GetFeedback()
        {
            UInt64 k = 0;
            k = (MaskFeedPos & R) | (MaskFeedNeg & R);
            k ^= MaskFeedNeg;
            k ^= (k >> 32);
            k ^= (k >> 16);
            k ^= (k >> 8);
            k ^= (k >> 4);
            k ^= (k >> 2);
            k ^= (k >> 1);
            k &= 1;
            return (k == 1); 
        }

        public void Clear()
        {
            R = 0;
        }

        public void Clock()
        {
            Clock(false);
        }

        public void Clock(bool InputBit)
        {
            bool feedback = this.GetFeedback();
            feedback ^= InputBit;
            R = R << 1;
            if (feedback==true/*feedback == 1*/) R |= 0x1;
            ClockCounter++;
        }
    }
}