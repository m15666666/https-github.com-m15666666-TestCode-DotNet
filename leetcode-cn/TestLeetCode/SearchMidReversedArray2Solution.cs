using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/*
假设按照升序排序的数组在预先未知的某个点上进行了旋转。

( 例如，数组 [0,0,1,2,2,5,6] 可能变为 [2,5,6,0,0,1,2] )。

编写一个函数来判断给定的目标值是否存在于数组中。若存在返回 true，否则返回 false。

示例 1:

输入: nums = [2,5,6,0,0,1,2], target = 0
输出: true
示例 2:

输入: nums = [2,5,6,0,0,1,2], target = 3
输出: false
进阶:

这是 搜索旋转排序数组 的延伸题目，本题中的 nums  可能包含重复元素。
这会影响到程序的时间复杂度吗？会有怎样的影响，为什么？
     
*/
/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array-ii/
/// 81.搜索旋转排序数组II
/// 
/// 
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
        if (nums == null || nums.Length == 0) return false;
        if (nums.Length == 1) return nums[0] == target;
        if (nums[0] == target) return true;
        if (nums.Length == 2) return nums[1] == target;

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right && nums[left] == nums[right]) right--;

        while (left <= right) 
        {
            int mid = left + (right - left) / 2;
            int midV = nums[mid];
            if (midV == target) return true;

            while(left <= mid && nums[left] == midV) left++;
            if (mid < left) continue;

            // 前半部分有序
            if (nums[left] < midV) 
            {
                if (target < midV && nums[left] <= target)  right = mid - 1; 
                else left = mid + 1;
                continue;
            } 
            
            // 后半部分有序
            if (midV < target && target <= nums[right]) left = mid + 1;
            else right = mid - 1;
        }
        return false;
    }

    //public static bool Search(int[] nums, int target)
    //{
    //    return SearchIndex(nums, target) != -1;
    //}

    //private static int SearchIndex(int[] nums, int target)
    //{
    //    if (nums == null || nums.Length == 0) return -1;
    //    if (nums.Length == 1) return nums[0] == target ? 0 : -1;
    //    if (nums[0] == target) return 0;
    //    if (nums.Length == 2)
    //    {
    //        return nums[1] == target ? 1 : -1;
    //    }

    //    int startIndex = 0;
    //    int stopIndex = nums.Length - 1;

    //    var reverseIndex = FindMidIndex(nums, ref startIndex, ref stopIndex );
    //    if (reverseIndex != -1 )
    //    {
    //        int firstV = nums[0];

    //        if (firstV < target) stopIndex = reverseIndex - 1;
    //        else startIndex = reverseIndex;
    //    }

    //    while ( startIndex <= stopIndex )
    //    {
    //        int midIndex = (startIndex + stopIndex) / 2;
    //        var midV = nums[midIndex];

    //        if (midV == target) return midIndex;

    //        if (startIndex == midIndex || stopIndex == midIndex)
    //        {
    //            if (nums[startIndex] == target) return startIndex;
    //            if (nums[stopIndex] == target) return stopIndex;
    //            return -1;
    //        }

    //        if (midV < target) startIndex = midIndex + 1;
    //        else stopIndex = midIndex - 1;
    //    }

    //    return -1;
    //}

    //private static int FindMidIndex(int[] nums, ref int startIndex0, ref int stopIndex0  )
    //{
    //    int startIndex = startIndex0;
    //    int firstV = nums[startIndex];
        
    //    while (firstV == nums[stopIndex0] && startIndex0 < stopIndex0) stopIndex0--;

    //    int stopIndex = stopIndex0;
    //    int lastV = nums[stopIndex];
    //    if (firstV < lastV) return -1;

    //    while ( startIndex <= stopIndex )
    //    {
    //        int midIndex = (startIndex + stopIndex) / 2;
    //        var midV = nums[midIndex];

    //        if (startIndex == midIndex || stopIndex == midIndex)
    //        {
    //            return nums[startIndex] < nums[stopIndex] ? startIndex : stopIndex;
    //        }

    //        if (firstV <= midV)
    //        {
    //            if ((midIndex + 1 <= stopIndex0) && (nums[midIndex + 1] < midV)) return midIndex + 1;
    //            startIndex = midIndex + 1;
    //        }
    //        else
    //        {
    //            if ((startIndex0 <= midIndex - 1) && (midV < nums[midIndex - 1])) return midIndex;
    //            stopIndex = midIndex - 1;
    //        }
    //    }

    //    return -1;
    //}
}
/*

搜索旋转排序数组 II
蛋炒饭
发布于 9 个月前
13.7k
解题思路：
本题是需要使用二分查找，怎么分是关键，举个例子：

第一类
1011110111 和 1110111101 这种。此种情况下 nums[start] == nums[mid]，分不清到底是前面有序还是后面有序，此时 start++ 即可。相当于去掉一个重复的干扰项。
第二类
22 33 44 55 66 77 11 这种，也就是 nums[start] < nums[mid]。此例子中就是 2 < 5；
这种情况下，前半部分有序。因此如果 nums[start] <=target<nums[mid]，则在前半部分找，否则去后半部分找。
第三类
66 77 11 22 33 44 55 这种，也就是 nums[start] > nums[mid]。此例子中就是 6 > 2；
这种情况下，后半部分有序。因此如果 nums[mid] <target<=nums[end]。则在后半部分找，否则去前半部分找。
代码:
public boolean search(int[] nums, int target) {
        if (nums == null || nums.length == 0) {
            return false;
        }
        int start = 0;
        int end = nums.length - 1;
        int mid;
        while (start <= end) {
            mid = start + (end - start) / 2;
            if (nums[mid] == target) {
                return true;
            }
            if (nums[start] == nums[mid]) {
                start++;
                continue;
            }
            //前半部分有序
            if (nums[start] < nums[mid]) {
                //target在前半部分
                if (nums[mid] > target && nums[start] <= target) {
                    end = mid - 1;
                } else {  //否则，去后半部分找
                    start = mid + 1;
                }
            } else {
                //后半部分有序
                //target在后半部分
                if (nums[mid] < target && nums[end] >= target) {
                    start = mid + 1;
                } else {  //否则，去后半部分找
                    end = mid - 1;

                }
            }
        }
        //一直没找到，返回false
        return false;

    }
下一篇：Python 二分查找

public class Solution {
    public bool Search(int[] nums, int target) {
        if(nums==null||nums.Length==0) return false;
         int left=0;
         int right=nums.Length-1;
         while(left<=right)
         {
             int mid=(left+right+1)/2;
             if(nums[mid]==target)
              return true;
            //此种情况，让长度压缩一位！！！！
             if(nums[right]==nums[mid])
              {
                  right--;
                  continue;
              }
            if(nums[mid]>=nums[left])
            {
                if(target>=nums[left]&&target<=nums[mid])
                 right=mid-1;
                 else
                 left=mid+1;
            }
            else
            {
                if(target>=nums[mid]&&target<=nums[right])
                  left=mid+1;
                else 
                 right=mid-1;
            }
         }
         return false;
    }
}

public class Solution {
    public bool Search(int[] nums, int target) {
            if (nums.Length == 0) return false;
            int start = 0;
            int end = nums.Length - 1;
            while (start <= end)
            {
                int mid = (start + end) / 2;
                if (nums[mid] == target)
                {
                    return true;
                }
                else if (nums[start] == nums[mid] && nums[end] == nums[mid])
                {
                    start++;
                    end--;
                }
                else if (nums[start] > nums[mid])
                {
                    if (target < nums[start] && nums[mid] < target)
                    {
                        start = mid + 1;
                    }
                    else //if (target < nums[start] && nums[mid] > target || target >= nums[start])
                    {
                        end = mid - 1;
                    }
                }
                else if (nums[start] <= nums[mid])
                {
                    if (target < nums[start] || target >= nums[start] && nums[mid] < target)
                    {
                        start = mid + 1;
                    }
                    else //if (target >= nums[start] && nums[mid] > target)
                    {
                        end = mid - 1;
                    }
                }
            }
            return false;
    }
}

public class Solution
{
    public bool Search(int[] nums, int target)
    {
        int n = nums.Length;
        int l = 0, r = n - 1;
        while (l <= r)
        {
            int mid = (l + r) / 2;
            if (nums[mid] == target) return true;
            while (l < mid && nums[l] == nums[mid]) l++;
            while (r > mid && nums[r] == nums[mid]) r--;
            if (nums[l] <= nums[mid])
            {
                if (nums[l] <= target && target < nums[mid])
                    r = mid - 1;
                else
                    l = mid + 1;
            }
            else
            {
                if (nums[mid] < target && target <= nums[r])
                    l = mid + 1;
                else
                    r = mid - 1;
            }
        }
        return false;
    }
}

 
     
     
     
*/