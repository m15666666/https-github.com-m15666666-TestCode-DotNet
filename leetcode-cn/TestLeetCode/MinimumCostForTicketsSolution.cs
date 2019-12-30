using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在一个火车旅行很受欢迎的国度，你提前一年计划了一些火车旅行。在接下来的一年里，你要旅行的日子将以一个名为 days 的数组给出。每一项是一个从 1 到 365 的整数。

火车票有三种不同的销售方式：

一张为期一天的通行证售价为 costs[0] 美元；
一张为期七天的通行证售价为 costs[1] 美元；
一张为期三十天的通行证售价为 costs[2] 美元。
通行证允许数天无限制的旅行。 例如，如果我们在第 2 天获得一张为期 7 天的通行证，那么我们可以连着旅行 7 天：第 2 天、第 3 天、第 4 天、第 5 天、第 6 天、第 7 天和第 8 天。

返回你想要完成在给定的列表 days 中列出的每一天的旅行所需要的最低消费。

示例 1：

输入：days = [1,4,6,7,8,20], costs = [2,7,15]
输出：11
解释： 
例如，这里有一种购买通行证的方法，可以让你完成你的旅行计划：
在第 1 天，你花了 costs[0] = $2 买了一张为期 1 天的通行证，它将在第 1 天生效。
在第 3 天，你花了 costs[1] = $7 买了一张为期 7 天的通行证，它将在第 3, 4, ..., 9 天生效。
在第 20 天，你花了 costs[0] = $2 买了一张为期 1 天的通行证，它将在第 20 天生效。
你总共花了 $11，并完成了你计划的每一天旅行。
示例 2：

输入：days = [1,2,3,4,5,6,7,8,9,10,30,31], costs = [2,7,15]
输出：17
解释：
例如，这里有一种购买通行证的方法，可以让你完成你的旅行计划： 
在第 1 天，你花了 costs[2] = $15 买了一张为期 30 天的通行证，它将在第 1, 2, ..., 30 天生效。
在第 31 天，你花了 costs[0] = $2 买了一张为期 1 天的通行证，它将在第 31 天生效。 
你总共花了 $17，并完成了你计划的每一天旅行。
 

提示：

1 <= days.length <= 365
1 <= days[i] <= 365
days 按顺序严格递增
costs.length == 3
1 <= costs[i] <= 1000
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-cost-for-tickets/
/// 983. 最低票价
/// 
/// </summary>
class MinimumCostForTicketsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int MincostTickets(int[] days, int[] costs)
    {
        _costs = costs;
        
        _dp = new int[MaxDay + 1];
        Array.Fill(_dp, -1);

        _dayset = new HashSet<int>();
        foreach (int d in days) _dayset.Add(d);

        return Dp(1);
    }

    const int MaxDay = 365;
    private int[] _costs;
    private int[] _dp;
    private HashSet<int> _dayset;
    private int Dp(int day)
    {
        if (day > MaxDay) return 0;

        if (_dp[day] != -1) return _dp[day];

        int nextDayCost = Dp(day + 1); // day 通过 day+1首先递推到365天

        int cost;
        if (_dayset.Contains(day))
        {
            cost = Math.Min(nextDayCost + _costs[0],  Dp(day + 7) + _costs[1]);
            cost = Math.Min(cost, Dp(day + 30) + _costs[2]);
        }
        else
        {
            cost = nextDayCost;
        }

        _dp[day] = cost;
        return cost;
    }
}
/*
方法一：动态规划（日期变量型）
思路与算法

某天，如果你不必出行的话，等一等再购买火车票一定更优，如果你需要出行的话，那么就有三种选择：在通行期为 1 天、7 天、30 天中的火车票中选择一张购买。

我们可以把这种选择的过程表示成递归的形式，然后使用动态规划解决（记忆化搜索）。我们定义 dp(i) 为能够完成从第 i 天到最后的旅游计划的最小花费。那么，如果你在第 i 天需要出行的话，你的花费为：

\text{dp}(i) = \min(\text{dp}(i+1) + \text{costs}[0], \text{dp}(i+7) + \text{costs}[1], \text{dp}(i+30) + \text{costs}[2])
dp(i)=min(dp(i+1)+costs[0],dp(i+7)+costs[1],dp(i+30)+costs[2])

JavaPython3
class Solution {
    int[] costs;
    Integer[] memo;
    Set<Integer> dayset;

    public int mincostTickets(int[] days, int[] costs) {
        this.costs = costs;
        memo = new Integer[366];
        dayset = new HashSet();
        for (int d: days) dayset.add(d);

        return dp(1);
    }

    public int dp(int i) {
        if (i > 365)
            return 0;
        if (memo[i] != null)
            return memo[i];

        int ans;
        if (dayset.contains(i)) {
            ans = Math.min(dp(i+1) + costs[0],
                               dp(i+7) + costs[1]);
            ans = Math.min(ans, dp(i+30) + costs[2]);
        } else {
            ans = dp(i+1);
        }

        memo[i] = ans;
        return ans;
    }
}

复杂度分析

时间复杂度：O(W)O(W)，其中 W = 365W=365 是旅行计划中日期的最大值。

空间复杂度：O(W)O(W)。

方法二：动态规划（窗口变量型）
思路与算法

在 方法一 中，我们只需要在有出行需求的日期购买火车票就可以了。

现在，我们令 dp(i) 表示能够完成从 days[i] 到最后的旅行计划的最小花费。如果说 j1 是最大的下标满足 days[j1] < days[i] + 1，j7 是最大的下标满足 days[j7] < days[i] + 7， j30 是最大的下标满足 days[j30] < days[i] + 30，那么就有：

\text{dp}(i) = \min(\text{dp}(j1) + \text{costs}[0], \text{dp}(j7) + \text{costs}[1], \text{dp}(j30) + \text{costs}[2])
dp(i)=min(dp(j1)+costs[0],dp(j7)+costs[1],dp(j30)+costs[2])

JavaPython3
class Solution {
    int[] days, costs;
    Integer[] memo;
    int[] durations = new int[]{1, 7, 30};

    public int mincostTickets(int[] days, int[] costs) {
        this.days = days;
        this.costs = costs;
        memo = new Integer[days.length];

        return dp(0);
    }

    public int dp(int i) {
        if (i >= days.length)
            return 0;
        if (memo[i] != null)
            return memo[i];

        int ans = Integer.MAX_VALUE;
        int j = i;
        for (int k = 0; k < 3; ++k) {
            while (j < days.length && days[j] < days[i] + durations[k])
                j++;
            ans = Math.min(ans, dp(j) + costs[k]);
        }

        memo[i] = ans;
        return ans;
    }
}
复杂度分析

时间复杂度：O(N)O(N)，其中 NN 是旅行计划中不同出行日期的数量。

空间复杂度：O(N)O(N)。

下一篇：C++动态规划
? 著作权归作者所有
4
条评论

最热

编辑
预览







评论
antsAndCoffee
1 个月前
菜虚坤赞我

2
踩
回复
uxlsl
7 个月前
想不出!

3
踩
回复
mike-meng
9 天前
可以用二分查找来加快速度。解法如下：

class Solution {
public:
    int mincostTickets(vector<int>& days, vector<int>& costs) {
        int n = days.size();
        int last[3] = {1,7,30};
        vector<int> dp(n+1,INT_MAX);
        dp[0] = 0;
        
        for(int i = 1; i <= n; ++i){
            for(int j = 0; j < 3; ++j){
                auto it = upper_bound(days.begin(),days.end(),days[i-1]-last[j]);
                dp[i] = min(dp[i],dp[it-days.begin()] + costs[j]);
            }
        }
        
        return dp[n];
    }
};
赞
踩
回复
ccnuacmhdu
1 个月前
记忆化搜索，思路简单，容易想：

class Solution {
    // 暴力递归:对每个旅行日可以买1日票，7日票，30日票。再改成记忆化搜索
    public int mincostTickets(int[] days, int[] costs) {
        int[] dp = new int[366];
        return f(days, costs, 1, dp);      
    }
    // 从st天开始，到最后一个旅行日，所需最小费用
    private int f(int[] days, int[] costs, int st, int[] dp){
        // 二分找到大于等于st的最小旅行日，从该旅行日开始
        int loc = bi(days, st);
        if(loc == -1){
            return 0;
        }
        if(dp[st] != 0){
            return dp[st];
        }
        // 1. 买1日票
        int cost1 = costs[0] + f(days, costs, days[loc]+1, dp);
        // 2. 买7日票
        int cost2 = costs[1] + f(days, costs, days[loc]+7, dp);
        // 3. 买30日票
        int cost3 = costs[2] + f(days, costs, days[loc]+30, dp);
        return dp[st] = Math.min(cost1, Math.min(cost2, cost3));
    }
    // 二分查找大于等于v的最小值的位置
    private int bi(int[] a, int v){
        int l = 0;
        int r = a.length - 1;
        int res = -1;
        while(l <= r){
            int mid = l + ((r-l)>>1);
            if(a[mid] >= v){
                res = mid;
                r = mid - 1;
            }else{
                l = mid + 1;
            }
        }
        return res;
    }
}

public class Solution {
    public int MincostTickets(int[] days, int[] costs) {
   // 将从新年到某一天的花过的所有钱数全部记录起来。
        int[] lastAllDaysCost = new int[366];
        //  days的下标，确保遍历365天时，以便于知道下次旅游的日期。
        int dayIdx = 0;
        // 日，月，年的花费。
        //int ticketDay = costs[0];
        //int ticketWeek = costs[1];
        //int ticketMonth = costs[2];
        // 因为是第一天，所以过去的总花费为0
        lastAllDaysCost[0] = 0;
        // lastAllCost[i] 是截至到今年的第 i 天的总花费.
        
        // 模拟新年的第一天跑到旅行的最后一天。
        for (int today = days[0]; today <= days[days.Length-1]; today++) {
            if(dayIdx >= days.Length){
                break;
            }
            // 判断今天是否属于旅行日。
            if (days[dayIdx] != today) {
                // 如果这一天不旅行那么直接把上一天的过去总花费拿过来直接使用。
                lastAllDaysCost[today] = lastAllDaysCost[today - 1];
                continue;
            }
            // 开始等待下一个待旅行的日子到来。
            dayIdx+=1;
            // 如果一月前，买了月票，会不会更便宜？
            // 如果一周前，买了周票，会不会更便宜？
            // 如果都不会的话，那我暂时先买日票试试呗。
            lastAllDaysCost[today] = Math.Min(
                    Math.Min(
                            lastAllDaysCost[Math.Max(0, today - 1)] + costs[0]
                            , lastAllDaysCost[Math.Max(0, today - 7)] + costs[1])
                    , lastAllDaysCost[Math.Max(0, today - 30)] + costs[2]);
        }
        return lastAllDaysCost[days[days.Length - 1]];
    
    }
}

public class Solution {
    public int MincostTickets(int[] days, int[] costs) {
        if (days.Length == 0)
            return 0;
        var n = days.Last();
        var travel = new bool[n + 1];
        foreach (var day in days)
            travel[day] = true;
        var dp = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            if (!travel[i])
                dp[i] = dp[i - 1];
            else
            {
                dp[i] = Math.Min(Math.Min((i > 0? dp[i - 1] : 0) + costs[0],
                            (i > 6? dp[i - 7] : 0) + costs[1]),
                            (i > 29? dp[i - 30]: 0) + costs[2]);
            }
        }
        return dp[n];
    }
}
 
*/
