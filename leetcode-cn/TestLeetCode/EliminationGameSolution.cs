using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个从1 到 n 排序的整数列表。
首先，从左到右，从第一个数字开始，每隔一个数字进行删除，直到列表的末尾。
第二步，在剩下的数字中，从右到左，从倒数第一个数字开始，每隔一个数字进行删除，直到列表开头。
我们不断重复这两步，从左到右和从右到左交替进行，直到只剩下一个数字。
返回长度为 n 的列表中，最后剩下的数字。

示例：

输入:
n = 9,
1 2 3 4 5 6 7 8 9
2 4 6 8
2 6
6

输出:
6 
*/
/// <summary>
/// https://leetcode-cn.com/problems/elimination-game/
/// 390. 消除游戏
/// https://blog.csdn.net/qq_36946274/article/details/81416957
/// </summary>
class EliminationGameSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LastRemaining(int n)
    {
        //将其镜像对折为一个子问题，当前状态可以推出的下一个状态的结果，但是相反
        return n == 1 ? 1 : 2 * (n / 2 + 1 - LastRemaining(n / 2));
    }
}
/*
public class Solution {
    public int LastRemaining(int n) {
        if (n==1)
        {
            return 1;
        }
        else
        {
            return 2*(n/2 + 1 - LastRemaining(n/2));
        }
    }
} 
*/
