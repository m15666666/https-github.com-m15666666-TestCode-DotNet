using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你的初始能量为 P，初始分数为 0，只有一包令牌。

令牌的值为 token[i]，每个令牌最多只能使用一次，可能的两种使用方法如下：

如果你至少有 token[i] 点能量，可以将令牌置为正面朝上，失去 token[i] 点能量，并得到 1 分。
如果我们至少有 1 分，可以将令牌置为反面朝上，获得 token[i] 点能量，并失去 1 分。
在使用任意数量的令牌后，返回我们可以得到的最大分数。

示例 1：

输入：tokens = [100], P = 50
输出：0
示例 2：

输入：tokens = [100,200], P = 150
输出：1
示例 3：

输入：tokens = [100,200,300,400], P = 200
输出：2

提示：

tokens.length <= 1000
0 <= tokens[i] < 10000
0 <= P < 10000
*/
/// <summary>
/// https://leetcode-cn.com/problems/bag-of-tokens/
/// 948. 令牌放置
/// 
/// </summary>
class BagOfTokensSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int BagOfTokensScore(int[] tokens, int P)
    {
        Array.Sort(tokens);

        int lo = 0;
        int hi = tokens.Length - 1;
        int points = 0;
        int ret = 0;
        while (lo <= hi && (P >= tokens[lo] || points > 0))
        {
            while (lo <= hi && P >= tokens[lo])
            {
                P -= tokens[lo++];
                points++;
            }

            ret = Math.Max(ret, points);
            if (lo <= hi && points > 0)
            {
                P += tokens[hi--];
                points--;
            }
        }

        return ret;
    }
}
/*
方法一： 贪心
思路

如果让我们来玩令牌放置这个游戏，在让令牌正面朝上的时候，肯定要去找能量最小的令牌。同样的，在让令牌反面朝上的时候，肯定要去找能量最大的令牌。

算法

只要还有能量，就一直让令牌正面朝上，直到没有能量的时候，让一个令牌反面朝上从而获得能量继续之前的操作。

一定要小心处理边界条件，不然很有可能会写出错误的答案。这里要牢牢记住，在有能量时候，只能让令牌正面朝上，直到能量不够用了才能让令牌反面朝上。

最终答案一定是在一次让令牌正常朝上操作之后产生的（永远不可能在让令牌反面朝上操作之后产生）

JavaPython
class Solution {
    public int bagOfTokensScore(int[] tokens, int P) {
        Arrays.sort(tokens);
        int lo = 0, hi = tokens.length - 1;
        int points = 0, ans = 0;
        while (lo <= hi && (P >= tokens[lo] || points > 0)) {
            while (lo <= hi && P >= tokens[lo]) {
                P -= tokens[lo++];
                points++;
            }

            ans = Math.max(ans, points);
            if (lo <= hi && points > 0) {
                P += tokens[hi--];
                points--;
            }
        }

        return ans;
    }
}
复杂度分析

时间复杂度： O(N \log N)O(NlogN)，其中 NN 是 tokens 的大小。

空间复杂度： O(N)O(N)。
 
*/
