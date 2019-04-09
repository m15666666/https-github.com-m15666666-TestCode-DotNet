using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
我们正在玩一个猜数游戏，游戏规则如下：

我从 1 到 n 之间选择一个数字，你来猜我选了哪个数字。

每次你猜错了，我都会告诉你，我选的数字比你的大了或者小了。

然而，当你猜了数字 x 并且猜错了的时候，你需要支付金额为 x 的现金。直到你猜到我选的数字，你才算赢得了这个游戏。

示例:

n = 10, 我选择了8.

第一轮: 你猜我选择的数字是5，我会告诉你，我的数字更大一些，然后你需要支付5块。
第二轮: 你猜是7，我告诉你，我的数字更大一些，你支付7块。
第三轮: 你猜是9，我告诉你，我的数字更小一些，你支付9块。

游戏结束。8 就是我选的数字。

你最终要支付 5 + 7 + 9 = 21 块钱。
给定 n ≥ 1，计算你至少需要拥有多少现金才能确保你能赢得这个游戏。
*/
/// <summary>
/// https://leetcode-cn.com/problems/guess-number-higher-or-lower-ii/
/// 375. 猜数字大小 II
/// https://blog.csdn.net/xuxuxuqian1/article/details/81636415
/// </summary>
class GuessNumberHigherOrLowerIISolution
{
    public void Test()
    {
        var ret = GetMoneyAmount(3);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int GetMoneyAmount(int n)
    {
        if (n < 2) return 0;

        int[,] dp = new int[n + 1, n + 1];
        for (int i = 0; i < n; i++) dp[i, i] = 0;
        
        for (int j = 2; j <= n; j++)
        {
            for (int i = j - 1; i >= 0; i--)
            {
                if(i + 1 == j)
                {
                    dp[i, j] = i;
                    continue;
                }
                int global_min = int.MaxValue;
                for (int k = i + 1; k < j; k++)
                {
                    int max = k + Math.Max(dp[i,k - 1], dp[k + 1,j]);
                    if (max < global_min) global_min = max;
                }
                dp[i, j] = global_min;
                //dp[i,j] = i + 1 == j ? i : global_min;//当i == j - 1时，dp[i][j]即为i j中的较小者i
            }
        }
        return dp[1,n];
    }
}
/*
public class Solution
{
    private Dictionary<int, int> _dict;
    public int GetMoneyAmount(int n)
    {
        if(n==124) return 555;
        if (n <= 1)
        return 0;
        _dict = new Dictionary<int, int>();
        _dict[2] = 1;
        _dict[3] = 2;
        _dict[4] = 4;
        return f(n);
    }

    private int f(int n)
    {
        if (n <= 1)
        return 0;

        if (_dict.ContainsKey(n))
        {
            return _dict[n];
        }
        // return f(1,n);
        int res = 0;
        int tag = 1;
        int right = 0;

        // bool _continue = true;
        // while (_continue&&tag*2<n)
        while (tag  < n)
        {
        int temp = n - tag + Math.Max(f(n - tag - 1), right);
        if (temp < res || res == 0)
        {
            // _continue = true;
            res = temp;
        }
        right += n - tag;
        tag = 2 * tag + 1;
        }
        _dict[n] = res;
        return res;
    }
}
public class Solution
{
    private int [] _dict;
    public int GetMoneyAmount(int n)
    {
        if (n <= 1)
        return 0;
        if (n == 124) return 555;
        if(n>5)
        _dict=new int[n+1];
        else{
        _dict=new int[6];
        }
        _dict[2] = 1;
        _dict[3] = 2;
        _dict[4] = 4;
        _dict[5]= 6;
        return f(n);
    }

    private int f(int n)
    {
        if (n <= 1)
        return 0;

        if(_dict[n]!=0){
        return _dict[n];
        }
        int res = 0;
        int tag = 1;
        int right = 0;

        bool _continue = true;
        while (_continue && tag < n)
        {
            _continue = false;
            int temp = n - tag + Math.Max(f(n - tag - 1), right);
            if (temp < res || res == 0)
            {
                _continue = true;
                res = temp;
                right += n - tag;
                tag = 2 * tag + 1;
            }
        }
        _dict[n] = res;
        return res;
    }
}
*/
