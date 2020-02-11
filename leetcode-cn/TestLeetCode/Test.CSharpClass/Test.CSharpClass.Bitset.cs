using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Collections;

namespace Test.CSharpClass
{
    /// <summary>
    /// 
    /// 参考：
    /// https://stackoverflow.com/questions/8144349/what-is-the-c-sharp-equivalent-of-stdbitset-of-c
    /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.specialized.bitvector32?redirectedfrom=MSDN&view=netframework-4.8
    /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.bitarray?redirectedfrom=MSDN&view=netframework-4.8
    /// https://blog.csdn.net/kongmin_123/article/details/82225172
    /// 
    /// </summary>
    public static class Bitset
    {
        public static void Test() 
        {
            TestBitVector32();
            TestBitArray();
        }

        public static void TestBitVector32()
        {
            {
                // Creates and initializes a BitVector32 with all bit flags set to FALSE.
                BitVector32 myBV = new BitVector32(0);

                // Creates masks to isolate each of the first five bit flags.
                int myBit1 = BitVector32.CreateMask();
                int myBit2 = BitVector32.CreateMask(myBit1);
                int myBit3 = BitVector32.CreateMask(myBit2);
                int myBit4 = BitVector32.CreateMask(myBit3);
                int myBit5 = BitVector32.CreateMask(myBit4);

                // Sets the alternating bits to TRUE.
                Console.WriteLine("Setting alternating bits to TRUE:");
                Console.WriteLine("   Initial:         {0}", myBV.ToString());
                myBV[myBit1] = true;
                Console.WriteLine("   myBit1 = TRUE:   {0}", myBV.ToString());
                myBV[myBit3] = true;
                Console.WriteLine("   myBit3 = TRUE:   {0}", myBV.ToString());
                myBV[myBit5] = true;
                Console.WriteLine("   myBit5 = TRUE:   {0}", myBV.ToString());

                /*
                    This code produces the following output.

                    Setting alternating bits to TRUE:
                       Initial:         BitVector32{00000000000000000000000000000000}
                       myBit1 = TRUE:   BitVector32{00000000000000000000000000000001}
                       myBit3 = TRUE:   BitVector32{00000000000000000000000000000101}
                       myBit5 = TRUE:   BitVector32{00000000000000000000000000010101}


                    */
            }
            {
                // Creates and initializes a BitVector32.
                BitVector32 myBV = new BitVector32(0);

                // Creates four sections in the BitVector32 with maximum values 6, 3, 1, and 15.
                // mySect3, which uses exactly one bit, can also be used as a bit flag.
                BitVector32.Section mySect1 = BitVector32.CreateSection(6);
                BitVector32.Section mySect2 = BitVector32.CreateSection(3, mySect1);
                BitVector32.Section mySect3 = BitVector32.CreateSection(1, mySect2);
                BitVector32.Section mySect4 = BitVector32.CreateSection(15, mySect3);

                var v = myBV[mySect1];

                // Displays the values of the sections.
                Console.WriteLine("Initial values:");
                Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
                Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
                Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
                Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);

                // Sets each section to a new value and displays the value of the BitVector32 at each step.
                Console.WriteLine("Changing the values of each section:");
                Console.WriteLine("\tInitial:    \t{0}", myBV.ToString());
                myBV[mySect1] = 5;
                Console.WriteLine("\tmySect1 = 5:\t{0}", myBV.ToString());
                myBV[mySect2] = 3;
                Console.WriteLine("\tmySect2 = 3:\t{0}", myBV.ToString());
                myBV[mySect3] = 1;
                Console.WriteLine("\tmySect3 = 1:\t{0}", myBV.ToString());
                myBV[mySect4] = 9;
                Console.WriteLine("\tmySect4 = 9:\t{0}", myBV.ToString());

                // Displays the values of the sections.
                Console.WriteLine("New values:");
                Console.WriteLine("\tmySect1: {0}", myBV[mySect1]);
                Console.WriteLine("\tmySect2: {0}", myBV[mySect2]);
                Console.WriteLine("\tmySect3: {0}", myBV[mySect3]);
                Console.WriteLine("\tmySect4: {0}", myBV[mySect4]);

                /*
                This code produces the following output.

                Initial values:
                        mySect1: 0
                        mySect2: 0
                        mySect3: 0
                        mySect4: 0
                Changing the values of each section:
                        Initial:        BitVector32{00000000000000000000000000000000}
                        mySect1 = 5:    BitVector32{00000000000000000000000000000101}
                        mySect2 = 3:    BitVector32{00000000000000000000000000011101}
                        mySect3 = 1:    BitVector32{00000000000000000000000000111101}
                        mySect4 = 9:    BitVector32{00000000000000000000001001111101}
                New values:
                        mySect1: 5
                        mySect2: 3
                        mySect3: 1
                        mySect4: 9

                */
            }
        }

        public static void TestBitArray()
        {
            // Creates and initializes several BitArrays.
            BitArray myBA1 = new BitArray(5);

            BitArray myBA2 = new BitArray(5, false);

            byte[] myBytes = new byte[5] { 1, 2, 3, 4, 5 };
            BitArray myBA3 = new BitArray(myBytes);

            bool[] myBools = new bool[5] { true, false, true, true, false };
            BitArray myBA4 = new BitArray(myBools);

            int[] myInts = new int[5] { 6, 7, 8, 9, 10 };
            BitArray myBA5 = new BitArray(myInts);

            // Displays the properties and values of the BitArrays.
            Console.WriteLine("myBA1");
            Console.WriteLine("   Count:    {0}", myBA1.Count);
            Console.WriteLine("   Length:   {0}", myBA1.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA1, 8);

            Console.WriteLine("myBA2");
            Console.WriteLine("   Count:    {0}", myBA2.Count);
            Console.WriteLine("   Length:   {0}", myBA2.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA2, 8);

            Console.WriteLine("myBA3");
            Console.WriteLine("   Count:    {0}", myBA3.Count);
            Console.WriteLine("   Length:   {0}", myBA3.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA3, 8);

            Console.WriteLine("myBA4");
            Console.WriteLine("   Count:    {0}", myBA4.Count);
            Console.WriteLine("   Length:   {0}", myBA4.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA4, 8);

            Console.WriteLine("myBA5");
            Console.WriteLine("   Count:    {0}", myBA5.Count);
            Console.WriteLine("   Length:   {0}", myBA5.Length);
            Console.WriteLine("   Values:");
            PrintValues(myBA5, 8);

            /*
             进一步使用：使用BitArray.CopyTo输出到字节数组然后转换为字符串作为key来比较。

            int column = matrix[0].Length;
            byte[] buffer = new byte[column / 8 + 1];
            var bitArray = new System.Collections.BitArray(column);
            var bits2Count = new Dictionary<string, int>();
            foreach( var row in matrix)
            {
                int index = 0;
                bool reverse = row[0] == 0;
                if (reverse)
                    foreach( var bit in row)
                        bitArray.Set(index++, bit == 0);
                else
                    foreach (var bit in row)
                        bitArray.Set(index++, bit == 1);

                bitArray.CopyTo(buffer, 0);
                var bitString = BitConverter.ToString(buffer);
                if (!bits2Count.ContainsKey(bitString)) bits2Count.Add(bitString, 1);
                else bits2Count[bitString]++;
            }
            return bits2Count.Values.Max();
             
             */

            /* 
            This code produces the following output.

            myBA1
               Count:    5
               Length:   5
               Values:
               False   False   False   False   False
            myBA2
               Count:    5
               Length:   5
               Values:
               False   False   False   False   False
            myBA3
               Count:    40
               Length:   40
               Values:
                True   False   False   False   False   False   False   False
               False    True   False   False   False   False   False   False
                True    True   False   False   False   False   False   False
               False   False    True   False   False   False   False   False
                True   False    True   False   False   False   False   False
            myBA4
               Count:    5
               Length:   5
               Values:
                True   False    True    True   False
            myBA5
               Count:    160
               Length:   160
               Values:
               False    True    True   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
                True    True    True   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False    True   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
                True   False   False    True   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False    True   False    True   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
               False   False   False   False   False   False   False   False
            */
        }

        private static void PrintValues(IEnumerable myList, int myWidth)
        {
            int i = myWidth;
            foreach (Object obj in myList)
            {
                if (i <= 0)
                {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,8}", obj);
            }
            Console.WriteLine();
        }
    }
}
