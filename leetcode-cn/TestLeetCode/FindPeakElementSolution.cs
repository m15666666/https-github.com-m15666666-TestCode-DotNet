using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/find-peak-element/
/// 162. 寻找峰值
/// 峰值元素是指其值大于左右相邻值的元素。
/// 给定一个输入数组 nums，其中 nums[i] ≠ nums[i + 1]，找到峰值元素并返回其索引。
/// 数组可能包含多个峰值，在这种情况下，返回任何一个峰值所在位置即可。
/// 你可以假设 nums[-1] = nums[n] = -∞。
/// 示例 1:
/// 输入: nums = [1,2,3,1]
/// 输出: 2
/// 解释: 3 是峰值元素，你的函数应该返回其索引 2。
/// 示例 2:
/// 输入: nums = [1,2,1,3,5,6,4]
/// 输出: 1 或 5 
/// 解释: 你的函数可以返回索引 1，其峰值元素为 2；
/// 或者返回索引 5， 其峰值元素为 6。
/// </summary>
class FindPeakElementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int FindPeakElement(int[] nums)
    {
        if (nums == null || nums.Length == 0) return -1;
        if (nums.Length == 1) return 0;

        if (nums[1] < nums[0]) return 0;

        var lengthMinusOne = nums.Length - 1;
        for (var index = 1; index < lengthMinusOne; index++)
            if (nums[index - 1] < nums[index] && nums[index + 1] < nums[index]) return index;

        if (nums[lengthMinusOne-1] < nums[lengthMinusOne]) return lengthMinusOne;

        return -1;
    }
}
/*
 * 别人的算法1
    public int FindPeakElement(int[] nums) 
    {  int left = 0;
            int right = nums.Length - 1;
            while (left < right)
            {
                int mid = (right + left) / 2;
                if(nums[mid] > nums[mid + 1])
                {
                    right = mid; 
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
    }

    public int FindPeakElement(int[] nums) 
    { if(nums.Length==0)return 0;
        int l=0,r=nums.Length-1;
//中间元素比mid+1大，那么峰值在Mid的左边，同时中间元素可以取到峰值。如果比mid+1更小，那么峰值一定在Mid右边且取不到mid。  
        while(l<=r){      
            if(l==r)return l;         //左右相遇作为结束条件
            int mid=(l+r)/2;
            if(nums[mid]>=nums[mid+1])r=mid;        //取mid+1可以避免检测
            else l=mid+1;
        }
        return -1;
    }
*/
