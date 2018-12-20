using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/minimum-size-subarray-sum/
/// 209. 长度最小的子数组
/// 给定一个含有 n 个正整数的数组和一个正整数 s ，找出该数组中满足其和 ≥ s 的长度最小的连续子数组。
/// 如果不存在符合条件的连续子数组，返回 0。
/// 示例: 
/// 输入: s = 7, nums = [2,3,1,2,4,3]
/// 输出: 2
/// 解释: 子数组[4, 3] 是该条件下的长度最小的连续子数组。
/// 进阶:
/// 如果你已经完成了O(n) 时间复杂度的解法, 请尝试 O(n log n) 时间复杂度的解法。
/// </summary>
class MinimumSizeSubarraySumSolution
{
    public static void Test()
    {
        var ret = MinSubArrayLen(7, new[] { 2, 3, 1, 2, 4, 3 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int MinSubArrayLen(int s, int[] nums)
    {
        if (s < 1) return 1;
        if (nums == null || nums.Length == 0) return 0;
        int lastIndex = nums.Length - 1;
        int sum = 0;
        int minLength = int.MaxValue;
        for( int i = lastIndex; -1 < i; i--)
        {
            sum += nums[i];
            if (sum < s) continue;
            while( s <= sum && i <= lastIndex )
            {
                var len = lastIndex - i + 1;
                if (len < minLength) minLength = len;
                sum -= nums[lastIndex--];
            }
        }
        return minLength == int.MaxValue ? 0 : minLength;
    }

}