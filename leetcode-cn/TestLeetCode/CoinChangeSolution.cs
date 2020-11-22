using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定不同面额的硬币 coins 和一个总金额 amount。编写一个函数来计算可以凑成总金额所需的最少的硬币个数。如果没有任何一种硬币组合能组成总金额，返回 -1。

示例 1:

输入: coins = [1, 2, 5], amount = 11
输出: 3 
解释: 11 = 5 + 5 + 1
示例 2:

输入: coins = [2], amount = 3
输出: -1
说明:
你可以认为每种硬币的数量是无限的。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/coin-change/
/// 322. 零钱兑换
/// https://blog.csdn.net/fisherming/article/details/79839545
/// </summary>
class CoinChangeSolution
{
    public void Test()
    {
        //var ret = CoinChange(new int[] { 1, 2, 5 }, 11);
        var ret = CoinChange(new int[] { 1 }, 0);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int CoinChange(int[] coins, int amount)
    {
        if (amount < 1) return 0;
        _map = new Dictionary<int, int>();
        return search( amount, coins );
    }

    private Dictionary<int, int> _map = null;
    private int search( int amount, int[] coins )
    {
        var key = amount;
        if (_map.ContainsKey(key)) return _map[key];

        int subSteps = int.MaxValue;
        for (int i = 0; i < coins.Length; i++)
        {
            var v = amount - coins[i];
            if (v < 0) continue;
            if (v == 0) return _map[key] = 1;

            var s = search( v, coins );
            if ( 0 < s ) if (s < subSteps) subSteps = s;
        }
        return _map[key] = subSteps == int.MaxValue ? -1 : subSteps + 1;
    }
}
/*
零钱兑换
力扣官方题解
发布于 2020-03-05
106.1k
预备知识
搜索回溯
动态规划
方法一：搜索回溯 [超出时间限制]
该问题可建模为以下优化问题：

\min_{x} \sum_{i=0}^{n - 1} x_i \ \text{subject to} \sum_{i=0}^{n - 1} x_i*c_i = S
x
min
​	
  
i=0
∑
n−1
​	
 x 
i
​	
  subject to 
i=0
∑
n−1
​	
 x 
i
​	
 ∗c 
i
​	
 =S

其中，SS 是总金额，c_ic 
i
​	
  是第 ii 枚硬币的面值，x_ix 
i
​	
  是面值为 c_ic 
i
​	
  的硬币数量，由于 x_i*c_ix 
i
​	
 ∗c 
i
​	
  不能超过总金额 SS ，可以得出 x_ix 
i
​	
  最多不会超过 \frac{S}{c_i} 
c 
i
​	
 
S
​	
  ， 所以 x_ix 
i
​	
  的取值范围为[{0, \frac{S}{c_i}}][0, 
c 
i
​	
 
S
​	
 ]。

一个简单的解决方案是枚举每个硬币数量子集 [x_0\dots\ x_{n - 1}][x 
0
​	
 … x 
n−1
​	
 ] 。如果满足上述约束条件，计算硬币数量总和并返回所有子集中的最小值。

算法

为了应用这一思想，该算法使用搜索回溯生成满足上述约束条件范围 ([{0, \frac{S}{c_i}}])([0, 
c 
i
​	
 
S
​	
 ]) 内的所有硬币数量子集 [x_0\dots\ x_{n-1}][x 
0
​	
 … x 
n−1
​	
 ]。针对给定的子集计算它们组成的金额数，如果金额数为 SS，则记录返回合法硬币总数的最小值，反之返回 -1−1 。


public class Solution {
    public int coinChange(int[] coins, int amount) {
        return coinChange(0, coins, amount);
    }

    private int coinChange(int idxCoin, int[] coins, int amount) {
        if (amount == 0) {
            return 0;
        }
        if (idxCoin < coins.length && amount > 0) {
            int maxVal = amount / coins[idxCoin];
            int minCost = Integer.MAX_VALUE;
            for (int x = 0; x <= maxVal; x++) {
                if (amount >= x * coins[idxCoin]) {
                    int res = coinChange(idxCoin + 1, coins, amount - x * coins[idxCoin]);
                    if (res != -1) {
                        minCost = Math.min(minCost, res + x);
                    }
                }
            }
            return (minCost == Integer.MAX_VALUE)? -1: minCost;
        }
        return -1;
    }
}

// Time Limit Exceeded
复杂度分析

时间复杂度：O(S^n)O(S 
n
 )。在最坏的情况下，复杂性是硬币数量的指数 ，是因为每个硬币面值 c_ic 
i
​	
  最多可以有 \frac{S}{c_i} 
c 
i
​	
 
S
​	
  个。因此，可能的组合数为：
\frac{S}{c_1}\frac{S}{c_2}\frac{S}{c_3}\ldots\frac{S}{c_n} = \frac{S^{n}}{{c_1}{c_2}{c_3}\ldots{c_n}}
c 
1
​	
 
S
​	
  
c 
2
​	
 
S
​	
  
c 
3
​	
 
S
​	
 … 
c 
n
​	
 
S
​	
 = 
c 
1
​	
 c 
2
​	
 c 
3
​	
 …c 
n
​	
 
S 
n
 
​	
 

空间复杂度：O(n)O(n)，在最坏的情况下，递归的最大深度是 nn。因此，我们需要系统递归堆栈使用 O(n)O(n) 的空间。
方法二：动态规划-自上而下 [通过]
我们能改进上面的指数时间复杂度的解吗？当然可以，利用动态规划，我们可以在多项式的时间范围内求解。首先，我们定义：

F(S)F(S)：组成金额 SS 所需的最少硬币数量

[c_{0}\ldots c_{n-1}][c 
0
​	
 …c 
n−1
​	
 ] ：可选的 nn 枚硬币面额值

我们注意到这个问题有一个最优的子结构性质，这是解决动态规划问题的关键。最优解可以从其子问题的最优解构造出来。如何将问题分解成子问题？假设我们知道 F(S)F(S) ，即组成金额 SS 最少的硬币数，最后一枚硬币的面值是 CC。那么由于问题的最优子结构，转移方程应为：

F(S) = F(S - C) + 1
F(S)=F(S−C)+1

但我们不知道最后一枚硬币的面值是多少，所以我们需要枚举每个硬币面额值 c_0, c_1, c_2 \ldots c_{n -1}c 
0
​	
 ,c 
1
​	
 ,c 
2
​	
 …c 
n−1
​	
  并选择其中的最小值。下列递推关系成立：

F(S) = \min_{i=0 ... n-1}{ F(S - c_i) } + 1 \ \text{subject to} \ \ S-c_i \geq 0
F(S)= 
i=0...n−1
min
​	
 F(S−c 
i
​	
 )+1 subject to  S−c 
i
​	
 ≥0

F(S) = 0 \ , \text{when} \ S = 0
F(S)=0 ,when S=0

F(S) = -1 \ , \text{when} \ n = 0
F(S)=−1 ,when n=0



在上面的递归树中，我们可以看到许多子问题被多次计算。例如， F(1)F(1) 被计算了 1313 次。为了避免重复的计算，我们将每个子问题的答案存在一个数组中进行记忆化，如果下次还要计算这个问题的值直接从数组中取出返回即可，这样能保证每个子问题最多只被计算一次。


public class Solution {
    public int coinChange(int[] coins, int amount) {
        if (amount < 1) {
            return 0;
        }
        return coinChange(coins, amount, new int[amount]);
    }

    private int coinChange(int[] coins, int rem, int[] count) {
        if (rem < 0) {
            return -1;
        }
        if (rem == 0) {
            return 0;
        }
        if (count[rem - 1] != 0) {
            return count[rem - 1];
        }
        int min = Integer.MAX_VALUE;
        for (int coin : coins) {
            int res = coinChange(coins, rem - coin, count);
            if (res >= 0 && res < min) {
                min = 1 + res;
            }
        }
        count[rem - 1] = (min == Integer.MAX_VALUE) ? -1 : min;
        return count[rem - 1];
    }
}
复杂度分析

时间复杂度：O(Sn)O(Sn)，其中 SS 是金额，nn 是面额数。我们一共需要计算 SS 个状态的答案，且每个状态 F(S)F(S) 由于上面的记忆化的措施只计算了一次，而计算一个状态的答案需要枚举 nn 个面额值，所以一共需要 O(Sn)O(Sn) 的时间复杂度。
空间复杂度：O(S)O(S)，我们需要额外开一个长为 SS 的数组来存储计算出来的答案 F(S)F(S) 。
方法三：动态规划：自下而上 [通过]
算法

我们采用自下而上的方式进行思考。仍定义 F(i)F(i) 为组成金额 ii 所需最少的硬币数量，假设在计算 F(i)F(i) 之前，我们已经计算出 F(0)-F(i-1)F(0)−F(i−1) 的答案。 则 F(i)F(i) 对应的转移方程应为

F(i)=\min_{j=0 \ldots n-1}{F(i -c_j)} + 1
F(i)= 
j=0…n−1
min
​	
 F(i−c 
j
​	
 )+1

其中 c_jc 
j
​	
  代表的是第 jj 枚硬币的面值，即我们枚举最后一枚硬币面额是 c_jc 
j
​	
 ，那么需要从 i-c_ji−c 
j
​	
  这个金额的状态 F(i-c_j)F(i−c 
j
​	
 ) 转移过来，再算上枚举的这枚硬币数量 11 的贡献，由于要硬币数量最少，所以 F(i)F(i) 为前面能转移过来的状态的最小值加上枚举的硬币数量 11 。

例子1：假设


coins = [1, 2, 5], amount = 11
则，当 i==0i==0 时无法用硬币组成，为 0 。当 i<0i<0 时，忽略 F(i)F(i)

F(i)	最小硬币数量
F(0)	0 //金额为0不能由硬币组成
F(1)	1 //F(1)=min(F(1-1),F(1-2),F(1-5))+1=1F(1)=min(F(1−1),F(1−2),F(1−5))+1=1
F(2)	1 //F(2)=min(F(2-1),F(2-2),F(2-5))+1=1F(2)=min(F(2−1),F(2−2),F(2−5))+1=1
F(3)	2 //F(3)=min(F(3-1),F(3-2),F(3-5))+1=2F(3)=min(F(3−1),F(3−2),F(3−5))+1=2
F(4)	2 //F(4)=min(F(4-1),F(4-2),F(4-5))+1=2F(4)=min(F(4−1),F(4−2),F(4−5))+1=2
...	...
F(11)	3 //F(11)=min(F(11-1),F(11-2),F(11-5))+1=3F(11)=min(F(11−1),F(11−2),F(11−5))+1=3
我们可以看到问题的答案是通过子问题的最优解得到的。

例子2：假设


coins = [1, 2, 3], amount = 6
在这里插入图片描述

在上图中，可以看到：

\begin{aligned} F(3) &= \min({F(3- c_1), F(3-c_2), F(3-c_3)}) + 1 \\ &= \min({F(3- 1), F(3-2), F(3-3)}) + 1 \\ &= \min({F(2), F(1), F(0)}) + 1 \\ &= \min({1, 1, 0}) + 1 \\ &= 1 \end{aligned}
F(3)
​	
  
=min(F(3−c 
1
​	
 ),F(3−c 
2
​	
 ),F(3−c 
3
​	
 ))+1
=min(F(3−1),F(3−2),F(3−3))+1
=min(F(2),F(1),F(0))+1
=min(1,1,0)+1
=1
​	
 


public class Solution {
    public int coinChange(int[] coins, int amount) {
        int max = amount + 1;
        int[] dp = new int[amount + 1];
        Arrays.fill(dp, max);
        dp[0] = 0;
        for (int i = 1; i <= amount; i++) {
            for (int j = 0; j < coins.length; j++) {
                if (coins[j] <= i) {
                    dp[i] = Math.min(dp[i], dp[i - coins[j]] + 1);
                }
            }
        }
        return dp[amount] > amount ? -1 : dp[amount];
    }
}
复杂度分析

时间复杂度：O(Sn)O(Sn)，其中 SS 是金额，nn 是面额数。我们一共需要计算 O(S)O(S) 个状态，SS 为题目所给的总金额。对于每个状态，每次需要枚举 nn 个面额来转移状态，所以一共需要 O(Sn)O(Sn) 的时间复杂度。
空间复杂度：O(S)O(S)。DP 数组需要开长度为总金额 SS 的空间。

void coinChange(vector<int>& coins, int amount, int c_index, int count, int& ans)
{
    if (amount == 0)
    {
        ans = min(ans, count);
        return;
    }
    if (c_index == coins.size()) return;

    for (int k = amount / coins[c_index]; k >= 0 && k + count < ans; k--)
    {
        coinChange(coins, amount - k * coins[c_index], c_index + 1, count + k, ans);
    }
}

int coinChange(vector<int>& coins, int amount)
{
    if (amount == 0) return 0;
    sort(coins.rbegin(), coins.rend());
    int ans = INT_MAX;
    coinChange(coins, amount, 0, 0, ans);
    return ans == INT_MAX ? -1 : ans;
}


public class Solution {
	public int CoinChange(int[] coins, int amount) {
		if (coins == null || coins.Length == 0 || amount < 0)
			return -1;
		int[] coinsComb = new int[amount + 1];
		coinsComb[0] = 0;
		for (int i = 1; i <= amount; i++)
		{
			coinsComb[i] = amount + 1;
		}
		for (int i = 1; i <= amount; i++)
		{
			foreach (int coin in coins)
			{
				if (i - coin >= 0) 
				{
					coinsComb [i] = Math.Min(coinsComb [i] ,coinsComb [i - coin] + 1);
				}
			}
		}
		return coinsComb[amount] > amount? -1:coinsComb[amount];
	}
}
public class Solution {
    public int CoinChange(int[] coins, int amount) {
        if (amount==0)
        {
            return 0;
        }
        Array.Sort(coins);  //面值右小到大排序
        int[] d = new int[amount + 1]; //d[i]表示组成价格i至少需要多少个硬币 d[0]=0;
        for (int i = 1; i <= amount; i++)
        {
            int minNum = Int16.MaxValue;  //最少硬币个数
            for (int j = 0; j < coins.Length; j++)
            {
                if (i == coins[j])
                {
                    d[i] = 1;
                    minNum = 1;
                    break;
                }
                else
                {
                    if (i - coins[j] > 0)
                    {
                        if (d[i - coins[j]] > 0)
                        {
                            if (d[i - coins[j]] + 1 < minNum)
                            {
                                minNum = d[i - coins[j]] + 1;
                            }
                        }
                    }
                }
            }
            if (minNum != Int16.MaxValue)
            {
                d[i] = minNum;
            }
        }
        if (d[amount] == 0)
        {
            return -1;
        }

        return d[amount];
    }
}
*/
