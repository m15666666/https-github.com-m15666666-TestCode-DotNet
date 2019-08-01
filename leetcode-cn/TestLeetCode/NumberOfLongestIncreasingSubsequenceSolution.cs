using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个未排序的整数数组，找到最长递增子序列的个数。

示例 1:

输入: [1,3,5,4,7]
输出: 2
解释: 有两个最长递增子序列，分别是 [1, 3, 4, 7] 和[1, 3, 5, 7]。
示例 2:

输入: [2,2,2,2,2]
输出: 5
解释: 最长递增子序列的长度是1，并且存在5个子序列的长度为1，因此输出5。
注意: 给定的数组长度不超过 2000 并且结果一定是32位有符号整数。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/number-of-longest-increasing-subsequence/
/// 673. 最长递增子序列的个数
/// https://blog.csdn.net/xuxuxuqian1/article/details/81071975
/// </summary>
class NumberOfLongestIncreasingSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindNumberOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        int n = nums.Length;
        int maxLength = 1;

        // maxLengths记录以i结尾的最长子序列长度 ，counts记录以i结尾的最长子序列的个数
        int[] maxLengths = new int[n];
        int[] counts = new int[n];
        Array.Fill(maxLengths, 1);
        Array.Fill(counts, 1);
        for (int i = 1; i < n; ++i)
        {
            for (int j = 0; j < i; ++j)
            {
                if (nums[j] < nums[i] && maxLengths[j] + 1 > maxLengths[i])
                {
                    maxLengths[i] = maxLengths[j] + 1;
                    counts[i] = counts[j];
                }
                else if (nums[j] < nums[i] && maxLengths[j] + 1 == maxLengths[i])
                {
                    counts[i] += counts[j];
                }
            }
            if (maxLength < maxLengths[i]) maxLength = maxLengths[i];
        }

        int ret = 0;
        for (int i = 0; i < n; ++i)
            if (maxLengths[i] == maxLength) ret += counts[i];
        return ret;
    }
}