using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个由整数数组 A 表示的环形数组 C，求 C 的非空子数组的最大可能和。

在此处，环形数组意味着数组的末端将会与开头相连呈环状。（形式上，当0 <= i < A.length 时 C[i] = A[i]，而当 i >= 0 时 C[i+A.length] = C[i]）

此外，子数组最多只能包含固定缓冲区 A 中的每个元素一次。（形式上，对于子数组 C[i], C[i+1], ..., C[j]，不存在 i <= k1, k2 <= j 其中 k1 % A.length = k2 % A.length）

 

示例 1：

输入：[1,-2,3,-2]
输出：3
解释：从子数组 [3] 得到最大和 3
示例 2：

输入：[5,-3,5]
输出：10
解释：从子数组 [5,5] 得到最大和 5 + 5 = 10
示例 3：

输入：[3,-1,2,-1]
输出：4
解释：从子数组 [2,-1,3] 得到最大和 2 + (-1) + 3 = 4
示例 4：

输入：[3,-2,2,-3]
输出：3
解释：从子数组 [3] 和 [3,-2,2] 都可以得到最大和 3
示例 5：

输入：[-2,-3,-1]
输出：-1
解释：从子数组 [-1] 得到最大和 -1
 

提示：

-30000 <= A[i] <= 30000
1 <= A.length <= 30000
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-sum-circular-subarray/
/// 918. 环形子数组的最大和
/// 
/// </summary>
class MaximumSumCircularSubarraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxSubarraySumCircular(int[] A)
    {
        int len = A.Length;

        int ret = A[0], cur = A[0];
        for (int i = 1; i < len; ++i)
        {
            cur = A[i] + Math.Max(cur, 0);
            ret = Math.Max(ret, cur);
        }

        int[] rightsums = new int[len];
        rightsums[len - 1] = A[len - 1];
        for (int i = len - 2; i >= 0; --i)
            rightsums[i] = rightsums[i + 1] + A[i];

        int[] maxright = new int[len];
        maxright[len - 1] = A[len - 1];
        for (int i = len - 2; i >= 0; --i)
            maxright[i] = Math.Max(maxright[i + 1], rightsums[i]);

        int leftsum = 0;
        for (int i = 0; i < len - 2; ++i)
        {
            leftsum += A[i];
            ret = Math.Max(ret, leftsum + maxright[i + 2]);
        }

        return ret;
    }
}
/*
Kanade 算法介绍
方法介绍

在方法 1 和方法 2 中，“grindy” 的解决方法需要很少的思考，但对于那些熟知这种方法的人而言，非常直观。如果没有经验，这些方法很难被发现。

方法 3 和方法 4 更容易实现，但需要更多的思考。

Kadane 算法解释

为了理解本文的算法，我们需要熟悉 Kadane 算法。在这个章节，我们解释算法背后的核心逻辑。

对于一个给定数组 A，Kadane 算法可以用来找到 A 的最大子段和。这里，我们只考虑非空子段。

Kadane 算法基于动态规划。令 dp[j] 为以 A[j] 结尾的最大子段和。也就是，

\text{dp}[j] = \max\limits_i (A[i] + A[i+1] + \cdots + A[j])
dp[j]= 
i
max
​	
 (A[i]+A[i+1]+⋯+A[j])

那么，以 j+1 j结尾的子段（例如 A[i], A[i+1] + ... + A[j+1]）最大化了 A[i] + ... + A[j] 的和，当这个子段非空那么就等于 dp[j] 否则就等于 0。所以，有以下递推式：

\text{dp}[j+1] = A[j+1] + \max(\text{dp}[j], 0)
dp[j+1]=A[j+1]+max(dp[j],0)

由于一个子段一定从某个位置截止，所以 \max\limits_j dp[j] 
j
max
​	
 dp[j] 就是需要的答案。

为了计算 dp 数组更快，Kadane 算法通常节约空间复杂度的形式表示。我们只维护两个变量 ans 等于 \max\limits_j dp[j] 
j
max
​	
 dp[j] 和 cur 等于 dp[j]dp[j]。随着 j 从 00 到 A.\text{length}-1A.length−1 遍历。

Kadane 算法的伪代码如下：

Python
#Kadane's algorithm
ans = cur = None
for x in A:
    cur = x + max(cur, 0)
    ans = max(ans, cur)
return ans
方法 1：邻接数组
想法和算法

循环数组的子段可以被分成 单区间 子段或者 双区间 子段，取决于定长数组 A 需要多少区间去表示。

例如，如果 A = [0, 1, 2, 3, 4, 5, 6] 是给定的循环数组，我们可以表示子段 [2, 3, 4] 为单区间 [2, 4][2,4]；如果我们希望表示子段 [5, 6, 0, 1]，那就是双区间 [5, 6], [0, 1][5,6],[0,1]。

使用 Kadane 算法，我们知道如何计算一个单区间子段的最大值，所以剩下的问题是双区间子段。

考虑区间为 [0, i], [j, A\text{.length} - 1][0,i],[j,A.length−1]。计算第 ii 个参数，对于固定 ii 两区间子串的最大值。计算 [0, i][0,i] 的和很简单，考虑，

T_j = A[j] + A[j+1] + \cdots + A[A\text{.length} - 1]
T 
j
​	
 =A[j]+A[j+1]+⋯+A[A.length−1]

和，

R_j = \max\limits_{k \geq j} T_k
R 
j
​	
 = 
k≥j
max
​	
 T 
k
​	
 

所以期望的第 ii 个候选结果为：

(A[0] + A[1] + \cdots + A[i]) + R_{i+2}
(A[0]+A[1]+⋯+A[i])+R 
i+2
​	
 

由于我们可以限行时间计算 T_jT 
j
​	
  和 R_jR 
j
​	
 ，结果是显然的。

JavaPython
class Solution {
    public int maxSubarraySumCircular(int[] A) {
        int N = A.length;

        int ans = A[0], cur = A[0];
        for (int i = 1; i < N; ++i) {
            cur = A[i] + Math.max(cur, 0);
            ans = Math.max(ans, cur);
        }

        // ans is the answer for 1-interval subarrays.
        // Now, let's consider all 2-interval subarrays.
        // For each i, we want to know
        // the maximum of sum(A[j:]) with j >= i+2

        // rightsums[i] = A[i] + A[i+1] + ... + A[N-1]
        int[] rightsums = new int[N];
        rightsums[N-1] = A[N-1];
        for (int i = N-2; i >= 0; --i)
            rightsums[i] = rightsums[i+1] + A[i];

        // maxright[i] = max_{j >= i} rightsums[j]
        int[] maxright = new int[N];
        maxright[N-1] = A[N-1];
        for (int i = N-2; i >= 0; --i)
            maxright[i] = Math.max(maxright[i+1], rightsums[i]);

        int leftsum = 0;
        for (int i = 0; i < N-2; ++i) {
            leftsum += A[i];
            ans = Math.max(ans, leftsum + maxright[i+2]);
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 A 的长度。
空间复杂度：O(N)O(N)。
方法 2：前缀和 + 单调队列
想法

首先，我们可以在一个定长数组上完成这个问题。

我们可以认为循环数组 A 的任意子段，一定是定长数组 A+A 的任一个子段。

例如，对于循环数组 A = [0,1,2,3,4,5]，那么它的子段 [4,5,0,1] 一定也是定长数组 [0,1,2,3,4,5,0,1,2,3,4,5] 的子段。令 B = A + A 就是这个定长数组。

假定 N = A\text{.length}N=A.length，考虑前缀和

P_k = B[0] + B[1] + \cdots + B[k-1]
P 
k
​	
 =B[0]+B[1]+⋯+B[k−1]

然后，考虑最大的 P_j - P_iP 
j
​	
 −P 
i
​	
  其中 j - i \leq Nj−i≤N。

考虑第 jj 个候选答案：对于固定 jj 的最优结果 P_j - P_iP 
j
​	
 −P 
i
​	
 。我们希望一个 ii 使得 P_iP 
i
​	
  尽量小 并且满足 j - N \leq i < jj−N≤i<j，称对于第 jj 个候选答案的的最优 ii。我们可以用优先队列实现它。

算法

迭代 jj，每次计算第 jj 个候选答案。我们维护一个 queue 保存可能的最优 ii。

核心想法是如果 i_1 < i_2i 
1
​	
 <i 
2
​	
  且 P_{i_1} \geq P_{i_2}P 
i 
1
​	
 
​	
 ≥P 
i 
2
​	
 
​	
 ，那么就不再需要考虑 i_1i 
1
​	
 。

查看代码了解更多实现细节。

JavaPython
class Solution {
    public int maxSubarraySumCircular(int[] A) {
        int N = A.length;

        // Compute P[j] = B[0] + B[1] + ... + B[j-1]
        // for fixed array B = A+A
        int[] P = new int[2*N+1];
        for (int i = 0; i < 2*N; ++i)
            P[i+1] = P[i] + A[i % N];

        // Want largest P[j] - P[i] with 1 <= j-i <= N
        // For each j, want smallest P[i] with i >= j-N
        int ans = A[0];
        // deque: i's, increasing by P[i]
        Deque<Integer> deque = new ArrayDeque();
        deque.offer(0);

        for (int j = 1; j <= 2*N; ++j) {
            // If the smallest i is too small, remove it.
            if (deque.peekFirst() < j-N)
                deque.pollFirst();

            // The optimal i is deque[0], for cand. answer P[j] - P[i].
            ans = Math.max(ans, P[j] - P[deque.peekFirst()]);

            // Remove any i1's with P[i2] <= P[i1].
            while (!deque.isEmpty() && P[j] <= P[deque.peekLast()])
                deque.pollLast();

            deque.offerLast(j);
        }

        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 A 的长度。
空间复杂度：O(N)O(N)。
方法 3：Kadane 算法（符号变种）
想法和算法

如方法 1 所示，一个循环数组的子段可以分成单区间子段和双区间子段。

使用 Kadane 算法 kadane 找到非空最大子段和，单区间子段的结果是 kadane(A)。

令 N = A.\text{length}N=A.length。对于双区间子段，如：

(A_0 + A_1 + \cdots + A_i) + (A_j + A_{j+1} + \cdots + A_{N - 1})
(A 
0
​	
 +A 
1
​	
 +⋯+A 
i
​	
 )+(A 
j
​	
 +A 
j+1
​	
 +⋯+A 
N−1
​	
 )

我们可以写成：

(\sum_{k=0}^{N-1} A_k) - (A_{i+1} + A_{i+2} + \cdots + A_{j-1})
( 
k=0
∑
N−1
​	
 A 
k
​	
 )−(A 
i+1
​	
 +A 
i+2
​	
 +⋯+A 
j−1
​	
 )

对于双区间子段，令 BB 是数组 AA 所有元素乘以 -1−1 的结果。那么双区间子段的结果就是 \text{sum}(A) + \text{kadane}(B)sum(A)+kadane(B)。

但是，这并不是完全正确的，如果我们选择了 BB 的完整数组，双区间子段 [0, i] + [j, N-1][0,i]+[j,N−1] 为空。

我们可以做出如下补救：做 Kadane 算法两次，一次去掉 BB 的第一个元素，一次去掉 BB 的最后一个元素。

JavaPython
class Solution {
    public int maxSubarraySumCircular(int[] A) {
        int S = 0;  // S = sum(A)
        for (int x: A)
            S += x;

        int ans1 = kadane(A, 0, A.length-1, 1);
        int ans2 = S + kadane(A, 1, A.length-1, -1);
        int ans3 = S + kadane(A, 0, A.length-2, -1);
        return Math.max(ans1, Math.max(ans2, ans3));
    }

    public int kadane(int[] A, int i, int j, int sign) {
        // The maximum non-empty subarray for array
        // [sign * A[i], sign * A[i+1], ..., sign * A[j]]
        int ans = Integer.MIN_VALUE;
        int cur = Integer.MIN_VALUE;
        for (int k = i; k <= j; ++k) {
            cur = sign * A[k] + Math.max(cur, 0);
            ans = Math.max(ans, cur);
        }
        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 A 的长度。
空间复杂度：O(1)O(1)。
方法 4：Kadane 算法（最小值变种）
想法和算法

如方法 3，循环数组的子段可以分成单区间子段（直接使用 Kanade 算法）和双区间子段.

我们可以改进 Kadane 算法，用 min 代替 max。所有 Kadane 算法的解释依然相同，但是算法可以让我们找到最小子段和。

对于双区间子段形如 (\sum_{k=0}^{N-1} A_k) - (\sum_{k=i+1}^{j-1} A_k)(∑ 
k=0
N−1
​	
 A 
k
​	
 )−(∑ 
k=i+1
j−1
​	
 A 
k
​	
 )，我们可以使用 kadane-min 算法最小化”内部“ (\sum_{k=i+1}^{j-1} A_k)(∑ 
k=i+1
j−1
​	
 A 
k
​	
 ) 和。

同理，由于区间 [i+1, j-1][i+1,j−1] 必须非空，我们将搜索分成 A[1:] 和 A[:-1] 两个区间考虑。

JavaPython
class Solution {
    public int maxSubarraySumCircular(int[] A) {
        // S: sum of A
        int S = 0;
        for (int x: A)
            S += x;

        // ans1: answer for one-interval subarray
        int ans1 = Integer.MIN_VALUE;
        int cur = Integer.MIN_VALUE;
        for (int x: A) {
            cur = x + Math.max(cur, 0);
            ans1 = Math.max(ans1, cur);
        }

        // ans2: answer for two-interval subarray, interior in A[1:]
        int ans2 = Integer.MAX_VALUE;
        cur = Integer.MAX_VALUE;
        for (int i = 1; i < A.length; ++i) {
            cur = A[i] + Math.min(cur, 0);
            ans2 = Math.min(ans2, cur);
        }
        ans2 = S - ans2;

        // ans3: answer for two-interval subarray, interior in A[:-1]
        int ans3 = Integer.MAX_VALUE;
        cur = Integer.MAX_VALUE;
        for (int i = 0; i < A.length - 1; ++i) {
            cur = A[i] + Math.min(cur, 0);
            ans3 = Math.min(ans3, cur);
        }

        return Math.max(ans1, Math.max(ans2, ans3));
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是 A 的长度。
空间复杂度：O(1)O(1)。
 
*/
