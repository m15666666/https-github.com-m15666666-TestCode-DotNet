using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
有两种形状的瓷砖：一种是 2x1 的多米诺形，另一种是形如 "L" 的托米诺形。两种形状都可以旋转。

XX  <- 多米诺

XX  <- "L" 托米诺
X
给定 N 的值，有多少种方法可以平铺 2 x N 的面板？返回值 mod 10^9 + 7。

（平铺指的是每个正方形都必须有瓷砖覆盖。两个平铺不同，当且仅当面板上有四个方向上的相邻单元中的两个，使得恰好有一个平铺有一个瓷砖占据两个正方形。）

示例:
输入: 3
输出: 5
解释: 
下面列出了五种不同的方法，不同字母代表不同瓷砖：
XYZ XXZ XYY XXY XYY
XYZ YYZ XZZ XYY XXY
提示：

N  的范围是 [1, 1000]
*/
/// <summary>
/// https://leetcode-cn.com/problems/domino-and-tromino-tiling/
/// 790. 多米诺和托米诺平铺
/// https://blog.csdn.net/qiang_____0712/article/details/85066880
/// https://blog.csdn.net/scylhy/article/details/98613654
/// </summary>
class DominoAndTrominoTilingSolution
{
    public void Test()
    {
        var ret = NumTilings(30);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumTilings(int N)
    {
        if (N < 1) return 0;

        switch (N)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 5;
        }

        List<long> evens = new List<long>(3) {1,2,5};
        List<long> odds = new List<long>(3) { 2,4,8};

        const long C = 1000000000 + 7;
        for (int i = 4; i <= N; i++)
        {
            var even = (evens[2] + evens[1] + 2 * evens[0] + odds[0]) % C;
            var odd = (odds[2] + 2 * evens[2]) % C;

            evens.RemoveAt(0);
            odds.RemoveAt(0);

            evens.Add(even);
            odds.Add(odd);
        }

        return (int)evens[2];
    }
}