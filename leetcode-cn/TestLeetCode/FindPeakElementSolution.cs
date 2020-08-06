using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/*
峰值元素是指其值大于左右相邻值的元素。

给定一个输入数组 nums，其中 nums[i] ≠ nums[i+1]，找到峰值元素并返回其索引。

数组可能包含多个峰值，在这种情况下，返回任何一个峰值所在位置即可。

你可以假设 nums[-1] = nums[n] = -∞。

示例 1:

输入: nums = [1,2,3,1]
输出: 2
解释: 3 是峰值元素，你的函数应该返回其索引 2。
示例 2:

输入: nums = [1,2,1,3,5,6,4]
输出: 1 或 5 
解释: 你的函数可以返回索引 1，其峰值元素为 2；
     或者返回索引 5， 其峰值元素为 6。
说明:

你的解法应该是 O(logN) 时间复杂度的。
 
*/
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
        int left = 0, right = nums.Length - 1;
        while (left < right) {
            int mid = (left + right) / 2;
            if (nums[mid] > nums[mid + 1]) right = mid;
            else left = mid + 1;
        }
        return left;

        //if (nums == null || nums.Length == 0) return -1;
        //if (nums.Length == 1) return 0;

        //if (nums[1] < nums[0]) return 0;

        //var lengthMinusOne = nums.Length - 1;
        //for (var index = 1; index < lengthMinusOne; index++)
        //    if (nums[index - 1] < nums[index] && nums[index + 1] < nums[index]) return index;

        //if (nums[lengthMinusOne-1] < nums[lengthMinusOne]) return lengthMinusOne;

        //return -1;
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



寻找峰值
力扣 (LeetCode)
发布于 2019-07-08
27.2k
方法一: 线性扫描
本方法利用了连续的两个元素 nums[j]nums[j] 和 nums[j + 1]nums[j+1] 不会相等这一事实。于是，我们可以从头开始遍历 numsnums 数组。每当我们遇到数字 nums[i]nums[i]，只需要检查它是否大于下一个元素 nums[i+1]nums[i+1] 即可判断 nums[i]nums[i] 是否是峰值。可以通过分别讨论问题的全部三种可能情况来理解本方法的思路。

情况 1. 所有的数字以降序排列。这种情况下，第一个元素即为峰值。我们首先检查当前元素是否大于下个元素。第一个元素满足这一条件，因此被正确判断为峰值。此时，我们不需要继续向下判断，也就不会有需要判断 nums[i]nums[i] 和上一个元素 nums[i-1]nums[i−1] 的大小的情况。

image.png

情况 2. 所有的数字以升序排列。这种情况下，我们会一直比较 nums[i]nums[i] 与 nums[i+1]nums[i+1] 以判断 nums[i]nums[i] 是否是峰值元素。没有元素符合这一条件，说明处于上坡而非峰值。于是，在结尾，我们返回末尾元素作为峰值元素，得到正确结果。在这种情况下，我们同样不需要比较 nums[i]nums[i] 和上一个元素 nums[i-1]nums[i−1]，因为处于上坡是 nums[i]nums[i] 不是峰值的充分条件。

image.png

情况 3. 峰值出现在中间某处。这种情况下，当遍历上升部分时，与情况 2 相同，没有元素满足 nums[i] > nums[i + 1]nums[i]>nums[i+1]。我们不需要比较 nums[i]nums[i] 和上一个元素 nums[i-1]nums[i−1]。当到达峰值元素时，nums[i] > nums[i + 1]nums[i]>nums[i+1] 条件满足。此时，我们同样不需要比较 nums[i]nums[i] 和上一个元素 nums[i-1]nums[i−1]。这是由于“遍历会到达第i个元素”本身就说明上一个元素（第i- 1个）不满足 nums[i] > nums[i + 1]nums[i]>nums[i+1] 这一条件，也就说明 nums[i-1] < nums[i]nums[i−1]<nums[i]。于是，我们同样可以得到正确结果。

image.png


public class Solution {
    public int findPeakElement(int[] nums) {
        for (int i = 0; i < nums.length - 1; i++) {
            if (nums[i] > nums[i + 1])
                return i;
        }
        return nums.length - 1;
    }
}
复杂度分析

时间复杂度 : O(n)O(n)。 我们对长度为 nn 的数组 numsnums 只进行一次遍历。
空间复杂度 : O(1)O(1)。 只使用了常数空间。



方法二：递归二分查找
算法

我们可以将 numsnums 数组中的任何给定序列视为交替的升序和降序序列。通过利用这一点，以及“可以返回任何一个峰作为结果”的要求，我们可以利用二分查找来找到所需的峰值元素。

在简单的二分查找中，我们处理的是一个有序数列，并通过在每一步减少搜索空间来找到所需要的数字。在本例中，我们对二分查找进行一点修改。首先从数组 numsnums 中找到中间的元素 midmid。若该元素恰好位于降序序列或者一个局部下降坡度中（通过将 nums[i]nums[i] 与右侧比较判断)，则说明峰值会在本元素的左边。于是，我们将搜索空间缩小为 midmid 的左边(包括其本身)，并在左侧子数组上重复上述过程。

若该元素恰好位于升序序列或者一个局部上升坡度中（通过将 nums[i]nums[i] 与右侧比较判断)，则说明峰值会在本元素的右边。于是，我们将搜索空间缩小为 midmid 的右边，并在右侧子数组上重复上述过程。

就这样，我们不断地缩小搜索空间，直到搜索空间中只有一个元素，该元素即为峰值元素。

为了理解本方法的原理，让我们再次讨论前文提到的全部三种情况。

情况 1. 这种情况下，首先找到中间元素 33。由于它处于下降坡度，将搜索空间缩小到 [1, 2, 3]。对于此子数组，22 为中间元素，也处于下降坡度中，于是将搜索空间缩小到 [1, 2]。现在 11 是中间元素并同样处于下降坡度，于是将搜索空间缩小到 [1]。 最终 11 作为答案被正确返回。



情况 2. 这种情况下，首先找到中间元素 33。由于它处于上升坡度，将搜索空间缩小到 [4, 5]。对于此子数组，44 为中间元素，也处于上升坡度中，于是将搜索空间缩小到 [5]。 最终 55 作为答案被正确返回。



情况 3. 这种情况下， 峰值位于中间某处。第一个中间元素是 44。它位于上升坡度，表明峰值在其右侧。于是，搜索空间缩小为 [5, 1]。 现在，55 位于下降坡度（相对其右侧相邻元素）, 搜索空间下降为 [5]。于是，55 被正确识别。




public class Solution {
    public int findPeakElement(int[] nums) {
        return search(nums, 0, nums.length - 1);
    }
    public int search(int[] nums, int l, int r) {
        if (l == r)
            return l;
        int mid = (l + r) / 2;
        if (nums[mid] > nums[mid + 1])
            return search(nums, l, mid);
        return search(nums, mid + 1, r);
    }
}
复杂度分析

时间复杂度 : O\big(log_2(n)\big)O(log 
2
​	
 (n))。每一步都将搜索空间减半。因此，总的搜索空间只需要 log_2(n)log 
2
​	
 (n) 步。其中 nn 为 numsnums 数组的长度。
空间复杂度: O\big(log_2(n)\big)O(log 
2
​	
 (n))。每一步都将搜索空间减半。因此，总的搜索空间只需要 log_2(n)log 
2
​	
 (n) 步。于是，递归树的深度为 log_2(n)log 
2
​	
 (n)。



方法三：迭代二分查找
算法

上述二分查找方法使用了递归。我们可以通过迭代达到同样的效果。本方法即为迭代实现二分查找。


public class Solution {
    public int findPeakElement(int[] nums) {
        int l = 0, r = nums.length - 1;
        while (l < r) {
            int mid = (l + r) / 2;
            if (nums[mid] > nums[mid + 1])
                r = mid;
            else
                l = mid + 1;
        }
        return l;
    }
}
复杂度分析

时间复杂度 : O\big(log_2(n)\big)O(log 
2
​	
 (n))。 每一步都将搜索空间减半。因此，总的搜索空间只需要 log_2(n)log 
2
​	
 (n) 步。其中 nn 为 numsnums 数组的长度。
空间复杂度 : O(1)O(1)。 只使用了常数空间。
下一篇：画解算法：162. 寻找峰值

public class Solution {
    public int FindPeakElement(int[] nums) {
        int l = 0, r = nums.Length - 1, mid = (l + r) / 2;
            while(l < r)
            {
                if (nums[mid] > nums[mid + 1])
                    r = mid;
                else
                    l = mid + 1;
                mid = (l + r) / 2;
            }

            return mid;
    }
}

public class Solution {
    public int FindPeakElement(int[] nums) {
        
        int left = 0;
        int right = nums.Length - 1;
        int mid = 0;
        
        while(left < right)
        {
            mid = (right - left) / 2 + left;
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
}



*/
