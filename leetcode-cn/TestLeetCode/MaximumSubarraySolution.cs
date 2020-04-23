using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和。

示例:

输入: [-2,1,-3,4,-1,2,1,-5,4],
输出: 6
解释: 连续子数组 [4,-1,2,1] 的和最大，为 6。
进阶:

如果你已经实现复杂度为 O(n) 的解法，尝试使用更为精妙的分治法求解
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-subarray/
/// 53. 最大子序和
/// 
/// 
/// </summary>
class MaximumSubarraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }
    public int MaxSubArray(int[] nums) 
    {
        if (nums == null || nums.Length == 0) return 0;

        int len = nums.Length;
        int preIndexMaxSum = nums[0];
        int ret = preIndexMaxSum;
        for (int i = 1; i < len; i++) 
        {
            if (0 < preIndexMaxSum) preIndexMaxSum += nums[i];
            else preIndexMaxSum = nums[i];

            if (ret < preIndexMaxSum) ret = preIndexMaxSum;
        }

        return ret;
    }
}
/*

最大子序和
力扣 (LeetCode)
发布于 4 个月前
75.5k
方法一：分治法
这个是使用分治法解决问题的典型的例子，并且可以用与合并排序相似的算法求解。下面是用分治法解决问题的模板：

定义基本情况。
将问题分解为子问题并递归地解决它们。
合并子问题的解以获得原始问题的解。
算法：
当最大子数组有 n 个数字时：

若 n==1，返回此元素。
left_sum 为最大子数组前 n/2 个元素，在索引为 (left + right) / 2 的元素属于左子数组。
right_sum 为最大子数组的右子数组，为最后 n/2 的元素。
cross_sum 是包含左右子数组且含索引 (left + right) / 2 的最大值。
在这里插入图片描述

class Solution {
  public int crossSum(int[] nums, int left, int right, int p) {
    if (left == right) return nums[left];

    int leftSubsum = Integer.MIN_VALUE;
    int currSum = 0;
    for(int i = p; i > left - 1; --i) {
      currSum += nums[i];
      leftSubsum = Math.max(leftSubsum, currSum);
    }

    int rightSubsum = Integer.MIN_VALUE;
    currSum = 0;
    for(int i = p + 1; i < right + 1; ++i) {
      currSum += nums[i];
      rightSubsum = Math.max(rightSubsum, currSum);
    }

    return leftSubsum + rightSubsum;
  }

  public int helper(int[] nums, int left, int right) {
    if (left == right) return nums[left];

    int p = (left + right) / 2;

    int leftSum = helper(nums, left, p);
    int rightSum = helper(nums, p + 1, right);
    int crossSum = crossSum(nums, left, right, p);

    return Math.max(Math.max(leftSum, rightSum), crossSum);
  }

  public int maxSubArray(int[] nums) {
    return helper(nums, 0, nums.length - 1);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N \log N)O(NlogN)。
空间复杂度：\mathcal{O}(\log N)O(logN)，递归时栈使用的空间。
方法二：贪心
使用单个数组作为输入来查找最大（或最小）元素（或总和）的问题，贪心算法是可以在线性时间解决的方法之一。
每一步都选择最佳方案，到最后就是全局最优的方案。
算法：
该算法通用且简单：遍历数组并在每个步骤中更新：

当前元素
当前元素位置的最大和
迄今为止的最大和
在这里插入图片描述
class Solution {
  public int maxSubArray(int[] nums) {
    int n = nums.length;
    int currSum = nums[0], maxSum = nums[0];

    for(int i = 1; i < n; ++i) {
      currSum = Math.max(nums[i], currSum + nums[i]);
      maxSum = Math.max(maxSum, currSum);
    }
    return maxSum;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。只遍历一次数组。
空间复杂度：\mathcal{O}(1)O(1)，只使用了常数空间。
方法三：动态规划（Kadane 算法）
算法：

在整个数组或在固定大小的滑动窗口中找到总和或最大值或最小值的问题可以通过动态规划（DP）在线性时间内解决。
有两种标准 DP 方法适用于数组：
常数空间，沿数组移动并在原数组修改。
线性空间，首先沿 left->right 方向移动，然后再沿 right->left 方向移动。 合并结果。
我们在这里使用第一种方法，因为可以修改数组跟踪当前位置的最大和。
下一步是在知道当前位置的最大和后更新全局最大和。
在这里插入图片描述
class Solution {
  public int maxSubArray(int[] nums) {
    int n = nums.length, maxSum = nums[0];
    for(int i = 1; i < n; ++i) {
      if (nums[i - 1] > 0) nums[i] += nums[i - 1];
      maxSum = Math.max(nums[i], maxSum);
    }
    return maxSum;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。只遍历了一次数组。
空间复杂度：\mathcal{O}(1)O(1)，使用了常数的空间。
下一篇：画解算法：53. 最大子序和
© 著作权归作者所有
144
条评论

最热
精选评论(5)
miao-shou-sheng-hua妙手生花
4 个月前
做了一天题的结果就是，看了答案都写不出来了。。。

tangweiquntangweiqun_老汤
（编辑过）2 个月前
标准动态规划的代码如下：

class Solution {
    // 动态规划
    public int maxSubArray(int[] nums) {
        if (nums == null || nums.length == 0) return 0;
        int ans = 0;

        // 1. 状态定义
        // dp[i] 表示前 i 个元素的最大连续子数组的和
        int[] dp = new int[nums.length];

        // 2. 状态初始化，数组中第一个元素的最大和就是第一个元素值
        dp[0] = nums[0];
        ans = nums[0];

        // 3. 状态转移
        // 转移方程：dp[i] = max(dp[i - 1], 0) + nums[i]
        //  dp 当前元素的值等于前一个元素值和 0 的最大值再加上 nums[i]
        for (int i = 1; i < nums.length; i++) {
            dp[i] = Math.max(dp[i - 1], 0) + nums[i];
            // 更新最大和
            ans = Math.max(ans, dp[i]);
        }

        return ans;
    }
}
以上代码的时间复杂度是 O(N)，空间复杂度也是 O(N)，实际上我们可以降低空间复杂度到 O(1)。

从上面的状态转移方程 dp[i] = max(dp[i - 1], 0) + nums[i] 看出，当前的状态的值只取决于前一个状态值，所以我们可以使用一个变量来代替 dp[i] 和 dp[i - 1]，如下代码：

class Solution {
    // 动态规划
    public int maxSubArray(int[] nums) {
        if (nums == null || nums.length == 0) return 0;
        int ans = 0;

        // 使用 currSum 代替 dp[i]
        int currSum = nums[0];
        ans = nums[0];

        for (int i = 1; i < nums.length; i++) {
            currSum = Math.max(currSum, 0) + nums[i];
            // 更新最大和
            ans = Math.max(ans, currSum);
        }

        return ans;
    }
}
以上代码的时间复杂度是 O(N)，空间复杂度也是 O(1)

public class Solution {
    public int MaxSubArray(int[] nums) {
            var result = nums[0];
            var sum = 0;
            foreach (var num in nums)
            {
                if (sum > 0)
                {
                    sum += num;
                }
                else
                {
                    sum = num;
                }

                result = Math.Max(result, sum);
            }

            return result;
    }
}

public class Solution {
    public int MaxSubArray(int[] nums) {
        //常规写法
        //记录最大和、本次可以“接受”的和（本次和）
        var max = int.MinValue;
        var thissum = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            //如果和小于等于0，那么当前值没有用，应当舍去
            //（为了优化写法，其实是上一次循环时的和）
            //即重置本次和为当前值
            if (thissum <= 0)
            {
                thissum = nums[i];
            }
            else
            {
                //否则，迭加和
                thissum += nums[i];
            }
            //用max记录更大的值
            //每次遍历都要比较记录当前的最大子序和
            if (max < thissum)
                max = thissum;
        }
        return max;

    }
} 
     
*/
