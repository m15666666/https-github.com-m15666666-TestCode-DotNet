using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定 N，想象一个凸 N 边多边形，其顶点按顺时针顺序依次标记为 A[0], A[i], ..., A[N-1]。

假设您将多边形剖分为 N-2 个三角形。对于每个三角形，该三角形的值是顶点标记的乘积，三角剖分的分数是进行三角剖分后所有 N-2 个三角形的值之和。

返回多边形进行三角剖分后可以得到的最低分。
 

示例 1：

输入：[1,2,3]
输出：6
解释：多边形已经三角化，唯一三角形的分数为 6。
示例 2：



输入：[3,7,4,5]
输出：144
解释：有两种三角剖分，可能得分分别为：3*7*5 + 4*5*7 = 245，或 3*4*5 + 3*4*7 = 144。最低分数为 144。
示例 3：

输入：[1,3,1,4,1,5]
输出：13
解释：最低分数三角剖分的得分情况为 1*1*3 + 1*1*4 + 1*1*5 + 1*1*1 = 13。
 

提示：

3 <= A.length <= 50
1 <= A[i] <= 100 
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-score-triangulation-of-polygon/
/// 1039. 多边形三角剖分的最低得分
/// 
/// </summary>
class MinimumScoreTriangulationOfPolygonSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinScoreTriangulation(int[] A)
    {
        int n = A.Length;
        int[,] dp = new int[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                dp[i, j] = int.MaxValue;

        for (int i = 0; i < n; i++) dp[i,(i + 1) % n] = 0;

        for (int count = 2; count < n; count++)
            for (int start = 0; start < n; start++)
            {
                int stop = (start + count) % n;
                var vStart = A[start];
                var vStop = A[stop];
                for (int mid = (start + 1) % n; mid != stop; mid = (mid + 1) % n)
                {
                    var v = dp[start, mid] + dp[mid, stop] +  vStart * A[mid] * vStop;
                    if (v < dp[start, stop]) dp[start, stop] = v;
                }
            }
        return dp[0,n - 1];
    }
}
/*
C++ 动态规划题解
大力王
813 阅读
dp[i][j]为区间[i, j]的多边形最小值题解
这样就有了递推公式：
dp[i][j] = min{dp[i][k] + dp[k][j] + A[i][k][j]} for all k in range (i, j)

class Solution {
public:
    const int INF = 1000000;
    int minScoreTriangulation(vector<int>& A) {
        int N = A.size();
        vector<vector<int> > dp(N, vector<int>(N, INF));
        for (int i = 0; i < N; ++i) {
            dp[i][(i + 1) % N] = 0; // 两个点构不成三角形，初始化为0
        }
        for (int len = 2; len < N; ++len) {
            for (int i = 0; i < N; ++i) {
                int j = (i + len) % N;
                for (int k = (i + 1) % N; k != j; k = (k + 1) % N) {
                    dp[i][j] = min(dp[i][j], dp[i][k] + dp[k][j] + A[i]* A[k] * A[j]);
                }
            }
        }
        return dp[0][N - 1];
    }
};

简单DFS+记忆化与动态规划
初始化：

dp[i][j]：表示从第i个到第j个角所形成的三角形的最小面积
状态转换方程
dp[i][j] = min(dp[i][j], dp[i][k] + dp[k][j] + A[i] * A[k] * A[j])
class Solution:
    def minScoreTriangulation(self, A: List[int]) -> int:
        length = len(A)
        inf = float('inf')
        dp = [[inf for _ in range(length)] for _ in range(length)]

        for i in range(length - 1):
            dp[i][i + 1] = 0

        for d in range(2, length):
            for i in range(0, length - d):
                j = i + d
                for k in range(i + 1, j):
                    dp[i][j] = min(dp[i][j], dp[i][k] + dp[k][j] + A[i] * A[k] * A[j])

        return dp[0][length - 1]
DFS加记忆化
主要是子问题的切分，dfs更容易理解一些

from functools import lru_cache


class Solution:
    def minScoreTriangulation(self, A: List[int]) -> int:
        @lru_cache(None)
        def dfs(left, right):
            if left + 1 == right:
                return 0
            ans = float('inf')
            for k in range(left + 1, right):
                ans = min(ans, dfs(left, k) + dfs(k, right) + A[left] * A[k] * A[right])

            return ans

        return dfs(0, len(A) - 1)
 
*/
