using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 两个整数之间的汉明距离指的是这两个数字对应二进制位不同的位置的数目。
/// </summary>
class HammingDistanceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int HammingDistance(int x, int y)
    {
        int xor = x ^ y;
        int ret = HammingWeight((uint)xor);
        return ret;
    }

    private static int HammingWeight(uint n)
    {
        int ret = 0;
        for (int i = 0; i < 32; i++)
        {
            if (n % 2 != 0) ret++;
            n >>= 1;
        }

        return ret;
    }
}