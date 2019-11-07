using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 A，对于每个整数 A[i]，我们可以选择 x = -K 或是 x = K，并将 x 加到 A[i] 中。

在此过程之后，我们得到一些数组 B。

返回 B 的最大值和 B 的最小值之间可能存在的最小差值。

示例 1：

输入：A = [1], K = 0
输出：0
解释：B = [1]
示例 2：

输入：A = [0,10], K = 2
输出：6
解释：B = [2,8]
示例 3：

输入：A = [1,3,6], K = 3
输出：3
解释：B = [4,6,3]
 

提示：

1 <= A.length <= 10000
0 <= A[i] <= 10000
0 <= K <= 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/smallest-range-ii/
/// 910. 最小差值 II
/// 
/// </summary>
class SmallestRangeIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SmallestRangeII(int[] A, int K)
    {
        Array.Sort(A);

        int len = A.Length;
        int ret = A[len - 1] - A[0];
        int smallestMax = A[len - 1] - K;
        int largestMin = A[0] + K;

        (smallestMax, largestMin) = (Math.Max(smallestMax, largestMin), Math.Min(smallestMax, largestMin));
        int upper = len - 1;
        for (int i = 0; i < upper; i++)
        {
            int aI = A[i];
            int aI1 = A[i + 1];
            int high = Math.Max(smallestMax, aI + K);
            int low = Math.Min(largestMin, aI1 - K);
            var span = high - low;
            if (span < ret) ret = span;
        }
        return ret;
    }
}
/*
方法 1：线性扫描
想法

如 最小差值 I 问题的解决方法，较小的 A[i] 将增加，较大的 A[i] 将变小。

算法

我们可以对上述想法形式化表述：如果 A[i] < A[j]，我们不必考虑当 A[i] 增大时 A[j] 会减小。这是因为区间 (A[i] + K, A[j] - K) 是 (A[i] - K, A[j] + K) 的子集（这里，当 a > b 时 (a, b) 表示 (b, a) ）。

这意味着对于 (up, down) 的选择一定不会差于 (down, up)。我们可以证明其中一个区间是另一个的子集，通过证明 A[i] + K 和 A[j] - K 是在 A[i] - K 和 A[j] + K 之间。

对于有序的 A，设 A[i] 是最大的需要增长的 i，那么 A[0] + K, A[i] + K, A[i+1] - K, A[A.length - 1] - K 就是计算结果的唯一值。

JavaPython
class Solution {
    public int smallestRangeII(int[] A, int K) {
        int N = A.length;
        Arrays.sort(A);
        int ans = A[N-1] - A[0];

        for (int i = 0; i < A.length - 1; ++i) {
            int a = A[i], b = A[i+1];
            int high = Math.max(A[N-1] - K, a + K);
            int low = Math.min(A[0] + K, b - K);
            ans = Math.min(ans, high - low);
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(N \log N)O(NlogN)，其中 NN 是 A 的长度。
空间复杂度：O(1)O(1)，额外空间就是自带排序算法的空间。
 
*/
