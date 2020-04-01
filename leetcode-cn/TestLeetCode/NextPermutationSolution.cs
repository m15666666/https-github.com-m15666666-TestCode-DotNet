using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
实现获取下一个排列的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。

如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。

必须原地修改，只允许使用额外常数空间。

以下是一些例子，输入位于左侧列，其相应输出位于右侧列。
1,2,3 → 1,3,2
3,2,1 → 1,2,3
1,1,5 → 1,5,1
*/
/// <summary>
/// https://leetcode-cn.com/problems/next-permutation/
/// 31. 下一个排列
/// 
/// 
/// https://leetcode-cn.com/problems/next-permutation/solution/
/// </summary>
class NextPermutationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void NextPermutation(int[] nums)
    {
        if (nums == null || nums.Length < 2) return;

        int decreaseIndex = -1;
        for( int i = nums.Length - 1; 0 < i; i--)
        {
            if( nums[i-1] < nums[i])
            {
                decreaseIndex = i - 1;
                break;
            }
        }

        if( decreaseIndex == -1)
        {
            Array.Reverse(nums);
            return;
        }

        var length = nums.Length - 1 - decreaseIndex;
        int v = nums[decreaseIndex];
        if (length == 1)
        {
            (nums[decreaseIndex], nums[decreaseIndex + 1]) = (nums[decreaseIndex + 1], v);
            return;
        }

        int lo = decreaseIndex + 1;
        int hi = nums.Length - 1;
        int mid;
        while(lo < hi)
        {
            mid = (lo + hi) / 2;
            if (nums[mid] <= v) hi = mid - 1;
            else lo = mid + 1;
        }
        if (nums[lo] <= v) lo--;
        (nums[decreaseIndex], nums[lo]) = (nums[lo], v);
        Array.Reverse(nums, decreaseIndex + 1, nums.Length - 1 - decreaseIndex);

        // 以下是倒序，并线性查找，效率不高
        //if( 1 < length ) Array.Reverse( nums, decreaseIndex + 1, length);
        ////if( 1 < length ) Array.Sort( nums, decreaseIndex + 1, length);

        //var a = nums[decreaseIndex];
        //for( int i = decreaseIndex + 1; i < nums.Length; i++)
        //{
        //    var b = nums[i];
        //    if( a < b)
        //    {
        //        nums[i] = a;
        //        nums[decreaseIndex] = b;
        //        return;
        //    }
        //}
    }
}
/*

下一个排列
力扣 (LeetCode)
发布于 2 年前
60.1k
概要
我们需要找到给定数字列表的下一个字典排列，而不是由给定数组形成的数字。

解决方案
方法一：暴力法
算法

在这种方法中，我们找出由给定数组的元素形成的列表的每个可能的排列，并找出比给定的排列更大的排列。
但是这个方法是一种非常天真的方法，因为它要求我们找出所有可能的排列
这需要很长时间，实施起来也很复杂。
因此，这种方法根本无法通过。 所以，我们直接采用正确的方法。

复杂度分析

时间复杂度：O(n!)O(n!)，可能的排列总计有 n!n! 个。
空间复杂度：O(n)O(n)，因为数组将用于存储排列。
方法二：一遍扫描
算法

首先，我们观察到对于任何给定序列的降序，没有可能的下一个更大的排列。

例如，以下数组不可能有下一个排列：

[9, 5, 4, 3, 1]
我们需要从右边找到第一对两个连续的数字 a[i]a[i] 和 a[i-1]a[i−1]，它们满足 a[i]>a[i-1]a[i]>a[i−1]。现在，没有对 a[i-1]a[i−1] 右侧的重新排列可以创建更大的排列，因为该子数组由数字按降序组成。因此，我们需要重新排列 a[i-1]a[i−1] 右边的数字，包括它自己。

现在，什么样子的重新排列将产生下一个更大的数字呢？我们想要创建比当前更大的排列。因此，我们需要将数字 a[i-1]a[i−1] 替换为位于其右侧区域的数字中比它更大的数字，例如 a[j]a[j]。

 Next Permutation 

我们交换数字 a[i-1]a[i−1] 和 a[j]a[j]。我们现在在索引 i-1i−1 处有正确的数字。 但目前的排列仍然不是我们正在寻找的排列。我们需要通过仅使用 a[i-1]a[i−1]右边的数字来形成最小的排列。 因此，我们需要放置那些按升序排列的数字，以获得最小的排列。

但是，请记住，在从右侧扫描数字时，我们只是继续递减索引直到我们找到 a[i]a[i] 和 a[i-1]a[i−1] 这对数。其中，a[i] > a[i-1]a[i]>a[i−1]。因此，a[i-1]a[i−1] 右边的所有数字都已按降序排序。此外，交换 a[i-1]a[i−1] 和 a[j]a[j] 并未改变该顺序。因此，我们只需要反转 a[i-1]a[i−1] 之后的数字，以获得下一个最小的字典排列。

下面的动画将有助于你理解：

Next Permutation

public class Solution {
    public void nextPermutation(int[] nums) {
        int i = nums.length - 2;
        while (i >= 0 && nums[i + 1] <= nums[i]) {
            i--;
        }
        if (i >= 0) {
            int j = nums.length - 1;
            while (j >= 0 && nums[j] <= nums[i]) {
                j--;
            }
            swap(nums, i, j);
        }
        reverse(nums, i + 1);
    }

    private void reverse(int[] nums, int start) {
        int i = start, j = nums.length - 1;
        while (i < j) {
            swap(nums, i, j);
            i++;
            j--;
        }
    }

    private void swap(int[] nums, int i, int j) {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，在最坏的情况下，只需要对整个数组进行两次扫描。

空间复杂度：O(1)O(1)，没有使用额外的空间，原地替换足以做到。

两种思路，效果拔群
贾大空
发布于 5 天前
174
class Solution {
public:
    void nextPermutation(vector<int>& nums) {
        // 思路一：从右往左，右边正序排列，当前值如果大于等于右边最大值，将当前值加入右侧排列，
        //       如果小于右边最大值，则当前值和右边第一个大于当前值的位置互换。
        // 思路二：从右往左找到第一个左边小于右边的数，将左边的数与右边倒序排列的数组中大于该值最小的数进行交换，反转右侧的数组

        //思路二
        if (nums.size() < 2)
            return;

        int start = 0;
        int end = nums.size() - 1;
        int right = nums.size() - 1;
        while (right > 0) {
            if (nums[right-1] < nums[right]) {  // find the first left value is less than right
                start = right;   // change start
                int i = right;
                while (i < nums.size() && nums[right-1] < nums[i])
                    i++;
                int tmp = nums[right-1];
                nums[right-1] = nums[i-1];
                nums[i-1] = tmp;
                break;
            }
            right--;
        }

        // reverse vector from start to end.
        for (int j = 0; j <= (end-start)/2; j++) {
            int tmp = nums[start+j];
            nums[start+j]= nums[end-j];
            nums[end-j] = tmp;
        }

        //思路一
        //if (nums.size() < 2)
        //    return;
//
        //for (int i = nums.size() - 2; i >= 0; i--) {  // i: 当前值；
        //    if (nums[i] >= nums[nums.size()-1]) {  //当前值如果大于等于右边最大值，将当前值加入右侧排列， 
        //        for (int j = i; j < nums.size() - 1; j++) {
        //            int tmp = nums[j];
        //            nums[j] = nums[j+1];
        //            nums[j+1] = tmp;
        //        }
        //    } else if (nums[i] < nums[nums.size()-1]) {  //如果小于右边最大值，则当前值和右边第一个大于当前值的位置互换。
        //        for (int j = i+1; j < nums.size(); j++) {
        //            if (nums[i] < nums[j]) {
        //                int tmp = nums[i];
        //                nums[i] = nums[j];
        //                nums[j] = tmp;
        //                return;
        //            }
        //        }
        //    }
        //}
    }
};

 
public class Solution {
    public void NextPermutation(int[] nums) {
        if(nums.Length==0) return;
        int i=nums.Length-1;
        while(i>0&&nums[i]<=nums[i-1]){
            i--;
        }
        i--;
        if(i<0){
            Array.Sort(nums);
            return;
        }
        int swapindex=int.MaxValue;
        int min=int.MaxValue;
        for(int k=i+1;k<nums.Length;k++){
            if(nums[k]>nums[i]){
                if(nums[k]<=min){
                    swapindex=k;
                    min=nums[k];
                }
            }
        }
        int temp=nums[i];
        nums[i]=nums[swapindex];
        nums[swapindex]=temp;
        Array.Sort(nums,i+1,nums.Length-i-1);
    }
}

// 使用快速排序
public class Solution {
    public void NextPermutation(int[] nums) {
        if (nums.Length == 1||nums.Length == 0)
            return;
        int pos = 0;
        if (nums[nums.Length - 2] < nums[nums.Length - 1])
            exchange(ref nums, nums.Length - 2, nums.Length - 1);
        else
        {
            for (int i = nums.Length - 3; i >= 0; i--)
            {
                if (nums[i] >= nums[i + 1])
                    continue;
                else
                {
                    for (int j = i+1; j < nums.Length; j++)
                        if (nums[j]>nums[i])
                            pos = j;
                    exchange(ref nums, pos, i);
                    quick_sort(ref nums, i + 1, nums.Length - 1);
                    return;
                }
            }
            Array.Reverse(nums);
        }
    }
    public static void exchange(ref int[] nums,int a,int b)
    {
        int c = nums[a];
        nums[a] = nums[b];
        nums[b] = c;
    }
    public static int[] quick_sort(ref int[] list, int left, int right)
    {
        if (left < right)
        {
            int mid = get_partition(ref list, left, right);
            quick_sort(ref list, left, mid - 1);
            quick_sort(ref list, mid + 1, right);
        }
        return list;
    }
    private static int get_partition(ref int[] list, int left, int right)
    {
        int tmp = list[left];
        while (left < right)
        {
            while (left < right && list[right] >= tmp)
            {
                right -= 1;
            }
            list[left] = list[right];
            while (left < right && list[left] <= tmp)
            {
                left += 1;
            }
            list[right] = list[left];
        }
        list[right] = tmp;
        return right;
    }
}
*/
