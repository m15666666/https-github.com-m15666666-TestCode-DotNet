using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组和一个整数 k，你需要找到该数组中和为 k 的连续的子数组的个数。

示例 1 :

输入:nums = [1,1,1], k = 2
输出: 2 , [1,1] 与 [1,1] 为两种不同的情况。
说明 :

数组的长度为 [1, 20,000]。
数组中元素的范围是 [-1000, 1000] ，且整数 k 的范围是 [-1e7, 1e7]。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/subarray-sum-equals-k/
/// 560. 和为K的子数组
/// https://blog.csdn.net/familyshizhouna/article/details/83273481
/// </summary>
class SubarraySumEqualsKSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SubarraySum(int[] nums, int k)
    {
        Dictionary<int, int> sum2count = new Dictionary<int, int>(128);

        int ret = 0;
        int sum = 0;
        int key;
        sum2count.Add(0, 1);
        foreach ( var v in nums)
        {
            sum += v;

            key = sum - k;
            if (sum2count.ContainsKey(key)) ret += sum2count[key];

            if (!sum2count.ContainsKey(sum)) sum2count.Add(sum, 1);
            else ++sum2count[sum];
        }
        return ret;
    }
}
/*
public class Solution {
    public int SubarraySum(int[] nums, int k) {
        Dictionary<int, int> map = new Dictionary<int, int>();
        int sum = 0, result = 0;
        map.Add(0,1);
        for (int i = 0; i < nums.Length; i++)
        {
            sum+=nums[i];
            var temp = sum - k;
            if (map.ContainsKey(temp)) result += map[temp];
            if (map.ContainsKey(sum)) map[sum]++;
            else map.Add(sum, 1);
        }
        return result;
    }
}
public class Solution {

    public int SubarraySum(int[] nums, int k) {
      Dictionary<int, int> sumFrequency = new Dictionary<int, int>();     //Key:元素和,Value:元素和出现次数
        sumFrequency.Add(0, 1);
        
        int sum = 0, count = 0;
        for(int i = 0; i < nums.Length; ++i) {
            sum += nums[i];
            
            if(sumFrequency.ContainsKey(sum - k)) {
                count += sumFrequency[sum - k];
            }
            
            if(sumFrequency.ContainsKey(sum)) {
                sumFrequency[sum] += 1;
            } else {
                sumFrequency[sum] = 1;
            }
        }

        return count;
    }
}

*/
