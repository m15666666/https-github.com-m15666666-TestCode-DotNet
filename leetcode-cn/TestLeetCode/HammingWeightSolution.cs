using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 编写一个函数，输入是一个无符号整数，返回其二进制表达式中数字位数为 ‘1’ 的个数（也被称为汉明重量）。
/// </summary>
class HammingWeightSolution
{
    public static void Test()
    {
        uint num = 2147483648;
        int count = HammingWeight(num);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int HammingWeight(uint n)
    {
        int ret = 0;
        for( int i = 0; i < 32; i++)
        {
            if (n % 2 != 0) ret++;
            n >>= 1;
        }

        return ret;
    }    
}