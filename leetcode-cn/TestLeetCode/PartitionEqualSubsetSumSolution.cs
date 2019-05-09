using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个只包含正整数的非空数组。是否可以将这个数组分割成两个子集，使得两个子集的元素和相等。

注意:

每个数组中的元素不会超过 100
数组的大小不会超过 200
示例 1:

输入: [1, 5, 11, 5]

输出: true

解释: 数组可以分割成 [1, 5, 5] 和 [11].
 

示例 2:

输入: [1, 2, 3, 5]

输出: false

解释: 数组不能分割成两个元素和相等的子集. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-equal-subset-sum/
/// 416. 分割等和子集
/// https://blog.csdn.net/abc15766228491/article/details/83116703
/// </summary>
class PartitionEqualSubsetSumSolution
{
    public void Test()
    {
        var ret = CanPartition(new int[] { 2, 2, 3, 5 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanPartition(int[] nums)
    {
        Array.Sort(nums);
        var sum = nums.Sum();
        if (sum % 2 != 0) return false;
        var half = sum / 2;
        bool[] dp = new bool[half + 1];
        Array.Fill(dp, false);
        dp[0] = true;
        int highIndex = 0;
        foreach ( var n in nums)
        {
            var upper = Math.Min(highIndex, half);
            for( int i = upper; -1 < i; i--)
            {
                var index = i + n;
                if (half < index) continue;
                if (dp[i] && !dp[index]) {
                    dp[index] = true;
                    if (highIndex < index) highIndex = index;
                }
            }
            if (dp[half]) return true;
        }
        return false;
    }
}
/*
public class Solution {
    public bool CanPartition(int[] nums) {
        int key = nums.Sum();
        if ( key % 2 == 1) return false;
        key /= 2;
        int[] dp = new int[key + 1];
        for(int i = 0; i < nums.Length; i++) {
            for(int j = key; j >= nums[i]; j--) {
                dp[j] = Math.Max(dp[j], dp[j-nums[i]] + nums[i]);
            }
        }
        // Console.WriteLine(dp[key]);
        return dp[key] == key;
    }
}
public class Solution {
    //思路：动态规划-首先对nums所有元素求和，得到sum。若sum为奇数，则肯定无法平分为等和子集；若为偶数，则构建数组dp，其中dp[i]表示nums内是否存在和为i的子集。状态转移方程为dp[j]=dp[j] || dp[j - nums[i]]
    public bool CanPartition(int[] nums) {
        int sum = 0;
        
        for(int i = 0; i < nums.Length; ++i) {
            sum += nums[i];
        }
        
        //如果是奇数肯定无法平分为等和子集
        if(sum % 2 == 1) {
            return false;
        }
        
        sum /= 2;
        //dp[i]表示数组内是否有和为i的子集
        bool[] dp = new bool[sum + 1];
        
        //初始化dp
        for(int i = 0; i <= sum; ++i) {
            dp[i] = nums[0] == i;
        }
        
        //依次检验nums中的每个元素
        for(int i = 1; i < nums.Length; ++i) {
            for(int j = sum; j >= nums[i]; --j) {
                dp[j] = dp[j] || dp[j - nums[i]];
            }
        }
        
        return dp[sum];
    }
}
public class Solution {
    public bool CanPartition(int[] nums) {
        int sum = 0;
        
        for(int i = 0; i < nums.Length; ++i) {
            sum += nums[i];
        }
        
        //如果是奇数肯定无法平分为等和子集
        if(sum % 2 == 1) {
            return false;
        }
        
        sum /= 2;
        //dp[i]表示数组内是否有和为i的子集
        bool[] dp = new bool[sum + 1];
        
        //初始化dp
        for(int i = 0; i <= sum; ++i) {
            dp[i] = nums[0] == i;
        }
        
        for(int i = 1; i < nums.Length; ++i) {
            for(int j = sum; j >= nums[i]; --j) {
                dp[j] = dp[j] || dp[j - nums[i]];
            }
        }
        
        return dp[sum];
    }
}
*/
