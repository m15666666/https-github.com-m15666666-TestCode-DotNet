using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
如果一个由 '0' 和 '1' 组成的字符串，是以一些 '0'（可能没有 '0'）后面跟着一些 '1'（也可能没有 '1'）的形式组成的，那么该字符串是单调递增的。

我们给出一个由字符 '0' 和 '1' 组成的字符串 S，我们可以将任何 '0' 翻转为 '1' 或者将 '1' 翻转为 '0'。

返回使 S 单调递增的最小翻转次数。

示例 1：

输入："00110"
输出：1
解释：我们翻转最后一位得到 00111.
示例 2：

输入："010110"
输出：2
解释：我们翻转得到 011111，或者是 000111。
示例 3：

输入："00011000"
输出：2
解释：我们翻转得到 00000000。

提示：

1 <= S.length <= 20000
S 中只包含字符 '0' 和 '1'
 
*/
/// <summary>
/// https://leetcode-cn.com/problems/flip-string-to-monotone-increasing/
/// 926. 将字符串翻转到单调递增
/// 
/// </summary>
class FlipStringToMonotoneIncreasingSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinFlipsMonoIncr(string S)
    {
        int len = S.Length;
        int[] P = new int[len + 1];
        for (int i = 0; i < len; i++)
            P[i + 1] = P[i] + (S[i] == '1' ? 1 : 0);

        int ret = int.MaxValue;
        for (int j = 0; j <= len; ++j)
        {
            ret = Math.Min(ret, P[j] + len - j - (P[len] - P[j]));
        }
        return ret;
    }
}
/*

方法一：前缀和
思路

对于一个包含 5 个数字的字符串来说，答案可能是 '00000'，'00001'，'00011'，'00111'，'01111'，'11111' 中的任何一个。可以依次原始字符串转换成每个答案的代价，其中计算分成两个部分，左边全 0 的部分和右边全 1 的部分。

显然，这个问题可以简化成： 对于每种分割方法，左边有多少个 1 需要去反转，右边有多少个 0 需要去反转。

对这个问题，可以用全缀和来解决。定义 P[i+1] = A[0] + A[1] + ... + A[i]，P 可以在线性时间计算出来。

假设最终答案是 x 个 0 跟 N - x 个 1，那么就必须反转 P[x] 个 1， (N-x) - (P[N] - P[x]) 个 0。 其中 P[N] - P[x] 是右边全 1 部分原本 1 的个数。

算法

举个例子，对于 S = "010110"： P = [0, 0, 1, 1, 2, 3, 3]。假设 x=3，即最终答案左边有三个 0。

有 P[3] = 1 个 1 在左边全 0 部分，P[6] - P[3] = 2 个 1 在右边全 1 部分。

所以，左边有 P[3] = 1 个 1 需要反转，右边有 (N-x) - (P[N] - P[x]) = 1 个 0 需要去反转。

JavaPython
class Solution {
    public int minFlipsMonoIncr(String S) {
        int N = S.length();
        int[] P = new int[N + 1];
        for (int i = 0; i < N; ++i)
            P[i+1] = P[i] + (S.charAt(i) == '1' ? 1 : 0);

        int ans = Integer.MAX_VALUE;
        for (int j = 0; j <= N; ++j) {
            ans = Math.min(ans, P[j] + N-j-(P[N]-P[j]));
        }

        return ans;
    }
}
复杂度分析

时间复杂度： O(N)O(N)，其中 NN 是 S 的长度。

空间复杂度： O(N)O(N)。
 
*/
