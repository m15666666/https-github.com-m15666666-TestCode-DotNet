using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
输入一个按升序排序的整数数组（可能包含重复数字），你需要将它们分割成几个子序列，
其中每个子序列至少包含三个连续整数。返回你是否能做出这样的分割？

示例 1：

输入: [1,2,3,3,4,5]
输出: True
解释:
你可以分割出这样两个连续子序列 : 
1, 2, 3
3, 4, 5

示例 2：

输入: [1,2,3,3,4,4,5,5]
输出: True
解释:
你可以分割出这样两个连续子序列 : 
1, 2, 3, 4, 5
3, 4, 5
 
示例 3：

输入: [1,2,3,4,4,5]
输出: False

提示：

输入的数组长度范围为 [1, 10000] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/split-array-into-consecutive-subsequences/
/// 659. 分割数组为连续子序列
/// https://blog.csdn.net/LaputaFallen/article/details/80034863
/// </summary>
class SplitArrayIntoConsecutiveSubsequencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPossible(int[] nums)
    {
        Dictionary<int, int> freq = new Dictionary<int, int>();
        Dictionary<int, int>  appendfreq = new Dictionary<int, int>();

        foreach (int i in nums) freq[i] = freq.GetValueOrDefault(i, 0) + 1;
        foreach (int i in nums)
        {
            //若当前元素个数为0， continue
            if (freq[i] == 0) continue;
            // 存在一个线索头
            else if (0 < appendfreq.GetValueOrDefault(i, 0))
            {
                appendfreq[i]--;

                // 更新一个线索头
                appendfreq[i+1] = appendfreq.GetValueOrDefault(i + 1, 0) + 1;
            }
            //表示当前元素可以作为新序列的第一个元素
            else if ( 0 < freq.GetValueOrDefault(i + 1, 0) 
                && 0 < freq.GetValueOrDefault(i + 2, 0) )
            {
                freq[i + 1]--;
                freq[i + 2]--;

                // 创建一个线索头
                appendfreq[i + 3] = appendfreq.GetValueOrDefault(i + 3, 0) + 1;
            }
            else return false;

            freq[i]--;
        }

        return true;
    }
}