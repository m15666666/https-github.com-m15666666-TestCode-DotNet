using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 颠倒给定的 32 位无符号整数的二进制位。
/// Input: 43261596
/// Output: 964176192 
/// Explanation: 43261596 represented in binary as 00000010100101000001111010011100, 
/// return 964176192 represented in binary as 00111001011110000010100101000000.
/// 
/// https://blog.csdn.net/hy971216/article/details/80524704
/// </summary>
class ReverseBitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public uint reverseBits(uint n)
    {
        uint reverse_num = 0;
        const int bitCount = 32;
        for ( int i = 0; i < bitCount; i++)
        {
           uint temp = (n & (uint)(1 << i));
            if (temp != 0)
                reverse_num |= (uint)(1 << ((bitCount - 1) - i));
        }

        return reverse_num;
    }
}