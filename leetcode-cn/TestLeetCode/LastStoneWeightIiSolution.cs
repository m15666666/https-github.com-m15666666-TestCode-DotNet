using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有一堆石头，每块石头的重量都是正整数。

每一回合，从中选出任意两块石头，然后将它们一起粉碎。假设石头的重量分别为 x 和 y，且 x <= y。那么粉碎的可能结果如下：

如果 x == y，那么两块石头都会被完全粉碎；
如果 x != y，那么重量为 x 的石头将会完全粉碎，而重量为 y 的石头新重量为 y-x。
最后，最多只会剩下一块石头。返回此石头最小的可能重量。如果没有石头剩下，就返回 0。

 

示例：

输入：[2,7,4,1,8,1]
输出：1
解释：
组合 2 和 4，得到 2，所以数组转化为 [2,7,1,8,1]，
组合 7 和 8，得到 1，所以数组转化为 [2,1,1,1]，
组合 2 和 1，得到 1，所以数组转化为 [1,1,1]，
组合 1 和 1，得到 0，所以数组转化为 [1]，这就是最优值。
 

提示：

1 <= stones.length <= 30
1 <= stones[i] <= 1000
*/
/// <summary>
/// https://leetcode-cn.com/problems/last-stone-weight-ii/
/// 1049. 最后一块石头的重量 II
/// 
/// </summary>
class LastStoneWeightIiSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LastStoneWeightII(int[] stones)
    {
        int sum = stones.Sum();
        bool[] dp = new bool[sum / 2 + 1];
        Array.Fill(dp, false);
        dp[0] = true;

        foreach (int stone in stones)
            for (int space = dp.Length - 1; stone <= space; space--)
                dp[space] = dp[space] || dp[space - stone];

        for (int space = dp.Length - 1; -1 < space; space--)
            if (dp[space]) return sum - 2 * space;

        throw new ArgumentOutOfRangeException();
    }
}
/*
稍微脑筋转个弯，用时击败100，内存击败100%的用户
鲜嫩多汁老羊驼
781 阅读
如果把一个例子写成算式，会发现其实是用加号和减号把石头门的重量连起来，并使结果最小：
例子[2,7,4,1,8,1]中：
“组合 2 和 4，得到 2，所以数组转化为 [2,7,1,8,1]，
组合 7 和 8，得到 1，所以数组转化为 [2,1,1,1]，
组合 2 和 1，得到 1，所以数组转化为 [1,1,1]，
组合 1 和 1，得到 0，所以数组转化为 [1]，这就是最优值。
”
即
1-（（4-2）-（8-7））
也就是
1+2+8-4-7

换一种想法，就是 将这些数字分成两拨，使得他们的和的差最小

在进一步，可以变成 选出一些数字，使得它们最逼近整个数组和除以二的值，而最后的结果，就是abs（这个数-总和除以二）*2

那么现在，就可以写代码了：

class Solution:
    def lastStoneWeightII(self, stones: List[int]) -> int:
        target = sum(stones)/2.0
        candidates = {0}
        for x in stones:
            addition = set()
            for y in candidates:
                if x+y<= target:
                    addition.add(x+y)
            candidates = candidates.union(addition)
        return int(2*(target-max(candidates)))
执行用时 :52 ms, 在所有 Python3 提交中击败了97.78%的用户 内存消耗 :13.9 MB, 在所有 Python3 提交中击败了100.00%的用户

再优化一下：

class Solution:
    def lastStoneWeightII(self, stones: List[int]) -> int:
        target = sum(stones)/2.0
        candidates = {0}
        for x in stones:
            addition = set()
            for y in candidates:
                s = x+y
                if s==target:
                    return 0
                elif x+y< target:
                    addition.add(s)
            candidates = candidates.union(addition)
        return int(2*(target-max(candidates)))

C++ 动态规划，0-1背包
陌路独行
542 阅读
// 因为挑选石头是任意的, 不能使用贪心法每次挑选重量最大的两块石头
// 第一次挑选a,b, 放回a-b, ....., 第n次挑选c,a-b, 放回c-a+b， 最终结果为(a+d+c+g)-(b+e+f)
// 因此, 可以视作一个0-1背包问题，将石头分为两堆，两堆重量之差最小是多少
// 背包容量为 sum/2， 每个石头拿起来或者不拿起来，能装下最多的石头重量是多少
int lastStoneWeightII(vector<int> &stones){
    int sum = accumulate(stones.begin(), stones.end(), 0);
    vector<bool> dp(sum / 2 + 1, false);    // dp[i] - 是否可以找到一部分石头，其总重量为i
    dp[0] = true;                           // dp[0] - 不拿石头时，其总重量为0
    
    for(int i = 0; i < stones.size(); ++i)
        for(int w = dp.size() - 1; w >= stones[i]; --w)
            dp[w] = dp[w] | dp[w - stones[i]];
    
    // 找到可以放的最大重量假设为i, 则两部分的差值为 (sum - i) - i
    for(int i = dp.size() - 1; i >= 0; --i)
        if(dp[i] == true)
            return sum - 2 * i;
    
    return sum;
} 

public class Solution
    {
        private int[,] dp;

        private int half;

        private int dfs(int[] stones, int curIdx, int curSum)
        {
            if (curIdx >= stones.Length)
            {
                return curSum;
            }

            if (dp[curIdx, curSum] != 0)
            {
                return dp[curIdx, curSum];
            }
            int choosed = dfs(stones, curIdx + 1, curSum + stones[curIdx]);
            int unchoosed = dfs(stones, curIdx + 1, curSum);
            if (Math.Abs(choosed - half) > Math.Abs(unchoosed - half))
            {
                dp[curIdx, curSum] = unchoosed;
                return unchoosed;
            }
            else
            {
                dp[curIdx, curSum] = unchoosed;
                return choosed;
            }
        }

        public int LastStoneWeightII(int[] stones)
        {
            //问题本质：将元素分成两堆，使得Sum1 - Sum2最小
            
            half = stones.Sum() / 2;
            int total = stones.Sum();
            dp = new int[stones.Length, total + 1];
            int result = dfs(stones, 0, 0);
            return Math.Abs(total- result - result);
        }
    }

public class Solution {
    public int LastStoneWeightII(int[] stones) {
        int len = stones.Length;
        int sum = 0;
        foreach (int i in stones) {
            sum += i;
        }
        int maxCapacity = sum/2;
        int[] dp = new int[maxCapacity + 1];
        for (int i = 0; i < len; i++) {
            int curStone = stones[i];
            for (int j = maxCapacity; j >= curStone; j--) {
                dp[j] = Math.Max(dp[j], dp[j-curStone] + curStone);
            }
        }
        return sum - 2 * dp[maxCapacity];
    }
}
*/
