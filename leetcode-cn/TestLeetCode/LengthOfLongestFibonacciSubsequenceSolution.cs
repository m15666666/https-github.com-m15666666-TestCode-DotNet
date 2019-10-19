using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
如果序列 X_1, X_2, ..., X_n 满足下列条件，就说它是 斐波那契式 的：

n >= 3
对于所有 i + 2 <= n，都有 X_i + X_{i+1} = X_{i+2}
给定一个严格递增的正整数数组形成序列，找到 A 中最长的斐波那契式的子序列的长度。如果一个不存在，返回  0 。

（回想一下，子序列是从原序列 A 中派生出来的，它从 A 中删掉任意数量的元素（也可以不删），而不改变其余元素的顺序。例如， [3, 5, 8] 是 [3, 4, 5, 6, 7, 8] 的一个子序列）

 

示例 1：

输入: [1,2,3,4,5,6,7,8]
输出: 5
解释:
最长的斐波那契式子序列为：[1,2,3,5,8] 。
示例 2：

输入: [1,3,7,11,12,14,18]
输出: 3
解释:
最长的斐波那契式子序列有：
[1,11,12]，[3,11,14] 以及 [7,11,18] 。
 

提示：

3 <= A.length <= 1000
1 <= A[0] < A[1] < ... < A[A.length - 1] <= 10^9
（对于以 Java，C，C++，以及 C# 的提交，时间限制被减少了 50%）
*/
/// <summary>
/// https://leetcode-cn.com/problems/length-of-longest-fibonacci-subsequence/
/// 873. 最长的斐波那契子序列的长度
/// </summary>
class LengthOfLongestFibonacciSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LenLongestFibSubseq(int[] A)
    {
        int len = A.Length;
        HashSet<int> set = new HashSet<int>(A);

        int ret = 0;
        for (int i = 0; i < len; ++i)
            for (int j = i + 1; j < len; ++j)
            {
                /* With the starting pair (A[i], A[j]),
                 * y represents the future expected value in
                 * the fibonacci subsequence, and x represents
                 * the most current value found. */
                int x = A[j], y = A[i] + A[j];
                int length = 2;
                while (set.Contains(y))
                {
                    int z = x + y;
                    x = y;
                    y = z;
                    //(x, y) = (y, x + y);
                    ++length;
                    if (ret < length) ret = length;
                }
            }

        return 2 < ret ? ret : 0;
    }
}
/*
方法一：使用 Set 的暴力法
思路

每个斐波那契式的子序列都依靠两个相邻项来确定下一个预期项。例如，对于 2, 5，我们所期望的子序列必定以 7, 12, 19, 31 等继续。

我们可以使用 Set 结构来快速确定下一项是否在数组 A 中。由于这些项的值以指数形式增长，最大值 \leq 10^9≤10 
9
  的斐波那契式的子序列最多有 43 项。

算法

对于每个起始对 A[i], A[j]，我们保持下一个预期值 y = A[i] + A[j] 和此前看到的最大值 x = A[j]。如果 y 在数组中，我们可以更新这些值 (x, y) -> (y, x+y)。

此外，由于子序列的长度大于等于 3 只能是斐波那契式的，所以我们必须在最后进行检查 ans >= 3 ? ans : 0。

C++JavaPython
class Solution {
public:
    int lenLongestFibSubseq(vector<int>& A) {
        int N = A.size();
        unordered_set<int> S(A.begin(), A.end());

        int ans = 0;
        for (int i = 0; i < N; ++i)
            for (int j = i+1; j < N; ++j) {
                
                int x = A[j], y = A[i] + A[j];
                int length = 2;
                while (S.find(y) != S.end()) {
                    int z = x + y;
                    x = y;
                    y = z;
                    ans = max(ans, ++length);
                }
            }

        return ans >= 3 ? ans : 0;
    }
};
复杂度分析

时间复杂度：O(N^2 \log M)O(N 
2
 logM)，其中 NN 是 A 的长度，MM 是 A 中的最大值。
空间复杂度：O(N)O(N)，集合（set）S 使用的空间。
方法二：动态规划
思路

将斐波那契式的子序列中的两个连续项 A[i], A[j] 视为单个结点 (i, j)，整个子序列是这些连续结点之间的路径。

例如，对于斐波那契式的子序列 (A[1] = 2, A[2] = 3, A[4] = 5, A[7] = 8, A[10] = 13)，结点之间的路径为 (1, 2) <-> (2, 4) <-> (4, 7) <-> (7, 10)。

这样做的动机是，只有当 A[i] + A[j] == A[k] 时，两结点 (i, j) 和 (j, k) 才是连通的，我们需要这些信息才能知道这一连通。现在我们得到一个类似于 最长上升子序列 的问题。

算法

设 longest[i, j] 是结束在 [i, j] 的最长路径。那么 如果 (i, j) 和 (j, k) 是连通的， longest[j, k] = longest[i, j] + 1。

由于 i 由 A.index(A[k] - A[j]) 唯一确定，所以这是有效的：我们在 i 潜在时检查每组 j < k，并相应地更新 longest[j, k]。

C++JavaPython
class Solution {
public:
    int lenLongestFibSubseq(vector<int>& A) {
        int N = A.size();
        unordered_map<int, int> index;
        for (int i = 0; i < N; ++i)
            index[A[i]] = i;

        unordered_map<int, int> longest;
        int ans = 0;
        for (int k = 0; k < N; ++k)
            for (int j = 0; j < k; ++j) {
                if (A[k] - A[j] < A[j] && index.count(A[k] - A[j])) {
                    int i = index[A[k] - A[j]];
                    longest[j * N + k] = longest[i * N + j] + 1;
                    ans = max(ans, longest[j * N + k] + 2);
                }
            }

        return ans >= 3 ? ans : 0;
    }
};
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是 A 的长度。
空间复杂度：O(N \log M)O(NlogM)，其中 MM 是 A 中最大的元素。我们可以证明子序列中的元素数量是有限的（复杂度 O(\log \frac{M}{a})O(log 
a
M
?	
 )，其中 aa 是子序列中最小的元素）。

*/
