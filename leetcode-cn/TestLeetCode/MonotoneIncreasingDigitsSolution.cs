using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数 N，找出小于或等于 N 的最大的整数，同时这个整数需要满足其各个位数上的数字是单调递增。

（当且仅当每个相邻位数上的数字 x 和 y 满足 x <= y 时，我们称这个整数是单调递增的。）

示例 1:

输入: N = 10
输出: 9
示例 2:

输入: N = 1234
输出: 1234
示例 3:

输入: N = 332
输出: 299
说明: N 是在 [0, 10^9] 范围内的一个整数。
*/
/// <summary>
/// https://leetcode-cn.com/problems/monotone-increasing-digits/
/// 738. 单调递增的数字
/// https://www.cnblogs.com/tangweijqxx/p/10689863.html
/// </summary>
class MonotoneIncreasingDigitsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MonotoneIncreasingDigits(int N)
    {
        var s = N.ToString();
        if (s.Length == 1) return N;

        List<int> nums = new List<int>(s.Length);
        foreach ( var c in s)
        {
            nums.Add(c - '0');
        }

        int position = nums.Count;
        for ( int i = nums.Count - 1; 0 < i; i--)
        {
            if (nums[i] < nums[i - 1])
            {
                nums[i - 1]--;

                position = i;
            }
        }
        for (int j = position; j < nums.Count; j++)
            nums[j] = 9;

        int ret = 0;
        foreach (var n in nums)
        {
            ret = ret * 10 + n;
        }
        return ret;
    }
}