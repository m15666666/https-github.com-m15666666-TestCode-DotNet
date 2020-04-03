using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
假设按照升序排序的数组在预先未知的某个点上进行了旋转。

( 例如，数组 [0,1,2,4,5,6,7] 可能变为 [4,5,6,7,0,1,2] )。

搜索一个给定的目标值，如果数组中存在这个目标值，则返回它的索引，否则返回 -1 。

你可以假设数组中不存在重复的元素。

你的算法时间复杂度必须是 O(log n) 级别。

示例 1:

输入: nums = [4,5,6,7,0,1,2], target = 0
输出: 4
示例 2:

输入: nums = [4,5,6,7,0,1,2], target = 3
输出: -1
*/
/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array
/// 33. 搜索旋转排序数组
/// 
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
/*

搜索旋转排序数组
力扣 (LeetCode)
发布于 10 个月前
59.5k

方法：二分查找
题目要找到一种 O(logN)O(logN) 时间内的搜索方法，这提示我们可以用二分查找的方法。

算法非常直接：

找到旋转的下标 rotation_index ，也就是数组中最小的元素。二分查找在这里可以派上用场。
在选中的数组区域中再次使用二分查找。


class Solution {
  int [] nums;
  int target;

  public int find_rotate_index(int left, int right) {
    if (nums[left] < nums[right])
      return 0;

    while (left <= right) {
      int pivot = (left + right) / 2;
      if (nums[pivot] > nums[pivot + 1])
        return pivot + 1;
      else {
        if (nums[pivot] < nums[left])
          right = pivot - 1;
        else
          left = pivot + 1;
      }
    }
    return 0;
  }

  public int search(int left, int right) {
    while (left <= right) {
      int pivot = (left + right) / 2;
      if (nums[pivot] == target)
        return pivot;
      else {
        if (target<nums[pivot])
          right = pivot - 1;
        else
          left = pivot + 1;
      }
    }
    return -1;
  }

  public int search(int[] nums, int target)
{
    this.nums = nums;
    this.target = target;

    int n = nums.length;

    if (n == 0)
        return -1;
    if (n == 1)
        return this.nums[0] == target ? 0 : -1;

    int rotate_index = find_rotate_index(0, n - 1);

    // if target is the smallest element
    if (nums[rotate_index] == target)
        return rotate_index;
    // if array is not rotated, search in the entire array
    if (rotate_index == 0)
        return search(0, n - 1);
    if (target < nums[0])
        // search in the right side
        return search(rotate_index, n - 1);
    // search in the left side
    return search(0, rotate_index);
}
}
复杂度分析

时间复杂度： O(logN)O(logN) 。
空间复杂度： O(1)O(1) 。

public class Solution {
     public int Search(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }
            int left = 0;
            int right = nums.Length - 1;
            int mid;
            while(left <= right)
            {
                mid  = left + (right - left) / 2;
                if(nums[mid] == target)
                {
                    return mid;
                }
                if(nums[left] <= nums[mid])
                {
                    if(target >= nums[left] && target < nums[mid])
                    {
                        right = mid - 1;
                    }else
                    {
                        left = mid + 1;
                    }
                }else{
                    if(target > nums[mid] && target <= nums[right])
                    {
                        left = mid + 1;
                    }else{
                        right = mid - 1;
                    }
                }
            }

            return -1;
        }
}
*/
