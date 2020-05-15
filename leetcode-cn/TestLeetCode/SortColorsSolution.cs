/*
给定一个包含红色、白色和蓝色，一共 n 个元素的数组，原地对它们进行排序，使得相同颜色的元素相邻，并按照红色、白色、蓝色顺序排列。

此题中，我们使用整数 0、 1 和 2 分别表示红色、白色和蓝色。

注意:
不能使用代码库中的排序函数来解决这道题。

示例:

输入: [2,0,2,1,1,0]
输出: [0,0,1,1,2,2]
进阶：

一个直观的解决方案是使用计数排序的两趟扫描算法。
首先，迭代计算出0、1 和 2 元素的个数，然后按照0、1、2的排序，重写当前数组。
你能想出一个仅使用常数空间的一趟扫描算法吗？

*/

/// <summary>
/// https://leetcode-cn.com/problems/sort-colors/
/// 75.颜色分类
///
/// </summary>
internal class SortColorsSolution
{
    public void Test()
    {
        int[] nums = new int[] { 2, 0, 1 };
        SortColors(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void SortColors(int[] nums)
    {
        int redReadWriteIndex = 0;
        int index = 0;
        int blueReadWriteIndex = nums.Length - 1;

        int v;
        while (index <= blueReadWriteIndex)
        {
            if (nums[index] == 0)
            {
                v = nums[redReadWriteIndex];
                nums[redReadWriteIndex++] = nums[index];
                nums[index++] = v;
                continue;
            }
            if (nums[index] == 2)
            {
                v = nums[index];
                nums[index] = nums[blueReadWriteIndex];
                nums[blueReadWriteIndex--] = v;
                continue;
            }
            index++;
        }
    }

    //public void SortColors(int[] nums)
    //{
    //    if (nums == null || nums.Length < 2) return;

    //    int leftWriteIndex = -1,
    //        rightWriteIndex = nums.Length,
    //        leftReadIndex = 0,
    //        rightReadIndex = nums.Length - 1,
    //        length = nums.Length;

    //    int whiteColorCount = 0;
    //    int writeCount = 0;
    //    var v = nums[leftReadIndex++];
    //    do
    //    {
    //        if (v == 0)
    //        {
    //            nums[++leftWriteIndex] = 0;
    //            writeCount++;
    //            if (writeCount == length) break;
    //            v = nums[leftReadIndex++];
    //        }
    //        else if (v == 1)
    //        {
    //            whiteColorCount++;
    //            writeCount++;
    //            if (writeCount == length) break;
    //            v = nums[leftReadIndex++];
    //        }
    //        else if (v == 2)
    //        {
    //            writeCount++;
    //            if (writeCount < length)
    //            {
    //                v = nums[rightReadIndex--];
    //                nums[--rightWriteIndex] = 2;
    //            }
    //            else if (writeCount == length)
    //            {
    //                nums[--rightWriteIndex] = 2;
    //                break;
    //            }
    //        }
    //        else break;
    //    } while (true);

    //    if (0 < whiteColorCount)
    //    {
    //        int startIndex = leftWriteIndex + 1;
    //        int endIndex = startIndex + whiteColorCount - 1;
    //        for (int i = startIndex; i <= endIndex; i++) nums[i] = 1;
    //    }
    //}
}

/*

颜色分类
力扣 (LeetCode)
发布于 1 年前
46.7k
方法一: 一次遍历
直觉

本问题被称为 荷兰国旗问题
，最初由 Edsger W. Dijkstra提出。
其主要思想是给每个数字设定一种颜色，并按照荷兰国旗颜色的顺序进行调整。

image.png

我们用三个指针（p0, p2 和curr）来分别追踪0的最右边界，2的最左边界和当前考虑的元素。

image.png

本解法的思路是沿着数组移动 curr 指针，若nums[curr] = 0，则将其与 nums[p0]互换；若 nums[curr] = 2 ，则与 nums[p2]互换。

算法

初始化0的最右边界：p0 = 0。在整个算法执行过程中 nums[idx < p0] = 0.

初始化2的最左边界 ：p2 = n - 1。在整个算法执行过程中 nums[idx > p2] = 2.

初始化当前考虑的元素序号 ：curr = 0.

While curr <= p2 :

若 nums[curr] = 0 ：交换第 curr个 和 第p0个 元素，并将指针都向右移。

若 nums[curr] = 2 ：交换第 curr个和第 p2个元素，并将 p2指针左移 。

若 nums[curr] = 1 ：将指针curr右移。

实现



class Solution {
  /*
  荷兰三色旗问题解
  */
  public void sortColors(int[] nums) {
    // 对于所有 idx < i : nums[idx < i] = 0
    // j是当前考虑元素的下标
    int p0 = 0, curr = 0;
    // 对于所有 idx > k : nums[idx > k] = 2
    int p2 = nums.length - 1;

    int tmp;
    while (curr <= p2) {
      if (nums[curr] == 0) {
        // 交换第 p0个和第curr个元素
        // i++，j++
        tmp = nums[p0];
        nums[p0++] = nums[curr];
        nums[curr++] = tmp;
      }
      else if (nums[curr] == 2) {
        // 交换第k个和第curr个元素
        // p2--
        tmp = nums[curr];
        nums[curr] = nums[p2];
        nums[p2--] = tmp;
      }
      else curr++;
    }
  }
}
复杂度分析

时间复杂度 :由于对长度 NN的数组进行了一次遍历，时间复杂度为O(N)O(N) 。

空间复杂度 :由于只使用了常数空间，空间复杂度为O(1)O(1) 。

public class Solution {
    public void SortColors(int[] nums) {
        int left = 0,current =0,right = nums.Length -1;
        for(;current <= right;){
            if(nums[current] == 0){
                swap(current,left,nums);
                ++left;++current;
                continue;
            }
            if(nums[current] == 1){
                ++current;continue;
            }

            if(nums[current] == 2){
                swap(current,right,nums);
                
                --right;continue;
            }
        }
    }

    public void swap(int a, int b, int[] nums){
        int c = nums[a];
        nums[a] = nums[b];
        nums[b] = c;
    }
}


*/
