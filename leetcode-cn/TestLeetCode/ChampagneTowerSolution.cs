﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
我们把玻璃杯摆成金字塔的形状，其中第一层有1个玻璃杯，第二层有2个，依次类推到第100层，每个玻璃杯(250ml)将盛有香槟。

从顶层的第一个玻璃杯开始倾倒一些香槟，当顶层的杯子满了，任何溢出的香槟都会立刻等流量的流向左右两侧的玻璃杯。当左右两边的杯子也满了，就会等流量的流向它们左右两边的杯子，依次类推。（当最底层的玻璃杯满了，香槟会流到地板上）

例如，在倾倒一杯香槟后，最顶层的玻璃杯满了。倾倒了两杯香槟后，第二层的两个玻璃杯各自盛放一半的香槟。在倒三杯香槟后，第二层的香槟满了 - 此时总共有三个满的玻璃杯。在倒第四杯后，第三层中间的玻璃杯盛放了一半的香槟，他两边的玻璃杯各自盛放了四分之一的香槟，如下图所示。



现在当倾倒了非负整数杯香槟后，返回第 i 行 j 个玻璃杯所盛放的香槟占玻璃杯容积的比例（i 和 j都从0开始）。

示例 1:
输入: poured(倾倒香槟总杯数) = 1, query_glass(杯子的位置数) = 1, query_row(行数) = 1
输出: 0.0
解释: 我们在顶层（下标是（0，0））倒了一杯香槟后，没有溢出，因此所有在顶层以下的玻璃杯都是空的。

示例 2:
输入: poured(倾倒香槟总杯数) = 2, query_glass(杯子的位置数) = 1, query_row(行数) = 1
输出: 0.5
解释: 我们在顶层（下标是（0，0）倒了两杯香槟后，有一杯量的香槟将从顶层溢出，位于（1，0）的玻璃杯和（1，1）的玻璃杯平分了这一杯香槟，所以每个玻璃杯有一半的香槟。
注意:

poured 的范围[0, 10 ^ 9]。
query_glass 和query_row 的范围 [0, 99]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/champagne-tower/
/// 799. 香槟塔
/// https://blog.csdn.net/torch_man/article/details/80638799
/// </summary>
class ChampagneTowerSolution
{
    public void Test()
    {
        var ret = ChampagneTower(2, 0, 0);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public double ChampagneTower(int poured, int query_row, int query_glass)
    {
        int N = query_row + 1;
        int Length = (1 + N) * N / 2; 

        float[] capacity = new float[Length];
        Array.Fill(capacity, 0);
        capacity[0] = poured;

        for (int i = 0; i < query_row; i++)
        {
            int preN = i - 1 + 1; // 上一行的N
            int preLen = (1 + preN) * preN / 2;

            int currentN = i + 1; // 当前行的N
            int currentLen = preLen + currentN;

            bool anyWine = false;
            for (int j = 0; j <= i; j++)
            {
                int index = preLen + j;
                var remainHalf = (capacity[index] - 1.0f) / 2f;
                if (0 < remainHalf)
                {
                    int nextIndex = currentLen + j; // 下一行对应的下标
                    capacity[nextIndex] += remainHalf;
                    capacity[nextIndex + 1] += remainHalf;

                    anyWine = true;
                }
            }
            if (!anyWine) break;
        }

        {
            int preN = query_row - 1 + 1; // 上一行的N
            int preLen = (1 + preN) * preN / 2;
            return Math.Min(capacity[preLen + query_glass], 1.0);
        }
    }
}