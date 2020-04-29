using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个正整数 n，生成一个包含 1 到 n2 所有元素，且元素按顺时针顺序螺旋排列的正方形矩阵。

示例:

输入: 3
输出:
[
 [ 1, 2, 3 ],
 [ 8, 9, 4 ],
 [ 7, 6, 5 ]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/spiral-matrix-ii/
/// 59.旋转矩阵II
/// 
/// 
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


    public int[][] GenerateMatrix(int n) {

        int left = 0, right = n - 1, top = 0, bottom = n - 1;
        int[][] ret = new int[n][];
        for (int i = 0; i < n; i++)
            ret[i] = new int[n];

        int num = 1, tar = n * n;
        while(num <= tar){
            for(int i = left; i <= right; i++) ret[top][i] = num++;
            top++;

            for(int i = top; i <= bottom; i++) ret[i][right] = num++; 
            right--;

            for(int i = right; i >= left; i--) ret[bottom][i] = num++; 
            bottom--;

            for(int i = bottom; i >= top; i--) ret[i][left] = num++; 
            left++;
        }
        return ret;
    }

    //public int[,] GenerateMatrix(int n)
    //{
    //    if ( n < 1 ) return null;

    //    int[,] ret = new int[n, n];

    //    int up = 0, down = n - 1, left = 0, right = n - 1;
    //    int v = 1;

    //    while( true)
    //    {
    //        for( int i = left; i <= right; i++) ret[up, i] = v++;
    //        if (down < ++up) break;

    //        for (int i = up; i <= down; i++) ret[i, right] = v++;
    //        if (--right < left) break;

    //        for (int i = right; left <= i; i--) ret[down, i] = v++;
    //        if (--down < up) break;

    //        for (int i = down; up <= i; i--) ret[i, left] = v++;
    //        if (right < ++left) break;
    //    }

    //    return ret;
    //}
}
/*

Spiral Matrix II （模拟法，设定边界，代码简短清晰）
Krahets
发布于 10 个月前
13.4k
思路：
生成一个 n×n 空矩阵 mat，随后模拟整个向内环绕的填入过程：
定义当前左右上下边界 l,r,t,b，初始值 num = 1，迭代终止值 tar = n * n；
当 num <= tar 时，始终按照 从左到右 从上到下 从右到左 从下到上 填入顺序循环，每次填入后：
执行 num += 1：得到下一个需要填入的数字；
更新边界：例如从左到右填完后，上边界 t += 1，相当于上边界向内缩 1。
使用num <= tar而不是l < r || t < b作为迭代条件，是为了解决当n为奇数时，矩阵中心数字无法在迭代过程中被填充的问题。
最终返回 mat 即可。
Picture1.png

代码：
class Solution {
    public int[][] generateMatrix(int n) {
        int l = 0, r = n - 1, t = 0, b = n - 1;
        int[][] mat = new int[n][n];
        int num = 1, tar = n * n;
        while(num <= tar){
            for(int i = l; i <= r; i++) mat[t][i] = num++; // left to right.
            t++;
            for(int i = t; i <= b; i++) mat[i][r] = num++; // top to bottom.
            r--;
            for(int i = r; i >= l; i--) mat[b][i] = num++; // right to left.
            b--;
            for(int i = b; i >= t; i--) mat[i][l] = num++; // bottom to top.
            l++;
        }
        return mat;
    }
}
下一篇：模拟过程，时间复杂度 O(n^2)，空间复杂度 O(n^2)

public partial class Solution
{
    public int[][] GenerateMatrix(int n)
    {
        int[][] matrix = new int[n][];
        for (var i = 0; i < n; ++i)
        { matrix[i] = new int[n]; }
        int index = 0, limit = n * n, x = 0, y = -1, direct = 0;
        // direct: 0=right,1=down,2=left,3=up
        while (index < limit)
        {
            switch (direct)
            {
                case 0:
                    while (++y < n - x) matrix[x][y] = ++index;
                    --y; ++direct; break;
                case 1:
                    while (++x <= y) matrix[x][y] = ++index;
                    --x; ++direct; break;
                case 2:
                    while (--y >= n - x -1) matrix[x][y] = ++index;
                    ++y; ++direct; break;
                case 3:
                    while (--x > y) matrix[x][y] = ++index;
                    ++x; direct = 0; break;
                default: break;
            }
        }
        return matrix;
    }
}

 
*/