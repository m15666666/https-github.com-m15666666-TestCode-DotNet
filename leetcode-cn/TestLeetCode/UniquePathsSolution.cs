using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/unique-paths/
/// 62.不同路径
/// 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
/// 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
/// 问总共有多少条不同的路径？
/// </summary>
class UniquePathsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int UniquePaths(int m, int n)
    {
        if (m < 1 || n < 1) return 0;
        if (m == 1 || n == 1) return 1;

        int[] vertical = new int[m - 1];

        // 最下面一行，和最右面一列的路径都是1.
        for (int i = 0; i < vertical.Length; i++) vertical[i] = 1;

        for( int c = 0; c < n - 1; c++)
        {
            int horizontalValue = 1;
            for (int i = 0; i < vertical.Length; i++)
            {
                horizontalValue += vertical[i];
                vertical[i] = horizontalValue;
            }
        }

        return vertical[vertical.Length - 1];
    }

}