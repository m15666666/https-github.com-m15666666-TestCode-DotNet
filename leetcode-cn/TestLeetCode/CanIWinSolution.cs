using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在 "100 game" 这个游戏中，两名玩家轮流选择从 1 到 10 的任意整数，累计整数和，先使得累计整数和达到 100 的玩家，即为胜者。

如果我们将游戏规则改为 “玩家不能重复使用整数” 呢？

例如，两个玩家可以轮流从公共整数池中抽取从 1 到 15 的整数（不放回），直到累计整数和 >= 100。

给定一个整数 maxChoosableInteger （整数池中可选择的最大数）和另一个整数 desiredTotal（累计和），判断先出手的玩家是否能稳赢（假设两位玩家游戏时都表现最佳）？

你可以假设 maxChoosableInteger 不会大于 20， desiredTotal 不会大于 300。

示例：

输入：
maxChoosableInteger = 10
desiredTotal = 11

输出：
false

解释：
无论第一个玩家选择哪个整数，他都会失败。
第一个玩家可以选择从 1 到 10 的整数。
如果第一个玩家选择 1，那么第二个玩家只能选择从 2 到 10 的整数。
第二个玩家可以通过选择整数 10（那么累积和为 11 >= desiredTotal），从而取得胜利.
同样地，第一个玩家选择任意其他整数，第二个玩家都会赢。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/can-i-win/
/// 464. 我能赢吗
/// https://blog.csdn.net/laputafallen/article/details/79968342
/// https://blog.csdn.net/start_lie/article/details/84140106
/// </summary>
class CanIWinSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanIWin(int maxChoosableInteger, int desiredTotal)
    {
        var n = maxChoosableInteger;

        //如果目标数<=选择数,一次就赢,返回true
        if (desiredTotal <= n) return true;

        //获得总数之和,选择数总数比目标数小,结果不可达,返回false
        var sum = n * (n + 1) / 2;
        if ( sum < desiredTotal) return false;

        byte[] dp = new byte[1 << n];
        Array.Fill<byte>(dp, 0);

        return CanIWin(0, n, desiredTotal, dp);
    }
    private static bool CanIWin(int state, int n, int total, byte[] dp)
    {
        if (total <= 0) return false;

        const byte NoneInit = 0;
        const byte Win = 1;
        const byte Lose = 2;

        //dp代表已经使用了哪个数字情况下是否能赢
        if (dp[state] == NoneInit)
        {
            dp[state] = Lose;
            int mask = 1;
            for (int i = 1; i <= n; i++) {
                int used = state | mask;
                //如果,我能用这个数,且用了这个数对方赢不了
                if (used != state && !CanIWin(used, n, total - i, dp)) {
                    dp[state] = Win;
                    return true;
                }
                mask <<= 1;
            }
        }
        return dp[state] == Win;
    }
}