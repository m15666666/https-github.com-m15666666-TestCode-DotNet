using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 给定一个整数数组 nums ，找到一个具有最大和的连续子数组（子数组最少包含一个元素），返回其最大和。
/// https://blog.csdn.net/qq_18124075/article/details/80515104
/// </summary>
class MaxSubArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    /**
	* @param nums: A list of integers
	* @return: A integer indicate the sum of max subarray
	*/

    /*
		int maxSubArray(vector<int> &nums) {
		// write your code here
		// 分治思想
		int begin = 0, end = nums.size() - 1;
		return maxArray(nums, begin, end);
	}
	int maxArray(vector<int> &nums, int begin, int end){
		if (begin == end)
			return nums[begin];
		int mid = (begin + end) >> 1;
		int m1 = maxArray(nums, begin, mid);
		int m2 = maxArray(nums, mid + 1, end);
		// 从中向左地寻找最大和
		int i, left = nums[mid], now = nums[mid];
		for (i = mid-1; i >= begin; i--){
			now+= nums[i];
			left = max(now, left);
		}
		// 从中向右地寻找最大和
		int right = nums[mid + 1];
		now = nums[mid + 1];
		for (i = mid+2; i <= end; i++){
			now += nums[i];
			right = max(right, now);
		}
		int m3 = left + right;
		return max(m1, max(m2, m3));
	}
	*/
    int MaxSubArray(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        // write your code here
        // 动态规划
        int result = nums[0];
        int sum = nums[0];
        int len = nums.Length;
        for (int i = 1; i < len; i++)
        {
            if (sum > 0)
                sum += nums[i];
            else
                sum = nums[i];
            if (result < sum)
                result = sum;
        }
        return result;
    }
}