using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/find-first-and-last-position-of-element-in-sorted-array/
/// 在排序数组中查找元素的第一个和最后一个位置
/// 给定一个按照升序排列的整数数组 nums，和一个目标值 target。找出给定目标值在数组中的开始位置和结束位置
/// </summary>
class SearchRangeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int[] SearchRange(int[] nums, int target)
    {
        int[] ret = new int[] { -1, -1 };
        if (nums == null || nums.Length == 0) return ret;

        if (target < nums[0] || nums[nums.Length - 1] < target) return ret;

        int startIndex = 0;
        int stopIndex = nums.Length - 1;
        int targetIndex = -1;
        while( startIndex <= stopIndex )
        {
            var midIndex = (startIndex + stopIndex) / 2;
            var midV = nums[midIndex];

            if( midV == target)
            {
                targetIndex = midIndex;
                break;
            }

            if( midV < target)
            {
                startIndex = midIndex + 1;
                continue;
            }

            stopIndex = midIndex - 1;
        }

        if (targetIndex == -1) return ret;

        for(int i = targetIndex; -1 < i; i--)
        {
            if (nums[i] == target) ret[0] = i;
            else break;
        }
        for (int i = targetIndex; i < nums.Length; i++)
        {
            if (nums[i] == target) ret[1] = i;
            else break;
        }

        return ret;
    }

}