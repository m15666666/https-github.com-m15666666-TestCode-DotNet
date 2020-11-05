/*
给定一个无序的整数数组，找到其中最长上升子序列的长度。

示例:

输入: [10,9,2,5,3,7,101,18]
输出: 4
解释: 最长的上升子序列是 [2,3,7,101]，它的长度是 4。
说明:

可能会有多种最长上升子序列的组合，你只需要输出对应的长度即可。
你算法的时间复杂度应该为 O(n2) 。
进阶: 你能将算法的时间复杂度降低到 O(n log n) 吗?
*/

using System;
/// <summary>
/// https://leetcode-cn.com/problems/longest-increasing-subsequence/
/// 300. 最长上升子序列
/// https://blog.csdn.net/OneDeveloper/article/details/80047289
/// </summary>
internal class LongestIncreasingSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LengthOfLIS(int[] nums)
    {
        ////Array.BinarySearch
        int n = nums.Length;
        if (n == 0) return 0;

        int currentMaxLen = 1;
        int[] minValueOfILen = new int[n + 1];
        minValueOfILen[currentMaxLen] = nums[0];
        for (int i = 1; i < n; ++i)
        {
            var v = nums[i];
            if ( minValueOfILen[currentMaxLen] <= v)
            {
                if ( minValueOfILen[currentMaxLen] < v) minValueOfILen[++currentMaxLen] = v;
                continue;
            }
            if (v <= minValueOfILen[1])
            {
                if (v < minValueOfILen[1]) minValueOfILen[1] = v;
                continue;
            }

            int left = 1, right = currentMaxLen, pos = -1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if(minValueOfILen[mid] == v)
                {
                    pos = -1;
                    break;
                }

                if (minValueOfILen[mid] < v) pos = left = mid + 1;
                else right = mid - 1;
            }
            if(pos != -1)minValueOfILen[pos] = v;
        }
        return currentMaxLen;
    }

    //public int LengthOfLIS(int[] nums)
    //{
    //    if (nums == null || nums.Length == 0) return 0;
    //    int[] dp = new int[nums.Length];
    //    dp[0] = 1;
    //    int result = 1;
    //    for (int i = 1; i < nums.Length; i++)
    //    {
    //        int max = 0;
    //        for (int j = i - 1; j >= 0; j--)
    //            if (nums[j] < nums[i] && max < dp[j]) max = dp[j];

    //        dp[i] = max + 1;
    //        if (result < dp[i]) result = dp[i];
    //    }
    //    return result;
    //}
}

/*
最长上升子序列
力扣官方题解
发布于 2020-03-13
113.4k
方法一：动态规划
思路与算法

定义 dp[i]dp[i] 为考虑前 ii 个元素，以第 ii 个数字结尾的最长上升子序列的长度，注意 \textit{nums}[i]nums[i] 必须被选取。

我们从小到大计算 dp[]dp[] 数组的值，在计算 dp[i]dp[i] 之前，我们已经计算出 dp[0 \ldots i-1]dp[0…i−1] 的值，则状态转移方程为：

dp[i] = \text{max}(dp[j]) + 1, \text{其中} \, 0 \leq j < i \, \text{且} \, \textit{num}[j]<\textit{num}[i]
dp[i]=max(dp[j])+1,其中0≤j<i且num[j]<num[i]

即考虑往 dp[0 \ldots i-1]dp[0…i−1] 中最长的上升子序列后面再加一个 \textit{nums}[i]nums[i]。由于 dp[j]dp[j] 代表 \textit{nums}[0 \ldots j]nums[0…j] 中以 \textit{nums}[j]nums[j] 结尾的最长上升子序列，所以如果能从 dp[j]dp[j] 这个状态转移过来，那么 \textit{nums}[i]nums[i] 必然要大于 \textit{nums}[j]nums[j]，才能将 \textit{nums}[i]nums[i] 放在 \textit{nums}[j]nums[j] 后面以形成更长的上升子序列。

最后，整个数组的最长上升子序列即所有 dp[i]dp[i] 中的最大值。

\text{LIS}_{\textit{length}}= \text{max}(dp[i]), \text{其中} \, 0\leq i < n
LIS 
length
​	
 =max(dp[i]),其中0≤i<n

以下动画演示了该方法：




class Solution {
    public int lengthOfLIS(int[] nums) {
        if (nums.length == 0) {
            return 0;
        }
        int[] dp = new int[nums.length];
        dp[0] = 1;
        int maxans = 1;
        for (int i = 1; i < nums.length; i++) {
            dp[i] = 1;
            for (int j = 0; j < i; j++) {
                if (nums[i] > nums[j]) {
                    dp[i] = Math.max(dp[i], dp[j] + 1);
                }
            }
            maxans = Math.max(maxans, dp[i]);
        }
        return maxans;
    }
}
复杂度分析

时间复杂度：O(n^2)O(n 
2
 )，其中 nn 为数组 \textit{nums}nums 的长度。动态规划的状态数为 nn，计算状态 dp[i]dp[i] 时，需要 O(n)O(n) 的时间遍历 dp[0 \ldots i-1]dp[0…i−1] 的所有状态，所以总时间复杂度为 O(n^2)O(n 
2
 )。

空间复杂度：O(n)O(n)，需要额外使用长度为 nn 的 dpdp 数组。

方法二：贪心 + 二分查找
思路与算法

考虑一个简单的贪心，如果我们要使上升子序列尽可能的长，则我们需要让序列上升得尽可能慢，因此我们希望每次在上升子序列最后加上的那个数尽可能的小。

基于上面的贪心思路，我们维护一个数组 d[i]d[i] ，表示长度为 ii 的最长上升子序列的末尾元素的最小值，用 \textit{len}len 记录目前最长上升子序列的长度，起始时 lenlen 为 11，d[1] = \textit{nums}[0]d[1]=nums[0]。

同时我们可以注意到 d[i]d[i] 是关于 ii 单调递增的。因为如果 d[j] \geq d[i]d[j]≥d[i] 且 j < ij<i，我们考虑从长度为 ii 的最长上升子序列的末尾删除 i-ji−j 个元素，那么这个序列长度变为 jj ，且第 jj 个元素 xx（末尾元素）必然小于 d[i]d[i]，也就小于 d[j]d[j]。那么我们就找到了一个长度为 jj 的最长上升子序列，并且末尾元素比 d[j]d[j] 小，从而产生了矛盾。因此数组 d[]d[] 的单调性得证。

我们依次遍历数组 \textit{nums}[]nums[] 中的每个元素，并更新数组 d[]d[] 和 lenlen 的值。如果 \textit{nums}[i] > d[\textit{len}]nums[i]>d[len] 则更新 len = len + 1len=len+1，否则在 d[1 \ldots len]d[1…len]中找满足 d[i - 1] < \textit{nums}[j] < d[i]d[i−1]<nums[j]<d[i] 的下标 ii，并更新 d[i] = \textit{nums}[j]d[i]=nums[j]。

根据 dd 数组的单调性，我们可以使用二分查找寻找下标 ii，优化时间复杂度。

最后整个算法流程为：

设当前已求出的最长上升子序列的长度为 \textit{len}len（初始时为 11），从前往后遍历数组 \textit{nums}nums，在遍历到 \textit{nums}[i]nums[i] 时：

如果 \textit{nums}[i] > d[\textit{len}]nums[i]>d[len] ，则直接加入到 dd 数组末尾，并更新 \textit{len} = \textit{len} + 1len=len+1；

否则，在 dd 数组中二分查找，找到第一个比 \textit{nums}[i]nums[i] 小的数 d[k]d[k] ，并更新 d[k + 1] = \textit{nums}[i]d[k+1]=nums[i]。

以输入序列 [0, 8, 4, 12, 2][0,8,4,12,2] 为例：

第一步插入 00，d = [0]d=[0]；

第二步插入 88，d = [0, 8]d=[0,8]；

第三步插入 44，d = [0, 4]d=[0,4]；

第四步插入 1212，d = [0, 4, 12]d=[0,4,12]；

第五步插入 22，d = [0, 2, 12]d=[0,2,12]。

最终得到最大递增子序列长度为 33。


class Solution {
    public int lengthOfLIS(int[] nums) {
        int len = 1, n = nums.length;
        if (n == 0) {
            return 0;
        }
        int[] d = new int[n + 1];
        d[len] = nums[0];
        for (int i = 1; i < n; ++i) {
            if (nums[i] > d[len]) {
                d[++len] = nums[i];
            } else {
                int l = 1, r = len, pos = 0; // 如果找不到说明所有的数都比 nums[i] 大，此时要更新 d[1]，所以这里将 pos 设为 0
                while (l <= r) {
                    int mid = (l + r) >> 1;
                    if (d[mid] < nums[i]) {
                        pos = mid;
                        l = mid + 1;
                    } else {
                        r = mid - 1;
                    }
                }
                d[pos + 1] = nums[i];
            }
        }
        return len;
    }
}
复杂度分析

时间复杂度：O(n\log n)O(nlogn)。数组 \textit{nums}nums 的长度为 nn，我们依次用数组中的元素去更新 dd 数组，而更新 dd 数组时需要进行 O(\log n)O(logn) 的二分搜索，所以总时间复杂度为 O(n\log n)O(nlogn)。

空间复杂度：O(n)O(n)，需要额外使用长度为 nn 的 dd 数组。

// Dynamic programming + Dichotomy.
class Solution {
    public int lengthOfLIS(int[] nums) {
        int[] tails = new int[nums.length];
        int res = 0;
        for(int num : nums) {
            int i = 0, j = res;
            while(i < j) {
                int m = (i + j) / 2;
                if(tails[m] < num) i = m + 1;
                else j = m;
            }
            tails[i] = num;
            if(res == j) res++;
        }
        return res;
    }
}

public int LengthOfLIS(int[] nums) {
	var res = new int[nums.Length];
	int len = 0;

	foreach(int num in nums)
	{
		var index = Array.BinarySearch(res, 0, len, num);
		index = index < 0 ? ~index : index;
		res[index] = num;
		len = index == len ? len+1 : len;
	}

	return len;
}

public class Solution {
    public int LengthOfLIS(int[] nums)
        {
            int length = nums.Length;
            if (length == 0)
            {
                return 0;
            }
            List<int> list = new List<int> { nums[0] };
            for (int i = 1; i < length; i++)
            {
                if (nums[i] > list.LastOrDefault())
                {
                    list.Add(nums[i]);
                }
                else
                {
                    var big = Enumerable.Range(0, list.Count).ToList().Find(o => list[o] >= nums[i]);
                    list[big] = nums[i];
                }
            }
            return list.Distinct().Count();
        }
}

public class Solution {
    public int LengthOfLIS(int[] nums) {
        int[] top = new int[nums.Length];
            // 牌堆数初始化为 0
            int piles = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                // 要处理的扑克牌
                int poker = nums[i];

                // 搜索左侧边界的二分查找
                int left = 0, right = piles;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (top[mid] > poker)
                    {
                        right = mid;
                    }
                    else if (top[mid] < poker)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid;
                    }
                }

                // 没找到合适的牌堆，新建一堆
                if (left == piles) piles++;
                // 把这张牌放到牌堆顶
                top[left] = poker;
            }
            // 牌堆数就是 LIS 长度
            return piles;
    }
}

public class Solution {
    public int LengthOfLIS(int[] nums) {
        int len = nums.Length;
            if (len <= 1) return len;

            // tail 数组的定义：长度为 i + 1 的上升子序列的末尾最小是几
            int[] tail = new int[len];
            // 遍历第 1 个数，直接放在有序数组 tail 的开头
            tail[0] = nums[0];
            // end 表示有序数组 tail 的最后一个已经赋值元素的索引
            int end = 0;

            for (int i = 1; i < len; i++)
            {
                // 【逻辑 1】比 tail 数组实际有效的末尾的那个元素还大
                if (nums[i] > tail[end])
                {
                    // 直接添加在那个元素的后面，所以 end 先加 1
                    end++;
                    tail[end] = nums[i];
                }
                else
                {
                    // 使用二分查找法，在有序数组 tail 中
                    // 找到第 1 个大于等于 nums[i] 的元素，尝试让那个元素更小
                    int left = 0;
                    int right = end;
                    while (left < right)
                    {
                        // 选左中位数不是偶然，而是有原因的，原因请见 LeetCode 第 35 题题解
                        int mid = left + (right - left) / 2;
                        //int mid = left + ((right - left) >>> 1);
                        if (tail[mid] < nums[i])
                        {
                            // 中位数肯定不是要找的数，把它写在分支的前面
                            left = mid + 1;
                        }
                        else
                        {
                            right = mid;
                        }
                    }
                    // 走到这里是因为 【逻辑 1】 的反面，因此一定能找到第 1 个大于等于 nums[i] 的元素
                    // 因此，无需再单独判断
                    tail[left] = nums[i];
                }
                // 调试方法
                // printArray(nums[i], tail);
            }
            // 此时 end 是有序数组 tail 最后一个元素的索引
            // 题目要求返回的是长度，因此 +1 后返回
            end++;
            return end;
    }
}


*/