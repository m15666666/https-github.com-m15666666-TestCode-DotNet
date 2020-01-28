using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出非负整数数组 A ，返回两个非重叠（连续）子数组中元素的最大和，子数组的长度分别为 L 和 M。（这里需要澄清的是，长为 L 的子数组可以出现在长为 M 的子数组之前或之后。）

从形式上看，返回最大的 V，而 V = (A[i] + A[i+1] + ... + A[i+L-1]) + (A[j] + A[j+1] + ... + A[j+M-1]) 并满足下列条件之一：

 

0 <= i < i + L - 1 < j < j + M - 1 < A.length, 或
0 <= j < j + M - 1 < i < i + L - 1 < A.length.
 

示例 1：

输入：A = [0,6,5,2,2,5,1,9,4], L = 1, M = 2
输出：20
解释：子数组的一种选择中，[9] 长度为 1，[6,5] 长度为 2。
示例 2：

输入：A = [3,8,1,3,2,1,8,9,0], L = 3, M = 2
输出：29
解释：子数组的一种选择中，[3,8,1] 长度为 3，[8,9] 长度为 2。
示例 3：

输入：A = [2,1,5,6,0,9,5,0,3,8], L = 4, M = 3
输出：31
解释：子数组的一种选择中，[5,6,0,9] 长度为 4，[0,3,8] 长度为 3。
 

提示：

L >= 1
M >= 1
L + M <= A.length <= 1000
0 <= A[i] <= 1000
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-sum-of-two-non-overlapping-subarrays/
/// 1031. 两个非重叠子数组的最大和
/// 
/// </summary>
class MaximumSumOfTwoNonOverlappingSubarraysSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxSumTwoNoOverlap(int[] A, int L, int M)
    {
		const int leftL = 0;
		const int leftM = 1;
		const int RightL = 2;
		const int RightM = 3;

		int len = A.Length;
		int[,] dp = new int[len, 4];

		int presum = 0;
		for (int i = 0; i < L; ++i) presum += A[i];

		int maxsum = presum;
		dp[L - 1,leftL] = maxsum;
		for (int i = L; i < len; ++i)
		{
			presum -= A[i - L];
			presum += A[i];
			if (maxsum < presum) maxsum = presum;
			dp[i,leftL] = maxsum;
		}

		presum = 0;
		for (int i = 0; i < M; ++i) presum += A[i];

		maxsum = presum;
		dp[M - 1,leftM] = maxsum;
		for (int i = M; i < len; ++i)
		{
			presum -= A[i - M];
			presum += A[i];
			if (maxsum < presum) maxsum = presum;
			dp[i,leftM] = maxsum;
		}

		presum = 0;
		for (int i = len - 1; i >= len - L; --i) presum += A[i];
		
		maxsum = presum;
		dp[len - L,RightL] = maxsum;
		for (int i = len - L - 1; i >= 0; --i)
		{
			presum -= A[i + L];
			presum += A[i];
			if (maxsum < presum) maxsum = presum;
			dp[i,RightL] = maxsum;
		}

		presum = 0;
		for (int i = len - 1; i >= len - M; --i) presum += A[i];
		
		maxsum = presum;
		dp[len - M,RightM] = maxsum;
		for (int i = len - M - 1; i >= 0; --i)
		{
			presum -= A[i + M];
			presum += A[i];
			if (maxsum < presum) maxsum = presum;
			dp[i,RightM] = maxsum;
		}

		int ret = 0;
		for (int i = L; i <= len - M; ++i)
		{
			var v = dp[i - 1, leftL] + dp[i, RightM];
			if (ret < v) ret = v;
		}

		for (int i = M; i <= len - L; ++i)
		{
			var v = dp[i - 1, leftM] + dp[i, RightL];
			if (ret < v) ret = v;
		}

		return ret;
	}
}
/*
C++ 动态规划+滑动窗口 O(n)
blacksea3
823 阅读
思路:
考虑题意: 必然存在一条分界线把 A 拆分成两半，存在两大类情况：
长度为 L 的连续子数组在左边, 长度为 M 的连续子数组在右边
或者反过来长度为 M 的连续子数组在左边, 长度为 L 的连续子数组在右边
引入

dp[i][0]: 从 A[0]-A[i] 连续 L 长度子数组最大的元素和
dp[i][1]: 从 A[0]-A[i] 连续 M 长度子数组最大的元素和
dp[i][2]: 从 A[i]-A[A.size()-1] 连续 L 长度子数组最大的元素和
dp[i][3]: 从 A[i]-A[A.size()-1] 连续 M 长度子数组最大的元素和
某些超出范围的下标, 值设置为 0 (默认值)
代码中首先用滑动窗口计算了 dp, 然后将 dp 分成两组, 计算两大类情况下的结果，取最大值返回即可

代码：
class Solution {
public:
	int maxSumTwoNoOverlap(vector<int>& A, int L, int M) {
		//计算dp, 4个滑动窗口, 4种情况
		vector<vector<int>> dp(A.size(), vector<int>(4, 0));
		int presum = 0;
		int maxsum;
		for (int i = 0; i < L; ++i)
		{
			presum += A[i];
		}
		maxsum = presum;
		dp[L - 1][0] = maxsum;
		for (int i = L; i < A.size(); ++i)
		{
			presum -= A[i - L];
			presum += A[i];
			maxsum = max(maxsum, presum);
			dp[i][0] = maxsum;
		}

		presum = 0;
		for (int i = 0; i < M; ++i)
		{
			presum += A[i];
		}
		maxsum = presum;
		dp[M - 1][1] = maxsum;
		for (int i = M; i < A.size(); ++i)
		{
			presum -= A[i - M];
			presum += A[i];
			maxsum = max(maxsum, presum);
			dp[i][1] = maxsum;
		}

		presum = 0;
		for (int i = A.size() - 1; i >= A.size() - L; --i)
		{
			presum += A[i];
		}
		maxsum = presum;
		dp[A.size() - L][2] = maxsum;
		for (int i = A.size() - L - 1; i >= 0; --i)
		{
			presum -= A[i + L];
			presum += A[i];
			maxsum = max(maxsum, presum);
			dp[i][2] = maxsum;
		}

		presum = 0;
		for (int i = A.size() - 1; i >= A.size() - M; --i)
		{
			presum += A[i];
		}
		maxsum = presum;
		dp[A.size() - M][3] = maxsum;
		for (int i = A.size() - M - 1; i >= 0; --i)
		{
			presum -= A[i + M];
			presum += A[i];
			maxsum = max(maxsum, presum);
			dp[i][3] = maxsum;
		}

		//计算答案
		int res = 0;
		//L在M左边
		for (int i = L; i <= A.size() - M; ++i)
			res = max(res, dp[i - 1][0] + dp[i][3]);
		//M在L左边
		for (int i = M; i <= A.size() - L; ++i)
			res = max(res, dp[i - 1][1] + dp[i][2]);

		return res;
	}
};
 
*/
