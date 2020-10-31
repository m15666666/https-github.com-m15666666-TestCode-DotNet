/*
你和你的朋友，两个人一起玩 Nim 游戏：

桌子上有一堆石头。
你们轮流进行自己的回合，你作为先手。
每一回合，轮到的人拿掉 1 - 3 块石头。
拿掉最后一块石头的人就是获胜者。
j假设你们每一步都是最优解。请编写一个函数，来判断你是否可以在给定石头数量为 n 的情况下赢得游戏。如果可以赢，返回 true；否则，返回 false 。

 

示例 1：

输入：n = 4
输出：false
解释：如果堆中有 4 块石头，那么你永远不会赢得比赛；
     因为无论你拿走 1 块、2 块 还是 3 块石头，最后一块石头总是会被你的朋友拿走。
示例 2：

输入：n = 1
输出：true
示例 3：

输入：n = 2
输出：true
 

提示：

1 <= n <= 231 - 1

*/

/// <summary>
/// https://leetcode-cn.com/problems/nim-game/
/// 292. Nim 游戏
///
///
/// </summary>
internal class NimGameSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CanWinNim(int n)
    {
        return (n % 4) != 0;
    }
}

/*

Nim 游戏
力扣 (LeetCode)
发布于 2019-01-07
30.1k
如果堆中石头的数量 nn 不能被 44 整除，那么你总是可以赢得 Nim 游戏的胜利。

推理

让我们考虑一些小例子。显而易见的是，如果石头堆中只有一块、两块、或是三块石头，那么在你的回合，你就可以把全部石子拿走，从而在游戏中取胜。而如果就像题目描述那样，堆中恰好有四块石头，你就会失败。因为在这种情况下不管你取走多少石头，总会为你的对手留下几块，使得他可以在游戏中打败你。因此，要想获胜，在你的回合中，必须避免石头堆中的石子数为 4 的情况。

同样地，如果有五块、六块、或是七块石头，你可以控制自己拿取的石头数，总是恰好给你的对手留下四块石头，使他输掉这场比赛。但是如果石头堆里有八块石头，你就不可避免地会输掉，因为不管你从一堆石头中挑出一块、两块还是三块，你的对手都可以选择三块、两块或一块，以确保在再一次轮到你的时候，你会面对四块石头。

显然，它以相同的模式不断重复 n=4,8,12,16,\dotsn=4,8,12,16,…，基本可以看出是 44 的倍数。


public boolean canWinNim(int n) {
    return (n % 4 != 0);
}
复杂度分析

时间复杂度：O(1)O(1)，只进行了一次检查。
空间复杂度：O(1)O(1)，没有使用额外的空间

class Solution {
    public boolean canWinNim(int n) {
        if(n < 4){
            return true;
        }
        return !canWinNim(n - 1) ||!canWinNim(n - 2) || !canWinNim(n - 3); 
    }
}

public class Solution {
    public bool CanWinNim(int n) {
        n = n %4;
        if(n>=1) return true;
        else return false;
    }
}

public class Solution {
    public bool CanWinNim(int n) {
        //return n%4>0;

        // if (n<=3) return true;
        // else return (n&3)!=0;

        return (n&3)!=0 || (n>>2)==0;
    }
}


*/