using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array-ii/
/// 81.搜索旋转排序数组II
/// 假设按照升序排序的数组在预先未知的某个点上进行了旋转。
/// ( 例如，数组[0, 0, 1, 2, 2, 5, 6] 可能变为[2, 5, 6, 0, 0, 1, 2] )。
/// 编写一个函数来判断给定的目标值是否存在于数组中。若存在返回 true，否则返回 false。
/// </summary>
class SearchMidReversedArray2Solution
{
    public static void Test()
    {
        //int[] nums = new int[] {1,1,3,1};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static bool Search(int[] nums, int target)
    {
        return SearchIndex(nums, target) != -1;
    }

    private static int SearchIndex(int[] nums, int target)
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

        var reverseIndex = FindMidIndex(nums, ref startIndex, ref stopIndex );
        if (reverseIndex != -1 )
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

            if (startIndex == midIndex || stopIndex == midIndex)
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

    private static int FindMidIndex(int[] nums, ref int startIndex0, ref int stopIndex0  )
    {
        int startIndex = startIndex0;
        int firstV = nums[startIndex];
        
        while (firstV == nums[stopIndex0] && startIndex0 < stopIndex0) stopIndex0--;

        int stopIndex = stopIndex0;
        int lastV = nums[stopIndex];
        if (firstV < lastV) return -1;

        while ( startIndex <= stopIndex )
        {
            int midIndex = (startIndex + stopIndex) / 2;
            var midV = nums[midIndex];

            if (startIndex == midIndex || stopIndex == midIndex)
            {
                return nums[startIndex] < nums[stopIndex] ? startIndex : stopIndex;
            }

            if (firstV <= midV)
            {
                if ((midIndex + 1 <= stopIndex0) && (nums[midIndex + 1] < midV)) return midIndex + 1;
                startIndex = midIndex + 1;
            }
            else
            {
                if ((startIndex0 <= midIndex - 1) && (midV < nums[midIndex - 1])) return midIndex;
                stopIndex = midIndex - 1;
            }
        }

        return -1;
    }
}