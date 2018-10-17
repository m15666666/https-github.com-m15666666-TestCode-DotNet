using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/spiral-matrix-ii/
/// 59.旋转矩阵II
/// 给定一个正整数 n，生成一个包含 1 到 n2 所有元素，且元素按顺时针顺序螺旋排列的正方形矩阵。
/// </summary>
class GenerateMatrixSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int[,] GenerateMatrix(int n)
    {
        if ( n < 1 ) return null;

        int[,] ret = new int[n, n];

        int up = 0, down = n - 1, left = 0, right = n - 1;
        int v = 1;

        while( true)
        {
            for( int i = left; i <= right; i++) ret[up, i] = v++;
            if (down < ++up) break;

            for (int i = up; i <= down; i++) ret[i, right] = v++;
            if (--right < left) break;

            for (int i = right; left <= i; i--) ret[down, i] = v++;
            if (--down < up) break;

            for (int i = down; up <= i; i--) ret[i, left] = v++;
            if (right < ++left) break;
        }

        return ret;
    }
}