using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定正整数 N ，我们按任何顺序（包括原始顺序）将数字重新排序，注意其前导数字不能为零。

如果我们可以通过上述方式得到 2 的幂，返回 true；否则，返回 false。

 

示例 1：

输入：1
输出：true
示例 2：

输入：10
输出：false
示例 3：

输入：16
输出：true
示例 4：

输入：24
输出：false
示例 5：

输入：46
输出：true
 

提示：

1 <= N <= 10^9 
*/
/// <summary>
/// https://leetcode-cn.com/problems/reordered-power-of-2/
/// 869. 重新排序得到 2 的幂
/// 
/// </summary>
class ReorderedPowerOf2Solution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool ReorderedPowerOf2(int N)
    {
        DigitCount(N, digitsN);

        for (int shift = 0; shift < 31; ++shift)
        {
            DigitCount(1 << shift, digitsShift);
            bool match = true;
            for (int i = 0; i < 10; i++)
            {
                if (digitsShift[i] != digitsN[i])
                {
                    match = false;
                    break;
                }
            }
            if (match) return true;
        }
        return false;
    }

    private int[] digitsN = new int[10];
    private int[] digitsShift = new int[10];

    // Returns the count of digits of N
    // Eg. N = 112223334, returns [0,2,3,3,1,0,0,0,0,0]
    private static void DigitCount(int N, int[] count)
    {
        Array.Fill(count, 0);
        while (0 < N)
        {
            count[N % 10]++;
            N /= 10;
        }
    }

    //private static readonly HashSet<int> TwoPowers = new HashSet<int> { 
    //    1, 1 << 1, 1<<2,1<<3,1<<4,1<<5,1<<6,
    //    1<<7,1<<8,1<<9,1<<10,1<<11,1<<12,1<<13,1<<14,
    //    1<<15,1<<16,1<<17,1<<18,1<<19,1<<20,1<<21,1<<22,
    //    1<<23,1<<24,1<<25,1<<26,1<<27,1<<28,1<<29,1<<30,1<<31
    //};
}
/*
public class Solution {
    public bool ReorderedPowerOf2(int N)
    {
        var target = toOrderedString((uint)N);
        for (long i = 1; i <= int.MaxValue; i *= 2)
        {
            if (target == toOrderedString(i))
                return true;
        }

        return false;

        string toOrderedString(long x)
        {
            var sb = new StringBuilder();
            while (x > 0)
            {
                var val = x % 10;
                sb.Append((char)(val + '0'));

                x /= 10;
            }

            var result = string.Concat(sb.ToString().OrderBy(ch => ch));
            return result;
        }
    }
}

*/
