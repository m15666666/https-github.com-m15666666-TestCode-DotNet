using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个无序的整数数组，找到其中最长上升子序列的长度。

示例:

输入: [10,9,2,5,3,7,101,18]
输出: 4 
解释: 最长的上升子序列是 [2,3,7,101]，它的长度是 4。
说明:

可能会有多种最长上升子序列的组合，你只需要输出对应的长度即可。
你算法的时间复杂度应该为 O(n2) 。
进阶: 你能将算法的时间复杂度降低到 O(n log n) 吗? 
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-increasing-subsequence/
/// 300. 最长上升子序列
/// https://blog.csdn.net/OneDeveloper/article/details/80047289
/// </summary>
class LongestIncreasingSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LengthOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        int[] dp = new int[nums.Length];
        dp[0] = 1;
        int result = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            int max = 0;
            for (int j = i - 1; j >= 0; j--)
            {
                if (nums[i] > nums[j] && max < dp[j]) max = dp[j];
            }
            dp[i] = max + 1;
            if (result < dp[i]) result = dp[i];
        }
        return result;
    }
}
/*
// 谦虚的方法，不断改进最长上升序列，为
public class Solution {
    public int LengthOfLIS(int[] nums) {
        if (nums == null || nums.Length == 0)
            return 0;
        int maxS = 1;
        int[] seq = new int[nums.Length + 1];
        seq[1] = nums[0];
        for(int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > seq[maxS])
            {
                seq[++maxS] = nums[i];
            }
            else
            {
                seq[BinaryFind(seq, maxS, nums[i])] = nums[i];
            }
        }
        return maxS;
    }
    
    private int BinaryFind(int[] nums, int end, int target)
    {
        int start = 1;
        int mid;
        while(start <= end)
        {
            mid = start + (end - start) / 2;
            if (nums[mid] == target)
                return mid;
            if (nums[mid] > target)
                end = mid - 1;
            else
                start = mid + 1;
        }
        return start;
    }
} 
public class Solution
{
//    生成 dp 数组时使用二分查找算法来进行。
//用ends数组来保存单调序列，这个序列并不是 最优值的单调序列，对于arr[i],如果比 ends 中的每个
//数都大时放到 ends 后面，否则用arr[i]代替ends数组中第一个比arr[i]大的数，保持 ends 递增。这样
//每一个arr[i]在ends的位置就是 dp[i] 的值。
    public int LengthOfLIS(int[] nums)
    {
        if (nums.Length == 0) return 0;
        if (nums.Length == 1) return 1;
        int[] stack = new int[nums.Length];
        int top = 0;
        stack[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > stack[top])
                stack[++top] = nums[i];
            else
            {
                int low = 0, high = top, mid;
                while (low <= high)
                {
                    mid = (low + high) / 2;
                    if (nums[i] > stack[mid])
                        low = mid + 1;
                    else
                        high = mid - 1;
                }
                stack[low] = nums[i];
            }
        }
        return top + 1;
    }
}
public class Solution {
    public int LengthOfLIS(int[] nums) {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }
        
        var ans = int.MinValue;
        var dp = new int[nums.Length];
        for(int i = 0; i < nums.Length; i++)
        {
            dp[i] = 1;
            for(int j = 0; j < i; j++)
            {
                if(nums[j] < nums[i])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
            ans = Math.Max(ans, dp[i]);
        }
        return ans;
    }
}
public class Solution {
    public int LengthOfLIS(int[] nums) 
    {
        List<int> subList = new List<int>();
        for(int i = 0; i < nums.Length; i++)
        {
            int lastIndex = subList.Count-1;
            if(lastIndex < 0)
            {
                subList.Add(nums[i]);
            }
            else
            {
                if(subList[lastIndex] < nums[i])
                {
                    subList.Add(nums[i]);
                }
                else if(subList[lastIndex] > nums[i])
                {
                    for(int j = 0; j < subList.Count; j++)
                    {
                        if(subList[j] > nums[i] && (j == 0 || subList[j-1] < nums[i]))
                        {
                            subList[j] = nums[i];
                            break;
                        }
                    }
                }
            }
        }
        return subList.Count;
    }
}     
     
*/
