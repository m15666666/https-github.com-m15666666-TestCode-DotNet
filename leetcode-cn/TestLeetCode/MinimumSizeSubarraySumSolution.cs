using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个含有 n 个正整数的数组和一个正整数 s ，找出该数组中满足其和 ≥ s 的长度最小的 连续 子数组，并返回其长度。如果不存在符合条件的子数组，返回 0。

 

示例：

输入：s = 7, nums = [2,3,1,2,4,3]
输出：2
解释：子数组 [4,3] 是该条件下的长度最小的子数组。
 

进阶：

如果你已经完成了 O(n) 时间复杂度的解法, 请尝试 O(n log n) 时间复杂度的解法。

*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-size-subarray-sum/
/// 209. 长度最小的子数组
/// 给定一个含有 n 个正整数的数组和一个正整数 s ，找出该数组中满足其和 ≥ s 的长度最小的连续子数组。
/// 如果不存在符合条件的连续子数组，返回 0。
/// 示例: 
/// 输入: s = 7, nums = [2,3,1,2,4,3]
/// 输出: 2
/// 解释: 子数组[4, 3] 是该条件下的长度最小的连续子数组。
/// 进阶:
/// 如果你已经完成了O(n) 时间复杂度的解法, 请尝试 O(n log n) 时间复杂度的解法。
/// </summary>
class MinimumSizeSubarraySumSolution
{
    public static void Test()
    {
        var ret = MinSubArrayLen(7, new[] { 2, 3, 1, 2, 4, 3 });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int MinSubArrayLen(int s, int[] nums)
    {
        if (s < 1) return 1;
        if (nums == null || nums.Length == 0) return 0;
        int lastIndex = nums.Length - 1;
        int sum = 0;
        int minLength = int.MaxValue;
        for( int i = lastIndex; -1 < i; i--)
        {
            sum += nums[i];
            if (sum < s) continue;
            while( s <= sum && i <= lastIndex )
            {
                var len = lastIndex - i + 1;
                if (len < minLength) minLength = len;
                sum -= nums[lastIndex--];
            }
        }
        return minLength == int.MaxValue ? 0 : minLength;
    }

}
/*
长度最小的子数组
力扣官方题解
发布于 2020-06-27
29.8k
方法一：暴力法
暴力法是最直观的方法。初始化子数组的最小长度为无穷大，枚举数组 \text{nums}nums 中的每个下标作为子数组的开始下标，对于每个开始下标 ii，需要找到大于或等于 ii 的最小下标 jj，使得从 \text{nums}[i]nums[i] 到 \text{nums}[j]nums[j] 的元素和大于或等于 ss，并更新子数组的最小长度（此时子数组的长度是 j-i+1j−i+1）。

注意：使用 Python 语言实现方法一会超出时间限制。


class Solution {
    public int minSubArrayLen(int s, int[] nums) {
        int n = nums.length;
        if (n == 0) {
            return 0;
        }
        int ans = Integer.MAX_VALUE;
        for (int i = 0; i < n; i++) {
            int sum = 0;
            for (int j = i; j < n; j++) {
                sum += nums[j];
                if (sum >= s) {
                    ans = Math.min(ans, j - i + 1);
                    break;
                }
            }
        }
        return ans == Integer.MAX_VALUE ? 0 : ans;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 是数组的长度。需要遍历每个下标作为子数组的开始下标，对于每个开始下标，需要遍历其后面的下标得到长度最小的子数组。

空间复杂度：O(1)O(1)。

方法二：前缀和 + 二分查找
方法一的时间复杂度是 O(n^2)O(n 
2
 )，因为在确定每个子数组的开始下标后，找到长度最小的子数组需要 O(n)O(n) 的时间。如果使用二分查找，则可以将时间优化到 O(\log n)O(logn)。

为了使用二分查找，需要额外创建一个数组 \text{sums}sums 用于存储数组 \text{nums}nums 的前缀和，其中 \text{sums}[i]sums[i] 表示从 \text{nums}[0]nums[0] 到 \text{nums}[i-1]nums[i−1] 的元素和。得到前缀和之后，对于每个开始下标 ii，可通过二分查找得到大于或等于 ii 的最小下标 \textit{bound}bound，使得 \text{sums}[\textit{bound}]-\text{sums}[i-1] \ge ssums[bound]−sums[i−1]≥s，并更新子数组的最小长度（此时子数组的长度是 \textit{bound}-(i-1)bound−(i−1)）。

因为这道题保证了数组中每个元素都为正，所以前缀和一定是递增的，这一点保证了二分的正确性。如果题目没有说明数组中每个元素都为正，这里就不能使用二分来查找这个位置了。

在很多语言中，都有现成的库和函数来为我们实现这里二分查找大于等于某个数的第一个位置的功能，比如 C++ 的 lower_bound，Java 中的 Arrays.binarySearch，C# 中的 Array.BinarySearch，Python 中的 bisect.bisect_left。但是有时面试官可能会让我们自己实现一个这样的二分查找函数，这里给出一个 C# 的版本，供读者参考：


private int LowerBound(int[] a, int l, int r, int target) 
{
    int mid = -1, originL = l, originR = r;
    while (l < r) 
    {
        mid = (l + r) >> 1;
        if (a[mid] < target) l = mid + 1;
        else r = mid;
    } 

    return (a[l] >= target) ? l : -1;
}
下面是这道题的代码。


public class Solution {
    private int LowerBound(int[] a, int l, int r, int target) 
    {
        int mid = -1, originL = l, originR = r;
        while (l < r) 
        {
            mid = (l + r) >> 1;
            if (a[mid] < target) l = mid + 1;
            else r = mid;
        } 

        return (a[l] >= target) ? l : -1;
    }

    public int MinSubArrayLen(int s, int[] nums) 
    {
        int n = nums.Length;
        if (n == 0) 
        {
            return 0;
        }

        int ans = int.MaxValue;
        int[] sums = new int[n + 1]; 
        // 为了方便计算，令 size = n + 1 
        // sums[0] = 0 意味着前 0 个元素的前缀和为 0
        // sums[1] = A[0] 前 1 个元素的前缀和为 A[0]
        // 以此类推
        for (int i = 1; i <= n; ++i) 
        {
            sums[i] = sums[i - 1] + nums[i - 1];
        }

        for (int i = 1; i <= n; ++i) 
        {
            int target = s + sums[i - 1];
            int bound = LowerBound(sums, i, n, target);
            if (bound != -1)
            {
                ans = Math.Min(ans, bound - i + 1);
            }
        }

        return ans == int.MaxValue ? 0 : ans;
    }
}
复杂度分析

时间复杂度：O(n \log n)O(nlogn)，其中 nn 是数组的长度。需要遍历每个下标作为子数组的开始下标，遍历的时间复杂度是 O(n)O(n)，对于每个开始下标，需要通过二分查找得到长度最小的子数组，二分查找得时间复杂度是 O(\log n)O(logn)，因此总时间复杂度是 O(n \log n)O(nlogn)。

空间复杂度：O(n)O(n)，其中 nn 是数组的长度。额外创建数组 \text{sums}sums 存储前缀和。

方法三：双指针
在方法一和方法二中，都是每次确定子数组的开始下标，然后得到长度最小的子数组，因此时间复杂度较高。为了降低时间复杂度，可以使用双指针的方法。

定义两个指针 \textit{start}start 和 \textit{end}end 分别表示子数组的开始位置和结束位置，维护变量 \textit{sum}sum 存储子数组中的元素和（即从 \text{nums}[\textit{start}]nums[start] 到 \text{nums}[\textit{end}]nums[end] 的元素和）。

初始状态下，\textit{start}start 和 \textit{end}end 都指向下标 00，\textit{sum}sum 的值为 00。

每一轮迭代，将 \text{nums}[end]nums[end] 加到 \textit{sum}sum，如果 \textit{sum} \ge ssum≥s，则更新子数组的最小长度（此时子数组的长度是 \textit{end}-\textit{start}+1end−start+1），然后将 \text{nums}[start]nums[start] 从 \textit{sum}sum 中减去并将 \textit{start}start 右移，直到 \textit{sum} < ssum<s，在此过程中同样更新子数组的最小长度。在每一轮迭代的最后，将 \textit{end}end 右移。




public class Solution {
    public int MinSubArrayLen(int s, int[] nums) 
    {
        int n = nums.Length;
        if (n == 0) 
        {
            return 0;
        }

        int ans = int.MaxValue;
        int start = 0, end = 0;
        int sum = 0;
        while (end < n) 
        {
            sum += nums[end];
            while (sum >= s) 
            {
                ans = Math.Min(ans, end - start + 1);
                sum -= nums[start];
                ++start;
            }
            
            ++end;
        }

        return ans == int.MaxValue ? 0 : ans;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 是数组的长度。指针 \textit{start}start 和 \textit{end}end 最多各移动 nn 次。

空间复杂度：O(1)O(1)。 
 
 
 
*/