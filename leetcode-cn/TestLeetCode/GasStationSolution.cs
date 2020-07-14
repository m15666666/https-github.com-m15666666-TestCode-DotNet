/*
在一条环路上有 N 个加油站，其中第 i 个加油站有汽油 gas[i] 升。

你有一辆油箱容量无限的的汽车，从第 i 个加油站开往第 i+1 个加油站需要消耗汽油 cost[i] 升。你从其中的一个加油站出发，开始时油箱为空。

如果你可以绕环路行驶一周，则返回出发时加油站的编号，否则返回 -1。

说明: 

如果题目有解，该答案即为唯一答案。
输入数组均为非空数组，且长度相同。
输入数组中的元素均为非负数。
示例 1:

输入:
gas  = [1,2,3,4,5]
cost = [3,4,5,1,2]

输出: 3

解释:
从 3 号加油站(索引为 3 处)出发，可获得 4 升汽油。此时油箱有 = 0 + 4 = 4 升汽油
开往 4 号加油站，此时油箱有 4 - 1 + 5 = 8 升汽油
开往 0 号加油站，此时油箱有 8 - 2 + 1 = 7 升汽油
开往 1 号加油站，此时油箱有 7 - 3 + 2 = 6 升汽油
开往 2 号加油站，此时油箱有 6 - 4 + 3 = 5 升汽油
开往 3 号加油站，你需要消耗 5 升汽油，正好足够你返回到 3 号加油站。
因此，3 可为起始索引。
示例 2:

输入:
gas  = [2,3,4]
cost = [3,4,3]

输出: -1

解释:
你不能从 0 号或 1 号加油站出发，因为没有足够的汽油可以让你行驶到下一个加油站。
我们从 2 号加油站出发，可以获得 4 升汽油。 此时油箱有 = 0 + 4 = 4 升汽油
开往 0 号加油站，此时油箱有 4 - 3 + 2 = 3 升汽油
开往 1 号加油站，此时油箱有 3 - 3 + 3 = 3 升汽油
你无法返回 2 号加油站，因为返程需要消耗 4 升汽油，但是你的油箱只有 3 升汽油。
因此，无论怎样，你都不可能绕环路行驶一周。

*/

/// <summary>
/// https://leetcode-cn.com/problems/gas-station/
/// 134.加油站
/// 在一条环路上有 N 个加油站，其中第 i 个加油站有汽油 gas[i] 升。
/// 你有一辆油箱容量无限的的汽车，从第 i 个加油站开往第 i+1 个加油站需要消耗汽油 cost[i] 升。你从其中的一个加油站出发，开始时油箱为空。
/// 如果你可以绕环路行驶一周，则返回出发时加油站的编号，否则返回 -1。
/// 说明:
/// 如果题目有解，该答案即为唯一答案。
/// 输入数组均为非空数组，且长度相同。
/// 输入数组中的元素均为非负数。
/// </summary>
internal class GasStationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int n = gas.Length;
        int total = 0;
        int current = 0;
        int startStation = 0;
        for (int i = 0; i < n; ++i)
        {
            var diff = gas[i] - cost[i];
            total += diff;
            current += diff;
            if (current < 0)
            {
                startStation = i + 1;
                current = 0;
            }
        }
        return 0 <= total ? startStation : -1;
    }

    //public int CanCompleteCircuit(int[] gas, int[] cost)
    //{
    //    if ( gas == null || cost == null || gas.Length == 0 || gas.Length != cost.Length ) return -1;

    //    var length = gas.Length;
    //    for( var startIndex = 0; startIndex < length; startIndex++ )
    //    {
    //        int currentIndex = startIndex;
    //        int gasSum = 0;

    //        while( true )
    //        {
    //            var currentCost = cost[currentIndex];
    //            gasSum += gas[currentIndex];

    //            if (gasSum < currentCost) break;

    //            gasSum -= currentCost;

    //            int nextIndex = (currentIndex + 1) % length;
    //            if (nextIndex == startIndex) return startIndex;

    //            currentIndex = nextIndex;
    //        }
    //    }
    //    return -1;
    //}
}

/*

加油站
力扣 (LeetCode)
发布于 2019-06-01
31.5k
方法：一次遍历
想法

第一想法是检查每一个加油站：

选择该加油站为出发站

模拟汽车环路行驶，在每一个加油站检查我们还剩多少升汽油。

这意味着 \mathcal{O}(N^2)O(N 
2
 ) 的时间复杂度。显然，我们可以做得更好。

首先注意两件事情：

如果 sum(gas) < sum(cost) ，那么不可能环行一圈，这种情况下答案是 -1 。

image.png

我们可以用这个式子计算环行过程中邮箱里剩下的油：total_tank = sum(gas) - sum(cost) ，如果 total_tank < 0 则返回 -1 。

对于加油站 i ，如果 gas[i] - cost[i] < 0 ，则不可能从这个加油站出发，因为在前往 i + 1 的过程中，汽油就不够了。

image.png

第二个规则可以被一般化，我们引入变量 curr_tank ，记录当前油箱里剩余的总油量。如果在某一个加油站 curr_tank比 0 小，意味着我们无法到达这个加油站。

下一步我们把这个加油站当做新的起点，并将 curr_tank 重置为 0 ，因为重新出发，油箱中的油为 0 。（从上一次重置的加油站到当前加油站的任意一个加油站出发，到达当前加油站之前， curr_tank 也一定会比 0 小）

算法

那么现在算法是很直接明了的：

初始化 total_tank 和 curr_tank 为 0 ，并且选择 0 号加油站为起点。

遍历所有的加油站：

每一步中，都通过加上 gas[i] 和减去 cost[i] 来更新 total_tank 和 curr_tank 。

如果在 i + 1 号加油站， curr_tank < 0 ，将 i + 1 号加油站作为新的起点，同时重置 curr_tank = 0 ，让油箱也清空。

如果 total_tank < 0 ，返回 -1 ，否则返回 starting station。

算法原理

想象 total_tank >= 0 的情况，同时上述算法返回 N_sN 
s
​	
  作为出发加油站。

算法直接保证了从 N_sN 
s
​	
  可以到达 00 ，但是剩余的路程，即从 00 到站 N_sN 
s
​	
  是否有足够的油呢？

如何确保从 N_sN 
s
​	
  出发可以环行一圈？

我们使用 反证法 。假设存在 0 < k < N_s0<k<N 
s
​	
  ，使得我们从 N_sN 
s
​	
  出发无法到达 k 号加油站。

条件 total_tank >= 0 可以被写作

\sum_{i = 1}^{N}{\alpha_i} \ge 0 \qquad (1)
i=1
∑
N
​	
 α 
i
​	
 ≥0(1)

其中 \alpha_i = \textrm{gas[i]} - \textrm{cost[i]}α 
i
​	
 =gas[i]−cost[i] 。

我们将出发站点 N_sN 
s
​	
  和无法到达站点 k 作为分隔点，将左式分成三个部分：

\sum_{i = 1}^{k}{\alpha_i} + \sum_{i = k + 1}^{N_s - 1}{\alpha_i} + \sum_{i = N_s}^{N}{\alpha_i} \ge 0 \qquad (2)
i=1
∑
k
​	
 α 
i
​	
 + 
i=k+1
∑
N 
s
​	
 −1
​	
 α 
i
​	
 + 
i=N 
s
​	
 
∑
N
​	
 α 
i
​	
 ≥0(2)

根据算法流程，第二项为负，因为每一个出发点前面一段路途的 curr_tank 一定为负。否则，出发点应该是比 N_sN 
s
​	
  更靠前的一个加油站而不是 N_sN 
s
​	
  。当且仅当 k = N_s - 1k=N 
s
​	
 −1 ，第二项才为 0 。

\sum_{i = k + 1}^{i = N_s - 1}{\alpha_i} \le 0 \qquad (3)
i=k+1
∑
i=N 
s
​	
 −1
​	
 α 
i
​	
 ≤0(3)

结合不等式 (2) 和 (3) ，可以得到

\sum_{i = 0}^{i = k}{\alpha_i} + \sum_{i = N_s}^{i = N}{\alpha_i} \ge 0 \qquad (4)
i=0
∑
i=k
​	
 α 
i
​	
 + 
i=N 
s
​	
 
∑
i=N
​	
 α 
i
​	
 ≥0(4)

同时，因为 kk 是一个从 N_sN 
s
​	
  出发不可到达的站点，意味着

\sum_{i = N_s}^{i = N}{\alpha_i} + \sum_{i = 0}^{i = k}{\alpha_i} < 0 \qquad (5)
i=N 
s
​	
 
∑
i=N
​	
 α 
i
​	
 + 
i=0
∑
i=k
​	
 α 
i
​	
 <0(5)

结合不等式 (4) 和 (5) ，可以得到一个矛盾。因此，假设 “存在一个 0 < k < N_s0<k<N 
s
​	
  ，从 N_sN 
s
​	
  出发无法到达 kk” 不成立。

因此，从 N_sN 
s
​	
  出发一定能环行一圈， N_sN 
s
​	
  是一个可行解。根据题目描述，答案是唯一的。

实现

class Solution {
  public int canCompleteCircuit(int[] gas, int[] cost) {
    int n = gas.length;

    int total_tank = 0;
    int curr_tank = 0;
    int starting_station = 0;
    for (int i = 0; i < n; ++i) {
      total_tank += gas[i] - cost[i];
      curr_tank += gas[i] - cost[i];
      // If one couldn't get here,
      if (curr_tank < 0) {
        // Pick up the next station as the starting one.
        starting_station = i + 1;
        // Start with an empty tank.
        curr_tank = 0;
      }
    }
    return total_tank >= 0 ? starting_station : -1;
  }
}

class Solution:
    def canCompleteCircuit(self, gas, cost):
        """
        :type gas: List[int]
        :type cost: List[int]
        :rtype: int
        """
        n = len(gas)
        
        total_tank, curr_tank = 0, 0
        starting_station = 0
        for i in range(n):
            total_tank += gas[i] - cost[i]
            curr_tank += gas[i] - cost[i]
            # If one couldn't get here,
            if curr_tank < 0:
                # Pick up the next station as the starting one.
                starting_station = i + 1
                # Start with an empty tank.
                curr_tank = 0
        
        return starting_station if total_tank >= 0 else -1
复杂度分析

时间复杂度： \mathcal{O}(N)O(N) ， 这是因为只有一个遍历了所有加油站一次的循环。

空间复杂度： \mathcal{O}(1)O(1) ，因为此算法只使用了常数个变量。

延伸阅读

还有许多加油站问题的变种问题，这里是一些例子：

允许 Δ 次停留的加油站间最小路径开销

油箱有容量限制下的加油站间最小路径开销

下一篇：使用图的思想分析该问题

public class Solution {
    public int CanCompleteCircuit(int[] gas, int[] cost) {
        int iMin = -1, min = int.MaxValue, budget = 0;
            for(int i = 0; i < gas.Length; i++)
            {
                gas[i] -= cost[i];
                budget += gas[i];
                if(budget < min)
                {
                    min = budget;
                    iMin = i;
                }
            }

            return budget >= 0 ? (iMin + 1)%gas.Length : -1;
    }
}


*/
