using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个包含非负数的数组和一个目标整数 k，编写一个函数来判断该数组是否含有连续的子数组，其大小至少为 2，总和为 k 的倍数，即总和为 n*k，其中 n 也是一个整数。

示例 1:

输入: [23,2,4,6,7], k = 6
输出: True
解释: [2,4] 是一个大小为 2 的子数组，并且和为 6。
示例 2:

输入: [23,2,6,4,7], k = 6
输出: True
解释: [23,2,6,4,7]是大小为 5 的子数组，并且和为 42。
说明:

数组的长度不会超过10,000。
你可以认为所有数字总和在 32 位有符号整数范围内。
*/
/// <summary>
/// https://leetcode-cn.com/problems/continuous-subarray-sum/
/// 523. 连续的子数组和
/// https://www.jianshu.com/p/7518acde8051
/// </summary>
class ContinuousSubArraySumSolution
{
    public void Test()
    {
        int[] nums = new int[] { 0,1,0 };
        int k = 0;
        var ret = CheckSubarraySum(nums, k);
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CheckSubarraySum(int[] nums, int k)
    {
        if (nums == null || nums.Length < 2) return false;
        int n = nums.Length;

        Dictionary<int, int> map = new Dictionary<int, int>(n);
        map.Add(0, -1);
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += nums[i];
            if (k != 0)
            {
                sum %= k;
            }
            
            if (map.ContainsKey(sum))
            {
                var preIndex = map[sum];
                if (i - preIndex > 1) return true;
            }
            else
            {
                map.Add(sum, i);
            }
        }
        return false;
    }
}
/*
public class Solution {
    public bool CheckSubarraySum(int[] nums, int k) {
         int[] sums = new int[nums.Length+1];
        sums[0] = 0;
        for (int i = 1; i <= nums.Length; i++) {
            sums[i] = sums[i-1] + nums[i-1];
        }

        for (int i = 0; i < nums.Length; i++) {
            for (int j = i+1; j < nums.Length; j++) { // [i,j]
                if ((k != 0 && (sums[j+1]-sums[i]) % k == 0) || (k == 0 && (sums[j+1]-sums[i] == 0))) return true;
            }
        }
        return false;

    }
}
public class Solution {
    public bool CheckSubarraySum(int[] nums, int k) {
        if(nums.Length<2)return false;
        int lengthR=nums.Length;
        int [,]dp=new int[lengthR+1,lengthR+1];
        for(int i=1;i<=lengthR;i++)
        {
            for(int j=0;j<lengthR-i+1;j++)
            {
                if(i==1){dp[i,j]=nums[j];}
                else{dp[i,j]=dp[i-1,j]+nums[j+i-1];}
                if(k!=0){if(dp[i,j]%k==0&&i>=2)return true;}
                else{if(dp[i,j]==0&&i>=2)return true;}  
            }
        }
        return false;
    }
}
*/
