using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
爱丽丝参与一个大致基于纸牌游戏 “21点” 规则的游戏，描述如下：

爱丽丝以 0 分开始，并在她的得分少于 K 分时抽取数字。 抽取时，她从 [1, W] 的范围中随机获得一个整数作为分数进行累计，其中 W 是整数。 每次抽取都是独立的，其结果具有相同的概率。

当爱丽丝获得不少于 K 分时，她就停止抽取数字。 爱丽丝的分数不超过 N 的概率是多少？

示例 1：

输入：N = 10, K = 1, W = 10
输出：1.00000
说明：爱丽丝得到一张卡，然后停止。
示例 2：

输入：N = 6, K = 1, W = 10
输出：0.60000
说明：爱丽丝得到一张卡，然后停止。
在 W = 10 的 6 种可能下，她的得分不超过 N = 6 分。
示例 3：

输入：N = 21, K = 17, W = 10
输出：0.73278
提示：

0 <= K <= N <= 10000
1 <= W <= 10000
如果答案与正确答案的误差不超过 10^-5，则该答案将被视为正确答案通过。
此问题的判断限制时间已经减少。
在真实的面试中遇到过这道题？
*/
/// <summary>
/// https://leetcode-cn.com/problems/new-21-game/
/// 837. 新21点
/// https://www.jianshu.com/p/7b052db9c217
/// </summary>
class New21GameSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public double New21Game(int N, int K, int W)
    {
        if (K == 0) return 1;

        //我们拿3 2 3举例，可以得到下面的规律
        //假设开始我们的分数为K-1=1，我们可以拿1 2 3，得到的分数为2 3 4，为什么要假设分数为K-1呢，因为这时候没有递归的情况出现，因为肯定都>=K了
        //1-W每个选择的概率
        double everOne = 1f / W;

        //其中满足<=N的数量是
        int lessNCount = N - K + 1;

        //我们记录算出来的所有开始分数的概率
        double[] dp = new double[K];

        // >=k 时，概率是固定值 1/W
        dp[K - 1] = lessNCount * everOne;

        // 从K-1向前计算概率。probability[i]概率定义为：如果i节点之前概率为1时，得到满足题目条件的概率。
        // 当开始分数为K-2时，我们得到的是1 2 3，
        // 这时候就有两部分了，一部分是>=K的2和3，另一部分是<k的1
        for (int currK = K - 2; -1 < currK; currK--)
        {
            double preValue = dp[currK + 1]; // 取前一个点的概率

            // currK 为向后W个几点的概率，每个概率都乘 1/W，然后取和。
            // 在其他条件不变的情况下，后一个点比前一个点增加概率 1/W
            double currValue = (everOne + 1) * preValue;

            int lastIndex = currK + W; // 最后一个节点，边缘节点

            // 当currK + W< N时，条件发生了变化，i-1节点不再拥有i节点的所有下一阶段概率节点，所以要减去不再拥有的节点。
            // 被减去的节点就是：currK + W + 1
            if ( lastIndex < N )
            {
                int removeIndex = lastIndex + 1;
                // <k 时，概率是后期计算得到的
                if (removeIndex < K) currValue -= everOne * dp[removeIndex];
                // >=k 时，概率是固定值 1/W
                else currValue -= everOne;
            }
            dp[currK] = currValue;
        }
        return dp[0]; // 下标为0时，之前的概率为1，下标为 1 ~ W 时，之前的概率为 1/W。
    }
}
/*
class Solution {
    public double new21Game(int N, int K, int W) {
        double[] dp = new double[N + W + 1];
        // dp[x] = the answer when Alice has x points
        for (int k = K; k <= N; ++k)
            dp[k] = 1.0;

        double S = Math.min(N - K + 1, W);
        // S = dp[k+1] + dp[k+2] + ... + dp[k+W]
        for (int k = K - 1; k >= 0; --k) {
            dp[k] = S / W;
            S += dp[k] - dp[k + W];
        }
        return dp[0];
    }
}

作者：LeetCode
链接：https://leetcode-cn.com/problems/new-21-game/solution/xin-21dian-by-leetcode/
来源：力扣（LeetCode）
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。 
*/
