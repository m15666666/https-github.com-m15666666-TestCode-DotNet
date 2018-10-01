using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array/description/
/// 搜索旋转排序数组
/// 假设按照升序排序的数组在预先未知的某个点上进行了旋转。
/// </summary>
class SearchMidReversedArraySolution
{
    public static void Test()
    {
        var ret = Search(new int[] { 3, 4, 5, 6, 1, 2 }, 2);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return -1;
        if (nums.Length == 1) return nums[0] == target ? 0 : -1;
        if (nums[0] == target) return 0;
        if (nums.Length == 2)
        {
            return nums[1] == target ? 1 : -1;
        }

        int startIndex = 0;
        int stopIndex = nums.Length - 1;

        var reverseIndex = FindMidIndex(nums);
        if ( reverseIndex != -1 )
        {
            int firstV = nums[0];

            if (firstV < target) stopIndex = reverseIndex - 1;
            else startIndex = reverseIndex;
        }

        while ( startIndex <= stopIndex )
        {
            int midIndex = (startIndex + stopIndex) / 2;
            var midV = nums[midIndex];

            if (midV == target) return midIndex;

            if ( startIndex == midIndex || stopIndex == midIndex)
            {
                if (nums[startIndex] == target) return startIndex;
                if (nums[stopIndex] == target) return stopIndex;
                return -1;
            }

            if (midV < target) startIndex = midIndex + 1;
            else stopIndex = midIndex - 1;
        }

        return -1;
    }

    private static int FindMidIndex(int[] nums)
    {
        int firstV = nums[0];
        if (firstV < nums[nums.Length - 1]) return -1;

        int startIndex = 0;
        int stopIndex = nums.Length - 1;

        while (true)
        {
            int midIndex = (startIndex + stopIndex) / 2;
            var midV = nums[midIndex];

            if (startIndex == midIndex || stopIndex == midIndex)
            {
                return nums[startIndex] < nums[stopIndex] ? startIndex : stopIndex;
            }

            if ( firstV < midV)
            {
                if( nums[midIndex + 1] < midV ) return midIndex + 1;

                startIndex = midIndex + 1;
            }
            else
            {
                if (midV < nums[midIndex - 1]) return midIndex;

                stopIndex = midIndex - 1;
            }
        }

        return -1;
    }
}