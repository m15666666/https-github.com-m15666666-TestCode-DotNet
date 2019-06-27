using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个二进制数组, 找到含有相同数量的 0 和 1 的最长连续子数组（的长度）。

示例 1:
输入: [0,1]
输出: 2
说明: [0, 1] 是具有相同数量0和1的最长连续子数组。
示例 2:
输入: [0,1,0]
输出: 2
说明: [0, 1] (或 [1, 0]) 是具有相同数量0和1的最长连续子数组。

注意: 给定的二进制数组的长度不会超过50000。
*/
/// <summary>
/// https://leetcode-cn.com/problems/contiguous-array/
/// 525. 连续数组
/// https://www.cnblogs.com/geek1116/p/9389236.html
/// </summary>
class ContiguousArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMaxLength(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
            if (nums[i] == 0) nums[i] = -1;

        int ret = 0;
        int sum = 0;
        Dictionary<int, int> map = new Dictionary<int, int>();
        map.Add(0, -1);
        for (int i = 0; i < nums.Length; i++)
        {
            sum += nums[i];
            //if (sum == 0)
            //{
            //    if (ret <= i) ret = i + 1;
            //}
            if (!map.ContainsKey(sum))
            {
                map.Add(sum, i);
                continue;
            }
            int length = i - map[sum];
            if (length > ret) ret = length;
        }
        return ret;
    }
}
/*
public class Solution 
{
	public int FindMaxLength(int[] nums) 
	{
		var ar = new int[2 * nums.Length + 1];	
		for (int i = 0; i < ar.Length; i++)
		{
			ar[i] = int.MaxValue;
		}
		var sum = 0;
		var ans = 0;
		for (int i = 0; i < nums.Length; i++)
		{
			sum += (nums[i] == 1 ? 1 : -1);
			var key = nums.Length + sum;
			if (ar[key] != int.MaxValue)
			{
				ans = Math.Max(ans, i - ar[key]);
			}
			else if (sum == 0)
			{
				ans = i + 1;
			}
			else
			{
				ar[key] = i;
			}
		}
		return ans;
	}
}
public class Solution {
	public int FindMaxLength (int[] nums) {
		var dic = new Dictionary<int, int> ();
		var prefixSum = 0;
		var ans = 0;
		for (int i = 0; i < nums.Length; i++) {
			var num = nums[i] == 0 ? -1 : 1;
			prefixSum += num;
			if (dic.TryGetValue (prefixSum, out var index)) {
				ans = Math.Max (ans, i - index);
			} else {
				dic[prefixSum] = i;
			}
			if (prefixSum == 0) {
				ans = i + 1;
			}
		}
		return ans;
	}
}
public class Solution {
    public int FindMaxLength(int[] nums) 
    {
        int res = 0, n = nums.Length, sum = 0;
        Dictionary<int, int> m = new Dictionary<int, int>();
        m.Add(0, -1);
        for(int i = 0; i < n; ++i)
        {
            sum += (nums[i] == 1) ? 1 : -1;
            if(m.ContainsKey(sum))
            {
                res = Math.Max(res, i - m[sum]);
            }
            else
            {
                m[sum] = i;
            }
        }
        return res;
    }
}
*/
