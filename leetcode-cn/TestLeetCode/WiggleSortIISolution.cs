using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个无序的数组 nums，将它重新排列成 nums[0] < nums[1] > nums[2] < nums[3]... 的顺序。

示例 1:

输入: nums = [1, 5, 1, 1, 6, 4]
输出: 一个可能的答案是 [1, 4, 1, 5, 1, 6]
示例 2:

输入: nums = [1, 3, 2, 2, 3, 1]
输出: 一个可能的答案是 [2, 3, 1, 3, 1, 2]
说明:
你可以假设所有输入都会得到有效的结果。

进阶:
你能用 O(n) 时间复杂度和 / 或原地 O(1) 额外空间来实现吗？ 
*/
/// <summary>
/// https://leetcode-cn.com/problems/wiggle-sort-ii/
/// 324. 摆动排序 II
/// https://blog.csdn.net/LaputaFallen/article/details/80044763
/// https://www.cnblogs.com/xidian2014/p/8832533.html
/// https://www.cnblogs.com/lightwindy/p/9771952.html
/// https://www.jianshu.com/p/d3d8992169a5
/// </summary>
class WiggleSortIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void WiggleSort(int[] nums)
    {
        if (nums == null || nums.Length < 2) return;
        Array.Sort(nums);
        int[] copy = new int[nums.Length];
        nums.CopyTo(copy, 0);
        
        int index = 1;
        for (int i = nums.Length - 1; index < nums.Length; i--)
        {
            nums[index] = copy[i];
            index += 2;
        }

        index = 0;
        for (int i = (nums.Length - 1) / 2; index < nums.Length; i--)
        {
            nums[index] = copy[i];
            index += 2;
        }
    }
}
/*
public class Solution {
    public void WiggleSort(int[] nums) {
         var arr = new int[nums.Length];
            Array.Copy(nums, arr,arr.Length);
            Array.Sort(arr);
            var i1 = nums.Length / 2 + (nums.Length & 1)-1;
            var i2 = nums.Length-1;
            for (var i = 0; i < nums.Length; i++) {
                if ((i & 1) == 0)
                {
                    nums[i] = arr[i1--];
                }
                else {
                    nums[i] = arr[i2--];
                }
            }
    }
}
public class Solution {
    public void WiggleSort(int[] nums) {
         int[] temp = (int[])nums.Clone();
            Array.Sort(temp);
            int k = (nums.Length + 1) / 2;
            int j = nums.Length;
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = i % 2 == 1 ? temp[--j] : temp[--k];
            }
    }
}
public class Solution {
    public void WiggleSort(int[] nums) {
          int[] sort = (int[])nums.Clone();
            Array.Sort(sort);
            for (int i = (sort.Length - 1) / 2, j = 0; i >= 0; i--, j += 2) nums[j] = sort[i];
            for (int i = sort.Length - 1, j = 1; i > (sort.Length - 1) / 2; i--, j += 2) nums[j] = sort[i];
    }
}
*/
