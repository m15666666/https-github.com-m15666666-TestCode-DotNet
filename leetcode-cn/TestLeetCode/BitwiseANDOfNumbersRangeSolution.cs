using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/bitwise-and-of-numbers-range/
/// 201. 数字范围按位与
/// 给定范围 [m, n]，其中 0 <= m <= n <= 2147483647，返回此范围内所有数字的按位与（包含 m, n 两端点）。
/// 示例 1: 
/// 输入: [5,7]
/// 输出: 4
/// 示例 2:
/// 输入: [0,1]
/// 输出: 0
/// </summary>
class BitwiseANDOfNumbersRangeSolution
{
    public static void Test()
    {
        var ret = RangeBitwiseAnd(5, 7);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int RangeBitwiseAnd(int m, int n)
    {
        if (m == 0) return 0;
        if (m == n) return m;

        int ret = 0;

        // 首先右移的是第31位（右移30位），32位始终为零（非负整数）
        int rightShiftCount = 30;
        while( 0 <= rightShiftCount)
        {
            var mbit = (m >> rightShiftCount) & 1;
            var nbit = (n >> rightShiftCount) & 1;

            if (mbit != nbit) break; // 从这个位及后面的低位都是0.

            if (mbit == 1) ret |= (1 << rightShiftCount);

            --rightShiftCount;
        }
        
        return ret;
    }
}
/*
// 别人的解法
    public int RangeBitwiseAnd(int m, int n) {
        int i  = 0;
        while((m >> i) != (n >> i))
            i++;
        return (m >> i) << i;
    }
*/
