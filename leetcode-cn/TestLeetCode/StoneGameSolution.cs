﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
亚历克斯和李用几堆石子在做游戏。偶数堆石子排成一行，每堆都有正整数颗石子 piles[i] 。

游戏以谁手中的石子最多来决出胜负。石子的总数是奇数，所以没有平局。

亚历克斯和李轮流进行，亚历克斯先开始。 每回合，玩家从行的开始或结束处取走整堆石头。 这种情况一直持续到没有更多的石子堆为止，此时手中石子最多的玩家获胜。

假设亚历克斯和李都发挥出最佳水平，当亚历克斯赢得比赛时返回 true ，当李赢得比赛时返回 false 。

 

示例：

输入：[5,3,4,5]
输出：true
解释：
亚历克斯先开始，只能拿前 5 颗或后 5 颗石子 。
假设他取了前 5 颗，这一行就变成了 [3,4,5] 。
如果李拿走前 3 颗，那么剩下的是 [4,5]，亚历克斯拿走后 5 颗赢得 10 分。
如果李拿走后 5 颗，那么剩下的是 [3,4]，亚历克斯拿走后 4 颗赢得 9 分。
这表明，取前 5 颗石子对亚历克斯来说是一个胜利的举动，所以我们返回 true 。
 

提示：

2 <= piles.length <= 500
piles.length 是偶数。
1 <= piles[i] <= 500
sum(piles) 是奇数。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/stone-game/
/// 877. 石子游戏
/// 
/// </summary>
class StoneGameSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool StoneGame(int[] piles)
    {
        int len = piles.Length;
        int[] scores = new int[len];
        for (int i = 0; i < len; i++)
            scores[i] = -piles[i]; // 倒数第一轮 Lee

        bool isAlex = true;
        for (int upper = len - 1, offset = 1; 0 < upper; offset++,  upper--, isAlex = !isAlex)
        {
            if (isAlex)
            {
                for (int i = 0; i < upper; i++)
                    scores[i] = Math.Max(piles[i] + scores[i + 1], piles[i + offset] + scores[i]);
            }
            else
            {
                for (int i = 0; i < upper; i++)
                    scores[i] = Math.Min(-piles[i] + scores[i + 1], -piles[i + offset] + scores[i]);
            }
        }
        return 0 < scores[0];
    }
}
/*
方法一：动态规划
思路

让我们改变游戏规则，使得每当李得分时，都会从亚历克斯的分数中扣除。

令 dp(i, j) 为亚历克斯可以获得的最大分数，其中剩下的堆中的石子数是 piles[i], piles[i+1], ..., piles[j]。

这在比分游戏中很自然：我们想知道游戏中每个位置的值。

我们可以根据 dp(i + 1，j) 和 dp(i，j-1) 来制定 dp(i，j) 的递归，我们可以使用动态编程以不重复这个递归中的工作。该方法可以输出正确的答案，因为状态形成一个DAG（有向无环图）。

算法

当剩下的堆的石子数是 piles[i], piles[i+1], ..., piles[j] 时，轮到的玩家最多有 2 种行为。

可以通过比较 j-i和 N modulo 2 来找出轮到的人。

如果玩家是亚历克斯，那么她将取走 piles[i] 或 piles[j] 颗石子，增加她的分数。

之后，总分为 piles[i] + dp(i+1, j) 或 piles[j] + dp(i, j-1)；我们想要其中的最大可能得分。

如果玩家是李，那么他将取走 piles[i] 或 piles[j] 颗石子，减少亚历克斯这一数量的分数。

之后，总分为 -piles[i] + dp(i+1, j) 或 -piles[j] + dp(i, j-1)；我们想要其中的最小可能得分。

C++JavaJavaScriptPython3
class Solution {
public:
    bool stoneGame(vector<int>& piles) {
        int N = piles.size();

        // dp[i+1][j+1] = the value of the game [piles[i], ..., piles[j]]
        int dp[N+2][N+2];
        memset(dp, 0, sizeof(dp));

        for (int size = 1; size <= N; ++size)
            for (int i = 0, j = size - 1; j < N; ++i, ++j) {
                int parity = (j + i + N) % 2;  // j - i - N; but +x = -x (mod 2)
                if (parity == 1)
                    dp[i+1][j+1] = max(piles[i] + dp[i+2][j+1], piles[j] + dp[i+1][j]);
                else
                    dp[i+1][j+1] = min(-piles[i] + dp[i+2][j+1], -piles[j] + dp[i+1][j]);
            }

        return dp[1][N] > 0;
    }
};
复杂度分析

时间复杂度：O(N^2)O(N 
2
 )，其中 NN 是石子堆的数目。
空间复杂度：O(N^2)O(N 
2
 )，该空间用以存储每个子游戏的中间结果。
方法二：数学
思路和算法

显然，亚历克斯总是赢得 2 堆时的游戏。 通过一些努力，我们可以获知她总是赢得 4 堆时的游戏。

如果亚历克斯最初获得第一堆，她总是可以拿第三堆。 如果她最初取到第四堆，她总是可以取第二堆。第一 + 第三，第二 + 第四 中的至少一组是更大的，所以她总能获胜。

我们可以将这个想法扩展到 N 堆的情况下。设第一、第三、第五、第七桩是白色的，第二、第四、第六、第八桩是黑色的。 亚历克斯总是可以拿到所有白色桩或所有黑色桩，其中一种颜色具有的石头数量必定大于另一种颜色的。

因此，亚历克斯总能赢得比赛。

C++JavaPythonJavaScript
class Solution {
public:
    bool stoneGame(vector<int>& piles) {
        return true;
    }
};
复杂度分析

时间和空间复杂度：O(1)O(1)。


public class Solution {
    public bool StoneGame(int[] piles) {
        int[,] ansTable = new int[piles.Length,piles.Length];
        for(int i=0;i<piles.Length;i++){
            ansTable[i,i] = piles[i];
        }
        for(int j=1;j<piles.Length;j++){
            for(int i = 0;i<piles.Length-j;i++){
                ansTable[i,i+j] = Math.Max(piles[i+j]-ansTable[i,i+j-1],piles[i]-ansTable[i+1,i+j]);
                // Console.WriteLine($"{i},{j}:{ansTable[i,j]}");
            }
        }
        // Console.WriteLine(ansTable[0,piles.Length-1]);
        return ansTable[0,piles.Length-1]>0;
    }
}
*/
